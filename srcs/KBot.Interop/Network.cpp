#include "Network.h"

Network* Network::instance = 0;

Network::Network()
{
	_recv = Module::GetInstance()->FindPattern<DWORD>("\x55\x8B\xEC\x83\xC4\xF4\x53\x56\x57\x33\xC9\x89\x4D\xF4\x89\x55\xFC\x8B\xD8\x8B\x45\xFC\xE8\x00\x00\x00\x00\x33\xC0", "xxxxxxxxxxxxxxxxxxxxxxx????xx", 0);
	_send = Module::GetInstance()->FindPattern<DWORD>("\x53\x56\x8B\xF2\x8B\xD8\xEB\x04", "xxxxxxxx", 0);
	_caller = Module::GetInstance()->FindPattern<DWORD>("\xA1\x00\x00\x00\x00\x8B\x00\xBA\x00\x00\x00\x00\xE8\x00\x00\x00\x00\xE9\x00\x00\x00\x00\xA1\x00\x00\x00\x00\x8B\x00\x8B\x40\x40", "x????xxx????x????x????x????xxxxx", 0);

    _caller = *reinterpret_cast<DWORD*>(_caller + 1);
}

void Network::OnPacketReceived()
{
	const char* packet = nullptr;

	__asm
	{
		pushad
		pushfd

		mov packet, edx
	}

    Network::GetInstance()->recvCallback(packet);

	__asm
	{
		popfd
		popad
	}


	Network::GetInstance()->RecvPacket(packet);
}

void Network::OnPacketSend()
{
	const char* packet = nullptr;

	__asm
	{
		pushad
		pushfd

		mov packet, edx
	}

    Network::GetInstance()->sendCallback(packet);

	__asm
	{
		popfd
		popad
	}

	Network::GetInstance()->SendPacket(packet);
}

void Network::Attach()
{
	DetourTransactionBegin();
	DetourUpdateThread(GetCurrentThread());
	DetourAttach(&reinterpret_cast<void*&>(_send), OnPacketSend);
	DetourAttach(&reinterpret_cast<void*&>(_recv), OnPacketReceived);
	DetourTransactionCommit();
}

void Network::Detach()
{
	DetourTransactionBegin();
	DetourUpdateThread(GetCurrentThread());
	DetourDetach(&reinterpret_cast<void*&>(_send), OnPacketSend);
	DetourDetach(&reinterpret_cast<void*&>(_recv), OnPacketReceived);
	DetourTransactionCommit();
}

void Network::SendPacket(const char* packet)
{
	__asm
	{
        mov esi, this
		mov eax, [esi]._caller
		mov eax, dword ptr ds : [eax]
		mov eax, dword ptr ds : [eax]
		mov edx, packet
		call[esi]._send
	}
}

void Network::RecvPacket(const char* packet)
{
	__asm
	{
        mov esi, this
		mov eax, [esi]._caller
		mov eax, dword ptr ds : [eax]
		mov eax, dword ptr ds : [eax]
		mov eax, dword ptr ds : [eax + 34h]
		mov edx, packet
		call[esi]._recv
	}
}

void Network::SetSendCallback(PacketCallback callback)
{
	sendCallback = callback;
}

void Network::SetRecvCallback(PacketCallback callback)
{
	recvCallback = callback;
}

Network* Network::GetInstance()
{
	if (instance == 0)
	{
		instance = new Network();
	}

	return instance;
}