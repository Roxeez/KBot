#pragma once
namespace KBot
{
	namespace Interop
	{
		using namespace System;
		using namespace System::Runtime::InteropServices;

		public delegate void NetworkCallback(String^ packet);

		public ref class NetworkBridge
		{
		private:
            static const char* ConvertToCharArray(String^ str)
            {
                return (const char*)(Marshal::StringToHGlobalAnsi(str).ToPointer());
            }

		public:
			NetworkBridge();
			void SendPacket(String^ packet);
			void RecvPacket(String^ packet);
			void AddSendCallback(NetworkCallback^ callback);
			void AddRecvCallback(NetworkCallback^ callback);
			~NetworkBridge();
		};
	};
};