#pragma once
#include <Windows.h>
#include <Psapi.h>

class Module
{
private:
	static Module* instance;

	DWORD baseAddress;
	DWORD moduleSize;

	Module();

	bool Match(const byte* data, const char* signature, const char* mask) const;
	byte* FindPattern(const char* signature, const char* mask, int offset) const;
public:
	static Module* GetInstance();
    DWORD GetBaseAddress();
	template <typename T> T FindPattern(const char* signature, const char* mask, int offset) const
	{
		return (T)FindPattern(signature, mask, offset);
	}
};