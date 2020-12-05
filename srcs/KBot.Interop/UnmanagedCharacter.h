#pragma once
#include <Windows.h>

class UnmanagedCharacter
{
private:
	static UnmanagedCharacter* instance;

	DWORD walkObj;
	DWORD walkFunction;

	UnmanagedCharacter();
public:
	void Walk(short x, short y);
	static UnmanagedCharacter* GetInstance();
};

