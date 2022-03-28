using CSE.Client.DTO.Common;
using CSE.Client.DTO.Profile;
using System;
using System.Collections.Generic;
using System.Text;

namespace CSE.Client.DTO.User
{
    public class UserR
    {
        public IEnumerable<UserItem> Results { get; set; }
        public List<ClientInfoRC> DataInfo { get; set; }
    }

    public class UserS
    {
        public Guid UserId { get; set; }
        public ProfileS ProfileIn { get; set; }
        public string Password { get; set; }
        public string UserAlias { get; set; }
        public bool LoginPostRegister { get; set; }
    }

    public class UserItem
    {
        public Guid UserId { get; set; }
        public string LoginUserName { get; set; }
    }
}
