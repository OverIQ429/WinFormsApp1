using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;
using System.Threading;
using System.Reflection;
using MathNet.Numerics.LinearAlgebra.Factorization;
using System.Diagnostics.Eventing.Reader;
using System.Threading.Channels;

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
                    labelSize.Text = $" Размер файла: {fileSizeInMB:F2} MB";
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
                    string compressedImagePath = "compressed_image.jpg";
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
            async Task StartProcessing()
            {
                Task[] tasks = new Task[]
                {
                    manytorrent(0),
                    manytorrent(1),
                    manytorrent(2),
                    manytorrent(3)
                };
                await Task.WhenAll(tasks);
                
            }
            static int Change_qualiti(Vector<double> singularValues, double qualityFactor)
            {
                double totalEnergy = singularValues.Sum();
                double targetEnergy = totalEnergy * (qualityFactor / 100.0);
                var array_singularValues = singularValues.ToArray();
                double currentEnergy = 0;
                int k = 0;

                while (k < array_singularValues.Length && currentEnergy < targetEnergy)
                {
                    currentEnergy += singularValues[k];
                    k++;
                }

                return Math.Max(1, k);
            }
            StartProcessing();
            async Task<Matrix<double>> Matrix_Arrays_Think(double [,] channel)
            {
                var Matrix_Think = DenseMatrix.OfArray(channel);
                var Svd = Matrix_Think.Svd(true);
                int k = Change_qualiti(Svd.S, qualityFactor);
                var U_k = Svd.U.SubMatrix(0, Svd.U.RowCount, 0, k);
                var S_k = DenseMatrix.OfDiagonalArray(k, k, Svd.S.SubVector(0, k).ToArray());
                var Vt_k = Svd.VT.SubMatrix(0, k, 0, Svd.VT.ColumnCount);
                var compressed = U_k * S_k * Vt_k;
                return compressed;
            }
            async void StartMatrixProcessing()
            {
                var compressedRed = await Matrix_Arrays_Think(redChannel);
                var compressedGreen = await Matrix_Arrays_Think(greenChannel);
                var compressedBlue = await Matrix_Arrays_Think(blueChannel);
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
                labelSize.Text = $" Размер файла: {fileSizeInMB:F2} MB";

                pictureBox1.Image = compressedImage;
            }
            StartMatrixProcessing();
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
