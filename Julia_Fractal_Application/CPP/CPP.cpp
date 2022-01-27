#include "CPP.h"
#include "pch.h"
#include <complex>

int colorBase(const double cReal, const double cImg, const double X, const double Y, const double x, const double y)
{
	const std::complex<double> c(cReal, cImg);
	std::complex<double> z(1.5 * (x - X / 2) / (0.5 * X * 0.85),
		1.0 * (y - Y / 2) / (0.5 * Y * 0.85));
	int i = 0;
	while (z.real() * z.real() + z.imag() * z.imag() < 4 && i < 100)
	{
		z = std::pow(z, 2.0) + c;
		++i;
	}

	return i;
}
