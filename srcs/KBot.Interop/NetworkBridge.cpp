#include "NetworkBridge.h"
#include "NostaleString.h"
#include "Network.h"

namespace KBot
{
	namespace Interop
	{
		NetworkBridge::NetworkBridge()
		{
			Network::GetInstance()->Attach();
		}

		void NetworkBridge::SendPacket(String^ packet)
		{
			Network::GetInstance()->SendPacket(NostaleString(ConvertToCharArray(packet)).ToString());
		}

		void NetworkBridge::RecvPacket(String^ packet)
		{
			Network::GetInstance()->RecvPacket(NostaleString(ConvertToCharArray(packet)).ToString());
		}

		void NetworkBridge::AddSendCallback(NetworkCallback^ callback)
		{
			Network::GetInstance()->SetSendCallback(static_cast<PacketCallback>(Marshal::GetFunctionPointerForDelegate(callback).ToPointer()));
		}

		void NetworkBridge::AddRecvCallback(NetworkCallback^ callback)
		{
			Network::GetInstance()->SetRecvCallback(static_cast<PacketCallback>(Marshal::GetFunctionPointerForDelegate(callback).ToPointer()));
		}

		NetworkBridge::~NetworkBridge()
		{
			Network::GetInstance()->Detach();
		}
	};
};