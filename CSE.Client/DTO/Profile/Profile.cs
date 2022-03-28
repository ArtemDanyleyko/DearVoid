using CSE.Client.DTO.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace CSE.Client.DTO.Profile
{
    public class ProfileR
    {
        public IEnumerable<ProfileS> Results { get; set; }
        public List<ClientInfoRC> DataInfo { get; set; }
    }

    public class ProfileS
    {
        public ClientSession ClientSession { get; set; }
        public Guid ProfileId { get; set; }
        public int Type { get; set; }
        public int DefaultShow { get; set; }
        public List<EmailC> Emails { get; set; }
        public List<NameC> Names { get; set; }
        public List<PhoneC> Phones { get; set; }
        public List<AddressC> Addresses { get; set; }
    }

    public class AddressC
    {
        public Guid Id { get; set; }

        public string Number { get; set; }

        public string Street1 { get; set; }

        public string Street2 { get; set; }

        public string Apt { get; set; }

        public string Suite { get; set; }

        public string PoBox { get; set; }

        public string City { get; set; }
        public string County { get; set; }

        public string State { get; set; }

        public string Zip1 { get; set; }

        public string Zip2 { get; set; }

        public string Country { get; set; }

        public bool IsPrimary { get; set; }

        public int Type { get; set; }
    }

    public class EmailC
    {
        public Guid Id { get; set; }

        public string Email { get; set; }

        public int Type { get; set; }

        public bool IsPrimary { get; set; }
    }

    public class NameC
    {
        public Guid Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }
        public string Company { get; set; }

        public int Type { get; set; }

        public bool IsPrimary { get; set; }
    }

    public class PhoneC
    {
        public Guid Id { get; set; }

        public string Number { get; set; }

        public int Type { get; set; }
        public bool IsPrimary { get; set; }
    }
}
