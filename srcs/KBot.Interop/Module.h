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
	byte* FindPattern(const char* signature, const char* mask) const;
public:
	static Module* GetInstance();

	template <typename T> T FindPattern(const char* signature, const char* mask) const
	{
		return (T)FindPattern(signature, mask);
	}
};