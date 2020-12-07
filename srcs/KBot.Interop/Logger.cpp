#include "Logger.h"

Logger* Logger::instance = 0;

Logger* Logger::GetInstance()
{
    if (instance == 0)
    {
        instance = new Logger();
    }

    return instance;
}

void Logger::Log(const char* log)
{
    callback(log);
}

void Logger::SetCallback(UnmanagedLoggerCallback callback)
{
    this->callback = callback;
}