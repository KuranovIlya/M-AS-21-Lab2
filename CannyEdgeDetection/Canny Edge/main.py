# Подключение библиотек
import sys

import numpy as np
import imageio.v2
from scipy import ndimage
import cupy
import os


# Функции фазы сглаживания

# Функция преобразования изображения к оттенкам серого
def rgb2gray(img):
    return np.dot(img[..., :3], [0.2989, 0.5870, 0.1140])

# Определение градиента функций с использованием фильтра Собеля
def gradient_x(img):
    grad_img = ndimage.convolve(img, np.array([[-1, 0, 1], [-2, 0, 2], [-1, 0, 1]]))
    return grad_img / np.max(grad_img)


def gradient_y(img):
    grad_img = ndimage.convolve(img, np.array([[-1, -2, -1], [0, 0, 0], [1, 2, 1]]))
    return grad_img / np.max(grad_img)


# Вычисление значения градиента
def gradient_mag(fx, fy):
    grad_mag = np.hypot(fx, fy)
    return grad_mag / np.max(grad_mag)


# Функции фазы подавления не-максимумов

# 2.a : Поиск ближайших направлений
def closest_dir_function(grad_dir):
    closest_dir_arr = np.zeros(grad_dir.shape)
    for i in range(1, int(grad_dir.shape[0] - 1)):
        for j in range(1, int(grad_dir.shape[1] - 1)):

            if ((-22.5 < grad_dir[i, j] <= 22.5) or (
                    -157.5 >= grad_dir[i, j] > 157.5)):
                closest_dir_arr[i, j] = 0

            elif ((22.5 < grad_dir[i, j] <= 67.5) or (
                    -112.5 >= grad_dir[i, j] > -157.5)):
                closest_dir_arr[i, j] = 45

            elif ((67.5 < grad_dir[i, j] <= 112.5) or (
                    -67.5 >= grad_dir[i, j] > -112.5)):
                closest_dir_arr[i, j] = 90

            else:
                closest_dir_arr[i, j] = 135

    return closest_dir_arr


# 2.b : Преобразование в тонкие линии
def non_maximal_suppressor(grad_mag, closest_dir):
    thinned_output = np.zeros(grad_mag.shape)
    for i in range(1, int(grad_mag.shape[0] - 1)):
        for j in range(1, int(grad_mag.shape[1] - 1)):

            if closest_dir[i, j] == 0:
                if (grad_mag[i, j] > grad_mag[i, j + 1]) and (grad_mag[i, j] > grad_mag[i, j - 1]):
                    thinned_output[i, j] = grad_mag[i, j]
                else:
                    thinned_output[i, j] = 0

            elif closest_dir[i, j] == 45:
                if (grad_mag[i, j] > grad_mag[i + 1, j + 1]) and (grad_mag[i, j] > grad_mag[i - 1, j - 1]):
                    thinned_output[i, j] = grad_mag[i, j]
                else:
                    thinned_output[i, j] = 0

            elif closest_dir[i, j] == 90:
                if (grad_mag[i, j] > grad_mag[i + 1, j]) and (grad_mag[i, j] > grad_mag[i - 1, j]):
                    thinned_output[i, j] = grad_mag[i, j]
                else:
                    thinned_output[i, j] = 0

            else:
                if (grad_mag[i, j] > grad_mag[i + 1, j - 1]) and (grad_mag[i, j] > grad_mag[i - 1, j + 1]):
                    thinned_output[i, j] = grad_mag[i, j]
                else:
                    thinned_output[i, j] = 0

    return thinned_output / np.max(thinned_output)


# Функции фазы пороговой фильтрации

# "Слабые" пиксели включаются в цепочку "сильных"
def DFS(img):
    for i in range(1, int(img.shape[0] - 1)):
        for j in range(1, int(img.shape[1] - 1)):
            if img[i, j] == 1:
                t_max = max(img[i - 1, j - 1], img[i - 1, j], img[i - 1, j + 1], img[i, j - 1],
                            img[i, j + 1], img[i + 1, j - 1], img[i + 1, j], img[i + 1, j + 1])
                if t_max == 2:
                    img[i, j] = 2


# Пороговое значение гистерезиса
def hysteresis_thresholding(img):
    low_ratio = 0.10
    high_ratio = 0.30
    diff = np.max(img) - np.min(img)
    t_low = np.min(img) + low_ratio * diff
    t_high = np.min(img) + high_ratio * diff

    temp_img = np.copy(img)

    # Присвоение значение пикселям
    for i in range(1, int(img.shape[0] - 1)):
        for j in range(1, int(img.shape[1] - 1)):
            # Сильные пиксели
            if img[i, j] > t_high:
                temp_img[i, j] = 2
            # Слабые пиксели
            elif img[i, j] < t_low:
                temp_img[i, j] = 0
            # Промежуточные пиксели
            else:
                temp_img[i, j] = 1

    # Включение слабых пикселей в цепочку чильных
    total_strong = np.sum(temp_img == 2)
    while 1:
        DFS(temp_img)
        if total_strong == np.sum(temp_img == 2):
            break
        total_strong = np.sum(temp_img == 2)

    # Удаление слабых пикселей
    for i in range(1, int(temp_img.shape[0] - 1)):
        for j in range(1, int(temp_img.shape[1] - 1)):
            if temp_img[i, j] == 1:
                temp_img[i, j] = 0

    temp_img = temp_img / np.max(temp_img)
    return temp_img


# Точка входа

output_path = "./Output Images/"
try:
    image_path = sys.argv[1].replace("\'", "\"")
    image_name = sys.argv[2].replace("\'", "\"")

    directory = os.getcwd()

    print(directory)

    input_img = imageio.v2.imread(image_path)

    gray_input_img = rgb2gray(input_img)
    blur_img = ndimage.gaussian_filter(gray_input_img, sigma=1.0)
    x_grad = gradient_x(blur_img)
    y_grad = gradient_y(blur_img)

    grad_mag = gradient_mag(x_grad, y_grad)

    grad_dir = np.degrees(np.arctan2(y_grad, x_grad))

    closest_dir = closest_dir_function(grad_dir)
    thinned_output = non_maximal_suppressor(grad_mag, closest_dir)

    output_img = hysteresis_thresholding(thinned_output)
    imageio.imwrite(output_path + image_name, output_img)

except Exception as Argument:
     f = open(".\Canny Edge\log.txt", "a")

     f.write(str(Argument))
     f.write("\n\n")

     f.close()



