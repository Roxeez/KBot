#pragma once
#include <Windows.h>

class Pet
{
private:
    Pet();
    static Pet* instance;

    DWORD function;
    DWORD base;

public:
    void Walk(short x, short y);
    static Pet* GetInstance()
    {
        if (instance == 0)
        {
            instance = new Pet();
        }

        return instance;
    }
};

