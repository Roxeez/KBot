#include "NtString.h"

NtString::NtString()
{
    _buffer = { 0x00, 0x00, 0x00, 0x01, 0x00, 0x00, 0x00, 0x00 };
}

NtString::NtString(const std::string& input)
    : NtString()
{
    Append(input);
}

NtString::NtString(const char* input)
    : NtString()
{
    Append(input);
}

NtString& NtString::operator+=(const std::string& input)
{
    Append(input);

    return *this;
}

NtString& NtString::operator+=(const char* input)
{
    Append(input);

    return *this;
}

unsigned int NtString::Length() const
{
    return _buffer.size() - 8;
}

const char* NtString::ToString() const
{
    return _buffer.data() + 8;
}

void NtString::Append(const std::string& input)
{
    for (auto c : input)
    {
        _buffer.push_back(c);
    }

    *reinterpret_cast<unsigned int*>(_buffer.data() + 4) = Length();
}