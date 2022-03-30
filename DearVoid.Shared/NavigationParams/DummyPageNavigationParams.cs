using System;
using System.Collections.Generic;

namespace DearVoid.NavigationParams
{
    public class DummyPageNavigationParams
    {
        public DummyPageNavigationParams(
            object page,
            Dictionary<string, Action<int>> buttonsActions,
            int index)
        {
            Page = page;
            ButtonsActions = buttonsActions;
            Index = index;
        }

        public object Page { get; }

        public Dictionary<string, Action<int>> ButtonsActions { get; }

        public int Index { get; }
    }
}
