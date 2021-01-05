namespace MyCompany.Common.Logger
{
    public interface IRabbitConfig
    {
         string HostName { get; set; }
         int Port { get; set; }
         string UserName { get; set; }
         string Password { get; set; }

    }
}