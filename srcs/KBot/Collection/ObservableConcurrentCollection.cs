﻿using System;
 using System.Collections.Generic;
 using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Windows.Threading;
 using PropertyChanged;

 namespace KBot.Collection
{
    [SuppressPropertyChangedWarnings]
    public class ObservableConcurrentCollection<T> : ObservableCollection<T>
    {
        protected override event PropertyChangedEventHandler PropertyChanged;
        public override event NotifyCollectionChangedEventHandler CollectionChanged;

        public void AddRange(IEnumerable<T> values)
        {
            foreach (T value in values)
            {
                Add(value);
            }
        }

        public void RemoveRange(IEnumerable<T> values)
        {
            foreach (T value in values)
            {
                Remove(value);
            }
        }
        
        protected override void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
        {
            using (BlockReentrancy())
            {
                NotifyCollectionChangedEventHandler eventHandler = CollectionChanged;
                if (eventHandler == null)
                {
                    return;
                }

                Delegate[] delegates = eventHandler.GetInvocationList();
                foreach (Delegate @delegate in delegates)
                {
                    var handler = (NotifyCollectionChangedEventHandler)@delegate;
                    if (handler.Target is DispatcherObject dispatcherObject && dispatcherObject.CheckAccess() == false)
                    {
                        dispatcherObject.Dispatcher.Invoke(DispatcherPriority.DataBind, handler, this, e);
                    }
                    else
                    {
                        handler(this, e);
                    }
                }
            }
        }

        protected override void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            PropertyChangedEventHandler eventHandler = PropertyChanged;
            if (eventHandler == null)
            {
                return;
            }

            Delegate[] delegates = eventHandler.GetInvocationList();
            foreach (Delegate @delegate in delegates)
            {
                var handler = (PropertyChangedEventHandler)@delegate;
                if (handler.Target is DispatcherObject dispatcherObject && dispatcherObject.CheckAccess() == false)
                {
                    dispatcherObject.Dispatcher.Invoke(DispatcherPriority.DataBind, handler, this, e);
                }
                else
                {
                    handler(this, e);
                }
            }
        }
    }
}