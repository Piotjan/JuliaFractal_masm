using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Drawing;
using System.Threading;

namespace Julia_Fractal_Application.sources
{
	class GenerateFractal
	{
		private class TaskParams
			{
				public double cReral;
				public double cImg;
				public double X;
				public double Y;
				public double x;
				public double y;
			}


		[DllImport("D:/Documents/sztudyja/Studies-projects/JuliaFractal_masm/Julia_Fractal_Application/x64/Debug/MASM.dll")]
		private static extern int colorBaseAsm(double cReal, double cImg, double X, double Y, double x, double y);

		[DllImport("D:/Documents/sztudyja/Studies-projects/JuliaFractal_masm/Julia_Fractal_Application/x64/Debug/CPP.dll")]
		private static extern int colorBaseCpp(double cReal, double cImg, double X, double Y, double x, double y);

		public static Tuple<Bitmap, double> run(double cReal, double cImg, int threads, int whichDll)
        {
			const int X = 400;
			const int Y = 400;
			Bitmap fractal = new Bitmap(X, Y);
			var watch = System.Diagnostics.Stopwatch.StartNew();
			var countdownEvent = new CountdownEvent(X * Y);
			Action<Object> threadFunctionWrapper = null;
			if(whichDll==0)
            {
				threadFunctionWrapper = threadParams =>
				{
					TaskParams castedParams = (TaskParams)threadParams;

					countdownEvent.Signal();
				};
            }

			//if (whichDll==0)
   //         {
			//	Parallel.For(0, X, new ParallelOptions { MaxDegreeOfParallelism = 1 }, (x) =>
			//	{
			//		for (int y = 0; y < Y; ++y)
			//		{
			//			int pixelColor = colorBaseAsm(cReal, cImg, X, Y, x, y);
			//			if (pixelColor == 100)
			//			{
			//				fractal.SetPixel(x, y, Color.FromArgb(0, 0, 0));
			//			}
			//			else
			//			{
			//				fractal.SetPixel(x, y, Color.FromArgb((byte)(pixelColor * 16), (byte)(pixelColor * 8), (byte)(pixelColor * 8)));
			//			}
			//		}
			//	});
			//}
			//else if(whichDll==1)
   //         {
			//	Parallel.For(0, X, new ParallelOptions { MaxDegreeOfParallelism = threads }, (x) =>
			//	{
			//		for (int y = 0; y < Y; ++y)
			//		{
			//			int pixelColor = colorBaseCpp(cReal, cImg, X, Y, x, y);
			//			if (pixelColor == 100)
			//			{
			//				fractal.SetPixel(x, y, Color.FromArgb(0, 0, 0));
			//			}
			//			else
			//			{
			//				fractal.SetPixel(x, y, Color.FromArgb((byte)(pixelColor * 16), (byte)(pixelColor * 8), (byte)(pixelColor * 8)));
			//			}
			//		}
			//	});
			//}
			watch.Stop();

			return new Tuple<Bitmap, double>(fractal, watch.ElapsedMilliseconds);
        }
	}
}
