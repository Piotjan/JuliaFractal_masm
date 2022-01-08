using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace Julia_Fractal_Application
{
    class klasaKlask
    {
        [DllImport("D:/Documents/sztudyja/Studies-projects/JuliaFractal_masm/Julia_Fractal_Application/x64/Debug/MASM.dll")]
        public static extern int MyProc1(int a, int b);
    }
}
