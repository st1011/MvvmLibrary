using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;

namespace MvvmLibrary.Collection
{
    // https://peteohanlon.wordpress.com/2008/10/22/bulk-loading-in-observablecollection/
    // http://artfulplace.hatenablog.com/entry/2016/12/29/133950

    public partial class RangeObservableCollection<T> : ObservableCollection<T>
    {
        public bool SuppressNotification { get; private set; } = false;

        public RangeObservableCollection()
            : base() { }

        public RangeObservableCollection(IEnumerable<T> collection)
            : base(collection) { }

        public RangeObservableCollection(List<T> list)
            : base(list) { }

        protected override void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
        {
            if (SuppressNotification) { return; }

            base.OnCollectionChanged(e);
        }

        public void AddRange(IEnumerable<T> collection)
        {
            if (collection == null) { throw new ArgumentNullException(nameof(collection)); }

            using (CreateSupressManager())
            {
                foreach (var item in collection)
                {
                    Add(item);
                }
            }
        }

        protected override void ClearItems()
        {
            if (!this.Any()) { return; }

            using (CreateSupressManager())
            {
                base.ClearItems();
            }
        }

        /// <summary>
        /// 変更通知を抑制するためのマネージャーを取得
        /// </summary>
        /// <returns></returns>
        public RangeObservableCollectionManager CreateSupressManager()
        {
            return new RangeObservableCollectionManager(this);
        }
    }
}
