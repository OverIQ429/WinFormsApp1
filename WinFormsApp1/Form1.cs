using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;
using System.Threading;
using System.Reflection;

namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        private Bitmap myImage;
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp;*.gif";
                openFileDialog.Title = "Выберите изображение";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {

                    myImage = new Bitmap(openFileDialog.FileName);
                    FileInfo fileInfo = new FileInfo(openFileDialog.FileName);
                    double fileSizeInMB = fileInfo.Length / (1024.0 * 1024.0);
                    labelSize.Text += $" Размер файла: {fileSizeInMB:F2} MB";
                }
                ImageFormat format = myImage.RawFormat;
                pictureBox1.Image = myImage;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image != null)
            {
                using (SaveFileDialog saveFileDialog = new SaveFileDialog())
                {
                    saveFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp;*.gif";
                    saveFileDialog.Title = "Сохраните изображение";
                    saveFileDialog.FileName = "image";
                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        pictureBox1.Image.Save(saveFileDialog.FileName);
                    }
                }
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            labelTrackBarValue.Text = $"{trackBar1.Value}";
        }

        private void ReduceImageQuality(Bitmap originalImage, int qualityFactor)
        {
            int width = originalImage.Width;
            int height = originalImage.Height;
            Bitmap newImage = new Bitmap(width, height);
            double[,] redChannel = new double[height, width]; //Массивы для хранения значений цветовых каналов
            double[,] greenChannel = new double[height, width];
            double[,] blueChannel = new double[height, width];
            int type0 = 0;
            int type1 = 1;
            int type2 = 2;
            int type3 = 3;
            async Task manytorrent(int type)
            {
                for (int y = type; y < height; y += 4)
                {
                    for (int x = 0; x < width; x++)
                    {
                        Color pixel = originalImage.GetPixel(x, y);

                        redChannel[y, x] = pixel.R;
                        greenChannel[y, x] = pixel.G;
                        blueChannel[y, x] = pixel.B;

                    }
                }
            }
            async void StartProcessing()
            {
                Task[] tasks = new Task[]
                {
                    manytorrent(type0),
                    manytorrent(type1),
                    manytorrent(type2),
                    manytorrent(type3)
                };
                await Task.WhenAll(tasks);
            }

            //Thread thr0 = new Thread(manytorrent0);
            //Thread thr1 = new Thread(manytorrent1);
            //Thread thr2 = new Thread(manytorrent2);
            //Thread thr3 = new Thread(manytorrent3);
            //thr1.Start();
            //thr2.Start();
            //thr3.Start();
            //thr0.Start();
            StartProcessing();
            var redMatrix = DenseMatrix.OfArray(redChannel);
            var greenMatrix = DenseMatrix.OfArray(greenChannel);
            var blueMatrix = DenseMatrix.OfArray(blueChannel); //создаються матрицы
            var redSvd = redMatrix.Svd(true);
            var greenSvd = greenMatrix.Svd(true);
            var blueSvd = blueMatrix.Svd(true); //Сингулярное разложение
            //сжатие цветового канала
            int numComponents = Math.Min(redSvd.S.Count, Math.Min(greenSvd.S.Count, blueSvd.S.Count)) * qualityFactor / 100;
            var compressedRed = redSvd.U.SubMatrix(0, height, 0, numComponents) *
                    DenseMatrix.OfDiagonalArray(numComponents, numComponents, redSvd.S.SubVector(0, numComponents).ToArray()) *
                    redSvd.VT.SubMatrix(0, numComponents, 0, width);

            var compressedGreen = greenSvd.U.SubMatrix(0, height, 0, numComponents) *
                      DenseMatrix.OfDiagonalArray(numComponents, numComponents, greenSvd.S.SubVector(0, numComponents).ToArray()) *
                      greenSvd.VT.SubMatrix(0, numComponents, 0, width);

            var compressedBlue = blueSvd.U.SubMatrix(0, height, 0, numComponents) *
                     DenseMatrix.OfDiagonalArray(numComponents, numComponents, blueSvd.S.SubVector(0, numComponents).ToArray()) *
                     blueSvd.VT.SubMatrix(0, numComponents, 0, width);

            Bitmap compressedImage = new Bitmap(width, height);

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    // Получаем значения пикселей, округляя до целых
                    int r = (int)Math.Clamp(compressedRed[y, x], 0, 255);
                    int g = (int)Math.Clamp(compressedGreen[y, x], 0, 255);
                    int b = (int)Math.Clamp(compressedBlue[y, x], 0, 255);

                    // Устанавливаем пиксель в новое изображение
                    compressedImage.SetPixel(x, y, Color.FromArgb(r, g, b));
                }
            }
            string compressedImagePath = "compressed_image.jpg";
            FileInfo fileInfo = new FileInfo(compressedImagePath);
            compressedImage.Save("compressed_image.jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
            double fileSizeInMB = fileInfo.Length / (1024.0 * 1024.0);
            labelSize.Text += $" Размер файла: {fileSizeInMB:F2} MB";

            pictureBox1.Image = compressedImage;
        }
        private void aprowdbutton_Click(object sender, EventArgs e)
        {
            if (myImage != null)
            {
                ReduceImageQuality(myImage, trackBar1.Value);
            }
        }
    }
}
