#include "LoggerBridge.h"
#include "Logger.h"

namespace KBot
{
    namespace Interop
    {
        void LoggerBridge::SetCallback(LoggerCallback^ callback)
        {
            Logger::GetInstance()->SetCallback(static_cast<UnmanagedLoggerCallback>(Marshal::GetFunctionPointerForDelegate(callback).ToPointer()));
        }
    };
};