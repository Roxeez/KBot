#pragma once
#include <Windows.h>

class Character
{
private:
	static Character* instance;

	DWORD walkObj;
	DWORD walkFunction;

    DWORD petWalkObj;
    DWORD petWalkFunction;

	Character();
public:
	void Walk(short x, short y);
    void PetWalk(short x, short y);
	static Character* GetInstance();
};

