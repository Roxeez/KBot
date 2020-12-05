#include "NetworkBridge.h"
#include "UnmanagedNetwork.h"

namespace KBot
{
	namespace Interop
	{
		NetworkBridge::NetworkBridge()
		{
			UnmanagedNetwork::GetInstance()->Attach();
		}

		void NetworkBridge::SendPacket(String^ packet)
		{
			UnmanagedNetwork::GetInstance()->SendPacket(NostaleString(ConvertToCharArray(packet)).ToString());
		}

		void NetworkBridge::RecvPacket(String^ packet)
		{
			UnmanagedNetwork::GetInstance()->RecvPacket(NostaleString(ConvertToCharArray(packet)).ToString());
		}

		void NetworkBridge::AddSendCallback(NetworkCallback^ callback)
		{
			UnmanagedNetwork::GetInstance()->SetSendCallback(static_cast<PacketCallback>(Marshal::GetFunctionPointerForDelegate(callback).ToPointer()));
		}

		void NetworkBridge::AddRecvCallback(NetworkCallback^ callback)
		{
			UnmanagedNetwork::GetInstance()->SetRecvCallback(static_cast<PacketCallback>(Marshal::GetFunctionPointerForDelegate(callback).ToPointer()));
		}

		NetworkBridge::~NetworkBridge()
		{
			UnmanagedNetwork::GetInstance()->Detach();
		}
	};
};