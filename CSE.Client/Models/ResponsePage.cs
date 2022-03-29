using CSE.Client.Enums;
using System.Collections.Generic;

namespace CSE.Client.Models
{
    public class ResponsePage
    {
        private IList<ResponsePage> list;

        public ResponsePage(
            IList<ResponsePage> pages,
            string xamlPage,
            PageStatus pageStatus)
        {
            this.list = pages;
            XamlPage = xamlPage;
            PageStatus = pageStatus;
        }

        public int Index => list.IndexOf(this);

        public string XamlPage { get; set; }

        public PageStatus PageStatus { get; set; }
    }
}
