#include "Module.h"

Module* Module::instance = 0;

Module* Module::GetInstance()
{
	if (instance == 0)
	{
		instance = new Module();
	}

	return instance;
}

Module::Module()
{
	HMODULE module = ::GetModuleHandle(nullptr);
	MODULEINFO moduleInfo = {};
	GetModuleInformation(::GetCurrentProcess(), module, &moduleInfo, sizeof(moduleInfo));

	baseAddress = reinterpret_cast<DWORD>(moduleInfo.lpBaseOfDll);
	moduleSize = moduleInfo.SizeOfImage;
}


bool Module::Match(const byte* data, const char* signature, const char* mask) const
{
	for (unsigned int i = 0; i < strlen(mask); i++)
	{
		if (mask[i] == 'x' && data[i] != static_cast<byte>(signature[i]))
		{
			return false;
		}
	}
	return true;
}

byte* Module::FindPattern(const char* signature, const char* mask) const
{
	for (unsigned int i = 0; i < moduleSize; i++)
	{
		byte* data = &reinterpret_cast<byte*>(baseAddress)[i];
		if (Module::Match(data, signature, mask))
		{
			return data;
		}
	}

	return nullptr;
}