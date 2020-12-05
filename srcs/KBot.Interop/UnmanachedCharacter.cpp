#include "UnmanagedCharacter.h"
#include "Module.h"

UnmanagedCharacter* UnmanagedCharacter::instance = 0;

UnmanagedCharacter::UnmanagedCharacter()
{
	walkObj = *(DWORD*)(Module::GetInstance()->FindPattern<DWORD>("\x33\xC9\x8B\x55\xFC\xA1\x00\x00\x00\x00\xE8\x00\x00\x00\x00", "xxxxxx????x????") + 0x6);
	walkFunction = Module::GetInstance()->FindPattern<DWORD>("\x55\x8B\xEC\x83\xC4\xEC\x53\x56\x57\x66\x89\x4D\xFA", "xxxxxxxxxxxxx");
}

void UnmanagedCharacter::Walk(short x, short y)
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

UnmanagedCharacter* UnmanagedCharacter::GetInstance()
{
    if (instance == 0)
    {
        instance = new UnmanagedCharacter();
    }

    return instance;
}