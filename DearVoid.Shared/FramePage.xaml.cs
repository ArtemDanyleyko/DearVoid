using DearVoid.NavigationParams;
using System;
using System.Collections.Generic;
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

            var navigationParams = e.Parameter as DummyPageNavigationParams;
            var page = navigationParams.Page as Page;

            AssignActions(
                page,
                navigationParams.ButtonsActions,
                navigationParams.Index);

            Frame.Content = page;
        }

        private void AssignActions(
            Page page,
            Dictionary<string, Action<int>> buttonsActions,
            int index)
        {
            foreach (var kv in buttonsActions)
            {
                var button = page.FindName($"{kv.Key}") as RadioButton;
                if (button is null)
                {
                    continue;
                }

                button.Checked += (o, e) =>
                {
                    kv.Value?.Invoke(index);
                };
            }
        }
    }
}
