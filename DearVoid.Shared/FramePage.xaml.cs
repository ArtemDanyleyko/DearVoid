using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace DearVoid
{
    public sealed partial class FramePage : Page
    {
        public FramePage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            var page = e.Parameter as Page;

            //AssignActions(page, navigationParams.ButtonsActions);

            Frame.Content = page;
        }

        //private void AssignActions(Page page, Dictionary<string, Action> buttonsActions)
        //{
        //    foreach (var kv in buttonsActions)
        //    {
        //        var button = page.FindName($"{kv.Key}{nameof(Button)}") as Button;
        //        if (button is null)
        //        {
        //            continue;
        //        }

        //        button.Click += (o, e) =>
        //        {
        //            kv.Value?.Invoke();
        //        };
        //    }
        //}
    }
}
