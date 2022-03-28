using System;
using System.Collections.Generic;
using System.Text;

namespace CSE.Client.DTO.Common
{
    public class ClientInfoRC
    {
        public string Info { get; set; }
        public InfoType Type { get; set; }
    }

    public class ClientSession
    {
        public Guid ApplicationUniqueID { get; set; }
        public Guid SessionID { get; set; }
        public Guid PickupRequestID { get; set; }
    }

    public enum InfoType
    {
        Information = 1,
        Warning = 2,
        Error = 3,
        ToUserWarning = 8,
        ToUserError = 9,
        ToUserSuccess = 10,
    };
}
