using ClientProxy.Enums;

namespace ClientProxy.Models
{
    public class NavigationIntent
    {
        private NavigationIntent(NavigationType navigationType, string xamlPage = null)
        {
            NavigationType = navigationType;
            XamlPage = xamlPage;
        }

        public NavigationType NavigationType { get; }
        public string XamlPage { get; }

        public static NavigationIntent Forward(string xamlPage = null)
            => new NavigationIntent(NavigationType.Forward, xamlPage);

        public static NavigationIntent Backward()
            => new NavigationIntent(NavigationType.Backward);
    }
}
