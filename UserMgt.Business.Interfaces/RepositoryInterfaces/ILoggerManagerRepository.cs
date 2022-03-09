

namespace  UserMgt.Business.Interfaces.RepositoryInterfaces

{
    public interface ILoggerManagerRepository
    {
        void LogInformation(string message);
        void LogWarn(string message);
        void LogDebug(string message);
        void LogError(string message);
    }
}
