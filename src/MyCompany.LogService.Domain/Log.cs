using System;

namespace MyCompany.LogService.Domain
{
    public class Log
    {
        public int LogId { get; private set; }
        public int UserId { get; private set; }
        public string UserName { get; private set; }
        public string ActionPerformed { get; private set; }
        public DateTime Timestamp { get; set; }

        public Log(int userId,string userName, string actionPerformed, DateTime timestamp)
        {
            UserId = userId;
            UserName = userName;
            ActionPerformed = actionPerformed;
            Timestamp = timestamp;
        }
    }
}