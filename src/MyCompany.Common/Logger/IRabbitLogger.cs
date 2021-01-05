using System.Threading.Tasks;

namespace MyCompany.Common.Logger
{
    public interface IRabbitLogger
    {
        Task LogAsync(LogMessage message);
    }
}