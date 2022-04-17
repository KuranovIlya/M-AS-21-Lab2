using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;
using Emgu.CV;
using Emgu.CV.Structure;

namespace CannyEdgeDetection
{
    public partial class MainForm : Form
    {
        Image<Bgra, byte> ImgInput;
        string image_path = "";
        string image_name = "";

        public MainForm()
        {
            InitializeComponent();

            if (!Directory.Exists(@".\Output Images"))
            {
                Directory.CreateDirectory(@".\Output Images");
            }
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog ofd = new OpenFileDialog();

                ofd.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    ImgInput = new Image<Bgra, byte>(ofd.FileName);
                    pbInputImage.Image = ImgInput.ToBitmap();

                    image_path = ofd.FileName;
                    image_name = ofd.SafeFileName;
                }
            }
            catch
            {
                MessageBox.Show("Не удалось открыть изображение", "Ошибка", MessageBoxButtons.OK);
            }
            
        }

        private void btnCanny_Click(object sender, EventArgs e)
        {
            try
            {
                //if (ImgInput == null)
                //{
                //    return;
                //}

                //Image<Gray, byte> _imgCanny = new Image<Gray, byte>(ImgInput.Width, ImgInput.Height, new Gray(0));
                //_imgCanny = ImgInput.Canny(Convert.ToDouble(nudThresh.Value), Convert.ToDouble(nudThreshLinking.Value));
                //pbCannyImage.Image = _imgCanny.ToBitmap();

                if (!String.IsNullOrWhiteSpace(image_path))
                {
                    processState.Text = "Процесс запущен...";
                    processState.ForeColor = Color.Red;

                    var psi = new ProcessStartInfo();
                    psi.FileName = @".\Canny Edge\venv\Scripts\python.exe";
                    var script = @"Canny Edge\main.py";

                    psi.Arguments = $"\"{script}\" \"{image_path}\" \"{image_name}\"";
                    psi.UseShellExecute = false;
                    psi.CreateNoWindow = true;
                    var process = Process.Start(psi);
                    process.WaitForExit();

                    pbCannyImage.Image = new Bitmap(String.Format(@".\Output Images\{0}", image_name));
                }
            } 
            catch
            {
                MessageBox.Show("Ошибка обработки изображения", "Ошибка", MessageBoxButtons.OK);
            }
            processState.Text = "";
            processState.ForeColor = System.Drawing.Color.Black;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (ImgInput != null)
            {
                try
                {
                    SaveFileDialog sfd = new SaveFileDialog();
                    sfd.Filter = "JPEG|*.jpeg|PNG|*.png|GIF|*.gif;";

                    if (sfd.ShowDialog() == DialogResult.OK)
                    {
                        switch (sfd.FilterIndex)
                        {
                            case 0:
                                {
                                    pbCannyImage.Image.Save(sfd.FileName, ImageFormat.Jpeg);
                                    break;
                                }
                            case 1:
                                {
                                    pbCannyImage.Image.Save(sfd.FileName, ImageFormat.Png);
                                    break;
                                }
                            default:
                                {
                                    pbCannyImage.Image.Save(sfd.FileName, ImageFormat.Gif);
                                    break;
                                }
                        }
                    }
                }
                catch
                {
                    MessageBox.Show("Не удалось сохранить изображение", "Ошибка", MessageBoxButtons.OK);
                }
            }
        }
    }
}
