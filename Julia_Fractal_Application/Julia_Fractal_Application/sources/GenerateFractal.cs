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
				public double[] colorPixel;
			}


		[DllImport("D:/Documents/sztudyja/Studies-projects/JuliaFractal_masm/Julia_Fractal_Application/x64/Debug/MASM.dll")]
		private static extern int colorBaseAsm(double cReal, double cImg, double X, double Y, double x, double y);

		[DllImport("D:/Documents/sztudyja/Studies-projects/JuliaFractal_masm/Julia_Fractal_Application/x64/Debug/CPP.dll")]
		private static extern int colorBaseCpp(double cReal, double cImg, double X, double Y, double x, double y);

		public static Tuple<Bitmap, double> run(double cReal, double cImg, int threads, int whichDll)
        {
			const int X = 5000;
			const int Y = 5000;
			Bitmap fractal = new Bitmap(X, Y);
			var countdownEvent = new CountdownEvent(X * Y);
			Action<Object> threadFunctionWrapper = null;
			if(whichDll==0)
            {
				threadFunctionWrapper = threadParams =>
				{
					TaskParams castedParams = (TaskParams)threadParams;
					int temp = (int)(castedParams.x * castedParams.Y + castedParams.y);
					castedParams.colorPixel[temp] = colorBaseAsm(castedParams.cReral,
																	castedParams.cImg,
																	castedParams.X,
																	castedParams.Y,
																	castedParams.x,
																	castedParams.y);
					countdownEvent.Signal();
				};
            }

			if (whichDll == 1)
			{
				threadFunctionWrapper = threadParams =>
				{
					TaskParams castedParams = (TaskParams)threadParams;
					int temp = (int)(castedParams.x * castedParams.Y + castedParams.y);
					castedParams.colorPixel[temp] = colorBaseCpp(castedParams.cReral,
																	castedParams.cImg,
																	castedParams.X,
																	castedParams.Y,
																	castedParams.x,
																	castedParams.y);
					countdownEvent.Signal();
				};
			}

			double[] colorPixel = new double[X * Y];
			ThreadPool.SetMinThreads(threads, threads);
			ThreadPool.SetMaxThreads(threads, threads);
			var watch = System.Diagnostics.Stopwatch.StartNew();
			for(int x=0;x<X;++x)
            {
				for(int y=0;y<Y;++y)
                {
					TaskParams taskParams = new TaskParams();
					taskParams.cReral = cReal;
					taskParams.cImg = cImg;
					taskParams.X = X;
					taskParams.Y = Y;
					taskParams.x = x;
					taskParams.y = y;
					taskParams.colorPixel = colorPixel;
					ThreadPool.QueueUserWorkItem(new WaitCallback(threadFunctionWrapper), taskParams);
                }
            }
			countdownEvent.Wait();
			watch.Stop();

			for(int x=0;x<X;++x)
            {
				for(int y=0;y<Y;++y)
                {
					int temp = (int)(x * Y + y);
					fractal.SetPixel(x, y, Color.FromArgb(255,
														(byte)(colorPixel[temp] * 16),
														(byte)(colorPixel[temp] * 8),
														(byte)(colorPixel[temp] * 8)));
				}
            }

			return new Tuple<Bitmap, double>(fractal, watch.ElapsedMilliseconds);
        }
	}
}
