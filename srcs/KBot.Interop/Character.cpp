#include "Character.h"
#include "Module.h"
#include "Logger.h"

#include <iostream>

Character* Character::instance = 0;

Character::Character()
{
	walkObj = *Module::GetInstance()->FindPattern<DWORD*>("\x33\xC9\x8B\x55\xFC\xA1\x00\x00\x00\x00\xE8\x00\x00\x00\x00", "xxxxxx????x????", 6);
	walkFunction = Module::GetInstance()->FindPattern<DWORD>("\x55\x8B\xEC\x83\xC4\xEC\x53\x56\x57\x66\x89\x4D\xFA", "xxxxxxxxxxxxx", 0);

    petWalkObj = ***Module::GetInstance()->FindPattern<DWORD***>("\x50\xA1\x00\x00\x00\x00\x8B\x00\x8B\x40\x20\x66\x8B\x4D\xF6", "xx????xxxxxxxxx", 2);
    petWalkFunction = Module::GetInstance()->FindPattern<DWORD>("\x55\x8b\xEC\x83\xC4\x00\x53\x56\x57\x8B\xF9\x89\x55\x00\x8B\xD8\xC6\x45", "xxxxx?xxxxxxx?xxxx", 0);
}


void Character::Walk(short x, short y)
{
    DWORD position = (y << 16) | x;

    DWORD obj = this->walkObj;
    DWORD function = this->walkFunction;

    __asm
    {
        push 1
        xor ecx, ecx
        mov edx, position
        mov eax, dword ptr ds : [obj]
        mov eax, dword ptr ds : [eax]
        call function
    }
}

void Character::PetWalk(short x, short y)
{
    DWORD position = (y << 16) | x;

    DWORD obj = *(DWORD*)(this->petWalkObj + 0x3C);
    DWORD function = this->petWalkFunction;

    if (obj == 0)
    {
        return;
    }

    _asm
    {
        push 1
        xor ecx, ecx
        mov edx, position
        mov eax, obj
        call function;
    }
}

Character* Character::GetInstance()
{
    if (instance == 0)
    {
        instance = new Character();
    }

    return instance;
}