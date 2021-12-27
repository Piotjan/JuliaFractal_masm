#pragma once
#include<vector>
#include<math.h>
#include<cmath>
#include<pch.h>

class ComplexNumbWorker
{
	std::pair<float, float> c;
	std::vector <std::pair<float, float>> sequence;
	std::vector<std::pair<float, float>> trigonometric;
public:
	ComplexNumbWorker(std::pair<float, float> _c);
	void createSequence(int repeats);
	std::vector<std::pair<float, float>> getSequence();
private:
	void changeForDegrees();
};
