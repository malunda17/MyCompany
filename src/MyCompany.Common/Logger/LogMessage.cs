using System;

namespace MyCompany.Common.Logger
{
    public class LogMessage
    {
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
        public int UserId { get; private set; }
        public string UserName { get;private  set; }
        public string ActionPerformed { get; private set; }

        public LogMessage(int userId,string userName,string actionPerformed)
        {
            UserId = userId;
            UserName = userName;
            ActionPerformed = actionPerformed;
        }
    }
}