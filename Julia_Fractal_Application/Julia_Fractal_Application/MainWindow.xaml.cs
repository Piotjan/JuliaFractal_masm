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
			//threads = Convert.ToInt32(Environment.ProcessorCount.ToString());
			//ThreadsLabel.Text = Environment.ProcessorCount.ToString();
			threads = 4;


		}

		private void Button_Click(object sender, RoutedEventArgs e)
        {
			Tuple<Bitmap, double> result = GenerateFractal.run(Convert.ToDouble(tbReal.Text), Convert.ToDouble(tbImg.Text), threads, dllParam);
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

				imFractal.Source = bitmapimage;
			}
			RunTimeLabel.Content = result.Item2;
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
			//ThreadsLabel.Text = "Threads: ";
        }
    }
}
