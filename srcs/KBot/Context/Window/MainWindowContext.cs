using System.Collections.Generic;
using System.Linq;
using PropertyChanged;

namespace KBot.Context.Window
{
    [AddINotifyPropertyChangedInterface]
    public sealed class MainWindowContext
    {
        public ITabContext GeneralTab { get; }
        public ITabContext FightTab { get; }
        public ITabContext PathTab { get; }
        public ITabContext ItemTab { get; }

        public MainWindowContext(IEnumerable<ITabContext> contexts)
        {
            GeneralTab = contexts.First(x => x.Key == TabContextKey.General);
            FightTab = contexts.First(x => x.Key == TabContextKey.Fight);
            PathTab = contexts.First(x => x.Key == TabContextKey.Path);
            ItemTab = contexts.First(x => x.Key == TabContextKey.Item);
        }
    }
}