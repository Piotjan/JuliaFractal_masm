using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Drawing;

namespace Julia_Fractal_Application.sources
{
	class GenerateFractal
	{
		[DllImport("D:/Documents/sztudyja/Studies-projects/JuliaFractal_masm/Julia_Fractal_Application/x64/Debug/CPP.dll")]
		private static extern int colorBase(double cReal, double cImg, int X, int Y, int x, int y);

		public static Bitmap run(double cReal, double cImg)
        {
			const int X = 400;
			const int Y = 400;
			Bitmap fractal = new Bitmap(X, Y);
			for(int x = 0;x<X;++x)
            {
				for(int y=0;y<Y;++y)
                {
					int pixelColor = colorBase(cReal, cImg, X, Y, x, y);
					if(pixelColor == 100)
                    {
						fractal.SetPixel(x, y, Color.FromArgb(0, 0, 0));
                    }
                    else
                    {
						fractal.SetPixel(x, y, Color.FromArgb((byte)(pixelColor * 16), (byte)(pixelColor * 8), (byte)(pixelColor * 8)));
                    }
				}
            }
			return fractal;
        }
	}
}
