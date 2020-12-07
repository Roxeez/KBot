using System.Collections.ObjectModel;

namespace KBot.Common.Collection
{
    public class ObservableSet<T> : ObservableCollection<T>
    {
        protected override void InsertItem(int index, T item)
        {
            if (item == null)
            {
                return;
            }
            
            if (Contains(item))
            {
                return;
            }
            
            base.InsertItem(index, item);
        }
        
        protected override void SetItem(int index, T item)
        {
            int i = IndexOf(item);
            if (i >= 0 && i != index)
            {
                return;
            }

            base.SetItem(index, item);
        }
    }
}