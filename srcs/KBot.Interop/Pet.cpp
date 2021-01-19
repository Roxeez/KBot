#include "Pet.h"
#include "Module.h"

#include <iostream>

Pet* Pet::instance = 0;

Pet::Pet()
{
    function = Module::GetInstance()->FindPattern<DWORD>("\x55\x8b\xEC\x83\xC4\x00\x53\x56\x57\x8B\xF9\x89\x55\x00\x8B\xD8\xC6\x45", "xxxxx?xxxxxxx?xxxx", 0);
    base = *Module::GetInstance()->FindPattern<DWORD*>("\x8B\xF8\x8B\xD3\xA1\x00\x00\x00\x00\xE8\x00\x00\x00\x00\x8B\xD0", "xxxxx????x????xx", 5);
}

void Pet::Walk(short x, short y)
{
    DWORD position = (y << 16) | x;

    DWORD function = this->function;
    DWORD address = *(DWORD*)(*(DWORD*)base + 0x4);

    int size = *(DWORD*)(*(DWORD*)base + 0x8);
    if (size == 0)
    {
        return;
    }

    if (size == 2)
    {
        address += 0x4;
    }

    DWORD object = *(DWORD*)address;

    __asm
    {
        push 1
        push 1
        xor ecx, ecx
        mov edx, position
        mov eax, object
        call function
    }
}