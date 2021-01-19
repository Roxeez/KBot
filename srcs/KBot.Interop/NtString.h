#pragma once
#include <string>
#include <vector>

class NtString
{
public:
	NtString();
	NtString(const std::string& input);
	NtString(const char* input);

	NtString& operator+=(const std::string& input);
	NtString& operator+=(const char* input);

	unsigned int Length() const;
	const char* ToString() const;

private:
	std::vector<char> _buffer;

	void Append(const std::string& input);
};