#include "UnmanagedNetwork.h"

UnmanagedNetwork* UnmanagedNetwork::instance = 0;

UnmanagedNetwork::UnmanagedNetwork()
{
	recv = Module::GetInstance()->FindPattern<DWORD>("\x55\x8B\xEC\x83\xC4\xF4\x53\x56\x57\x33\xC9\x89\x4D\xF4\x89\x55\xFC\x8B\xD8\x8B\x45\xFC\xE8\x00\x00\x00\x00\x33\xC0", "xxxxxxxxxxxxxxxxxxxxxxx????xx");
	send = Module::GetInstance()->FindPattern<DWORD>("\x53\x56\x8B\xF2\x8B\xD8\xEB\x04", "xxxxxxxx");
	caller = Module::GetInstance()->FindPattern<DWORD>("\xA1\x00\x00\x00\x00\x8B\x00\xBA\x00\x00\x00\x00\xE8\x00\x00\x00\x00\xE9\x00\x00\x00\x00\xA1\x00\x00\x00\x00\x8B\x00\x8B\x40\x40", "x????xxx????x????x????x????xxxxx");

	caller = *reinterpret_cast<DWORD*>(caller + 1);
}

void UnmanagedNetwork::OnPacketReceived()
{
	const char* packet = nullptr;

	__asm
	{
		pushad
		pushfd

		mov packet, edx
	}

	__asm
	{
		popfd
		popad
	}


	UnmanagedNetwork::GetInstance()->RecvPacket(packet);
}

void UnmanagedNetwork::OnPacketSend()
{
	const char* packet = nullptr;

	__asm
	{
		pushad
		pushfd

		mov packet, edx
	}

	__asm
	{
		popfd
		popad
	}

	UnmanagedNetwork::GetInstance()->SendPacket(packet);
}

void UnmanagedNetwork::Attach()
{
	DetourTransactionBegin();
	DetourUpdateThread(GetCurrentThread());
	DetourAttach(&reinterpret_cast<void*&>(send), OnPacketSend);
	DetourAttach(&reinterpret_cast<void*&>(recv), OnPacketReceived);
	DetourTransactionCommit();
}

void UnmanagedNetwork::Detach()
{
	DetourTransactionBegin();
	DetourUpdateThread(GetCurrentThread());
	DetourDetach(&reinterpret_cast<void*&>(send), OnPacketSend);
	DetourDetach(&reinterpret_cast<void*&>(recv), OnPacketReceived);
	DetourTransactionCommit();
}

void UnmanagedNetwork::SendPacket(const char* packet)
{
	DWORD caller = this->caller;
	DWORD send = this->send;

	__asm
	{
		mov eax, caller
		mov eax, dword ptr ds : [eax]
		mov eax, dword ptr ds : [eax]
		mov edx, packet
		call send
	}
	
    UnmanagedNetwork::GetInstance()->sendCallback(packet);
}

void UnmanagedNetwork::RecvPacket(const char* packet)
{
	DWORD caller = this->caller;
	DWORD recv = this->recv;

	__asm
	{
		mov eax, caller
		mov eax, dword ptr ds : [eax]
		mov eax, dword ptr ds : [eax]
		mov eax, dword ptr ds : [eax + 34h]
		mov edx, packet
		call recv
	}
	
	UnmanagedNetwork::GetInstance()->recvCallback(packet);
}

void UnmanagedNetwork::SetSendCallback(PacketCallback callback)
{
	sendCallback = callback;
}

void UnmanagedNetwork::SetRecvCallback(PacketCallback callback)
{
	recvCallback = callback;
}

UnmanagedNetwork* UnmanagedNetwork::GetInstance()
{
	if (instance == 0)
	{
		instance = new UnmanagedNetwork();
	}

	return instance;
}