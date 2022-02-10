#include "CPP.h"
#include "pch.h"
#include <complex>

int colorBaseCpp(const double cReal, const double cImg, const double X, const double Y, const double x, const double y)
{
	const std::complex<double> c(cReal, cImg);
	std::complex<double> z((x - X / 2) * 3.529411764705 / X, (y - Y / 2) / (0.425 * Y));
	int i = 0;
	while (z.real() * z.real() + z.imag() * z.imag() < 4 && i < 100)
	{
		z = std::pow(z, 2.0) + c;
		++i;
	}

	return i;
}
