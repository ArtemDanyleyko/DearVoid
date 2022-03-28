using System;
using System.Collections.Generic;

namespace DearVoid.NavigationParams
{
    public class DummyPageNavigationParams
    {
        public DummyPageNavigationParams(object page, Dictionary<string, Action> buttonsActions)
        {
            Page = page;
            ButtonsActions = buttonsActions;
        }

        public object Page { get; }

        public Dictionary<string, Action> ButtonsActions { get; }
    }
}
