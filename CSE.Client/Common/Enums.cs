using System;
using System.Collections.Generic;
using System.Text;

namespace CSE.Client.Common
{
    public static class Enums
    {
        public enum LookupLoaderType
        {
            EmailAddressType = 1
        }

        public enum SearchType
        {
            Basic = 1,
            Detail = 2,
        }

        public enum SearchOrder
        {
            ProfileId = 1,
            EmailPersonalPrimary = 2,
            EmailPrevious = 3
        }

        public enum AddressType
        {
            Work = 1,
            Home = 2,
            Previous = 5,
            Other = 9
        }

        public enum PhoneType
        {
            Cell = 1,
            Work = 2,
            Home = 3,
            Previous = 5,
            Other = 9
        }

        public enum NameType
        {
            Personal = 1,
            Nickname = 2,
            Alias = 3,
            Previous = 5,
        }

        public enum EmailType
        {
            PersonalPrimary = 1,
            Work = 2,
            Previous = 5,
            Other = 9
        }
        public enum Status
        {
            New = 1,
            Active = 2,
            Inactive = 3,
            Deleted = 4
        }

        public enum Visibility
        {
            HideItem = 0,
            ShowItem = 1,
            HideFromGroup = 2,
            ShowFromGroup = 3
        }

        public enum ProfileType
        {
            User = 1,
            Consumer = 2,
            Producer = 3
        }

        public enum PrimarySearchType
        {
            Email = 0
        }
    }
}
