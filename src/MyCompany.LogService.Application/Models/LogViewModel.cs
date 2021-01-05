using System.Runtime.Serialization;

namespace MyCompany.LogService.Application.Models
{
    [DataContract(Name ="Log")]
    public class LogViewModel
    {
        public int LogId { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string ActionPerformed { get; set; }
    }
}
