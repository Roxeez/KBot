#pragma once
#include <Windows.h>

class Character
{
private:
    Character();
    static Character* instance;

	DWORD object;
	DWORD function;
    DWORD* positionObject;

public:
	void Walk(short x, short y);

    static Character* GetInstance()
    {
        if (instance == 0)
        {
            instance = new Character();
        }

        return instance;
    }

    short GetPositionX();
    short GetPositionY();
};

