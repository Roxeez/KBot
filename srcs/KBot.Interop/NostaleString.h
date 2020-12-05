#pragma once
#include <string>
#include <vector>

class NostaleString
{
public:
	NostaleString()
	{
		buffer = { 0x00, 0x00, 0x00, 0x01, 0x00, 0x00, 0x00, 0x00 };
	}

	NostaleString(const std::string& input)
	{
		Append(input);
	}

	NostaleString(const char* input)
	{
		Append(input);
	}

	NostaleString& operator+=(const std::string& input)
	{
		Append(input);

		return *this;

	}
	NostaleString& operator+=(const char* input)
	{
		Append(input);

		return *this;
	}

	unsigned int Length() const
	{
		return buffer.size() - 8;
	}

	const char* ToString() const
	{
		return buffer.data() + 8;
	}

private:
	std::vector<char> buffer;

	void Append(const std::string& input)
	{
		for (auto c : input)
		{
			buffer.push_back(c);
		}

		*reinterpret_cast<unsigned int*>(buffer.data() + 4) = Length();
	}
};