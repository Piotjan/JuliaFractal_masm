#include "ComplexNumbWorker.h"

ComplexNumbWorker::ComplexNumbWorker(std::pair<float, float> _c) :
	c(_c)
{
	sequence.push_back(_c);
}

void ComplexNumbWorker::createSequence(int repeats)
{
	for (int i = 0; i < repeats; ++i)
	{
		std::pair<float, float> lastOne = sequence[sequence.size() - 1];
		float real = pow(lastOne.first, 2) - pow(lastOne.second, 2);
		float imaginary = 2 * lastOne.first * lastOne.second;
		std::pair<float, float> toPush;
		toPush.first = lastOne.first + c.first;
		toPush.second = lastOne.second + c.second;
		sequence.push_back(toPush);
	}
}

std::vector<std::pair<float, float>> ComplexNumbWorker::getSequence()
{
	return sequence;
}

void ComplexNumbWorker::changeForDegrees()
{
	for (int i = 0; i < sequence.size(); ++i)
	{
		std::pair<float, float> toPush;
		toPush.first = sqrt(pow(sequence[i].first, 2) + pow(sequence[i].second, 2));
		toPush.second = std::asin(sequence[i].second / toPush.first);
		sequence.push_back(toPush);
	}
}
