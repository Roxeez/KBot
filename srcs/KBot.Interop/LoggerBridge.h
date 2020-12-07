#pragma once
namespace KBot
{
    namespace Interop
    {
        using namespace System;
        using namespace System::Runtime::InteropServices;

        public delegate void LoggerCallback(String^ packet);

        public ref class LoggerBridge
        {
        private:
            static const char* ConvertToCharArray(String^ str)
            {
                return (const char*)(Marshal::StringToHGlobalAnsi(str).ToPointer());
            }
        public:
            void SetCallback(LoggerCallback^ callback);
        };
    };
};

