#pragma once
typedef void(__stdcall* UnmanagedLoggerCallback)(const char* log);

class Logger
{
private:
    UnmanagedLoggerCallback callback;
    static Logger* instance;
public:
    void Log(const char* message);
    void SetCallback(UnmanagedLoggerCallback callback);
    static Logger* GetInstance();
};

