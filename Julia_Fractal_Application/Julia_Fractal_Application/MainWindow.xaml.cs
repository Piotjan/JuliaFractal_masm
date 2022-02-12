using Julia_Fractal_Application.sources;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace Julia_Fractal_Application
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		int dllParam;
		int threads;
		public MainWindow()
		{
			InitializeComponent();
			threads = Convert.ToInt32(Environment.ProcessorCount.ToString());
			ThreadsLabel.Content = "Threads: " + Environment.ProcessorCount.ToString();
			ThreadSlider.Value = threads;
		}

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
			dllParam = 0;
        }

        private void CppRadio_Checked(object sender, RoutedEventArgs e)
        {
			dllParam = 1;
        }

        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
			threads = (int)ThreadSlider.Value;
			ThreadsLabel.Content = "Threads: " + threads;
        }

        private void generateButton_Click(object sender, RoutedEventArgs e)
        {
			int[] threadArray = new int[8] { 1, 2, 4, 8, 12, 16, 32, 64 };
			for(int i = 0;i<10;++i)
            {
				for(int j=0;j<8; ++j)
                {
					Tuple<Bitmap, double> result = GenerateFractal.run(Convert.ToDouble(tbReal.Text), Convert.ToDouble(tbImg.Text), threadArray[j], dllParam);
					Bitmap fractal = result.Item1;
					using (MemoryStream memory = new MemoryStream())
					{
						fractal.Save(memory, System.Drawing.Imaging.ImageFormat.Bmp);
						memory.Position = 0;
						BitmapImage bitmapimage = new BitmapImage();
						bitmapimage.BeginInit();
						bitmapimage.StreamSource = memory;
						bitmapimage.CacheOption = BitmapCacheOption.OnLoad;
						bitmapimage.EndInit();

						/*imFractal.Source = bitmapimage;
						string fileName ="..//..//..//..//" + $@"{ Guid.NewGuid()}.png";
						BitmapEncoder encoder = new PngBitmapEncoder();
						encoder.Frames.Add(BitmapFrame.Create(bitmapimage));
						using (var fileStream = new System.IO.FileStream(fileName, System.IO.FileMode.Create))
						{
							encoder.Save(fileStream);
						}*/
					}
                    Console.WriteLine(i + " Threads " + j + ": " + result.Item2);
					//RunTimeLabel.Content = result.Item2;
				}
            }
		}
    }
}
