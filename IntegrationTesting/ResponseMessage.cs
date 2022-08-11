using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationTesting
{
    internal class ResponseMessage
    {
        public string Result { get; set; }

        public string Message { get; set; }

        public int Status { get; set; }
    }

    public class User
    {
        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public string UserLastName { get; set; }
        public string UserPassword { get; set; }
        public DateTime DateTimeStamp { get; set; }
    }
}
