#pragma once
#include <Windows.h>
#include <detours.h>

#include "Module.h"

typedef void(__stdcall* PacketCallback)(const char* packet);

class Network
{
private:
	static Network* instance;

	DWORD _recv;
	DWORD _send;
	DWORD _caller;

	PacketCallback sendCallback;
	PacketCallback recvCallback;

	Network();

	static void OnPacketReceived();

	static void OnPacketSend();

public:
	void Attach();

	void Detach();

	void SendPacket(const char* packet);

	void RecvPacket(const char* packet);

	void SetSendCallback(PacketCallback callback);

	void SetRecvCallback(PacketCallback callback);

	static Network* GetInstance();
};
