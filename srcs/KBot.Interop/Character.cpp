#include "Character.h"
#include "Module.h"
#include "Logger.h"

#include <iostream>

Character* Character::instance = 0;

Character::Character()
{
	object = *Module::GetInstance()->FindPattern<DWORD*>("\x33\xC9\x8B\x55\xFC\xA1\x00\x00\x00\x00\xE8\x00\x00\x00\x00", "xxxxxx????x????", 6);
	function = Module::GetInstance()->FindPattern<DWORD>("\x55\x8B\xEC\x83\xC4\xEC\x53\x56\x57\x66\x89\x4D\xFA", "xxxxxxxxxxxxx", 0);

    positionObject = *Module::GetInstance()->FindPattern<DWORD**>("\xa1\x00\x00\x00\x00\xe8\x00\x00\x00\x00\x33\xd2\xa1\x00\x00\x00\x00\xe8\x00\x00\x00\x00\x33\xd2\xa1\x00\x00\x00\x00\xe8\x00\x00\x00\x00\x33\xd2\xa1\x00\x00\x00\x00\xe8\x00\x00\x00\x00\xa1", "x????x????xxx????x????xxx????x????xxx????x????x", 1);
}

void Character::Walk(short x, short y)
{
    DWORD position = (y << 16) | x;

    DWORD obj = this->object;
    DWORD function = this->function;

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

short Character::GetPositionX()
{
    return *(short*)((*positionObject + 0x63C));
}

short Character::GetPositionY()
{
    return *(short*)((*positionObject + 0x63E));
}