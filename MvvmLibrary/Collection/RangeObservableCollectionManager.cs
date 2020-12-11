using System;
using System.Collections.Specialized;

namespace MvvmLibrary.Collection
{

    public partial class RangeObservableCollection<T>
    {
        /// <summary>
        /// 変更通知を抑制するためのマネージャー
        /// </summary>
        public sealed class RangeObservableCollectionManager : IDisposable
        {
            internal RangeObservableCollectionManager(RangeObservableCollection<T> rangeObservableCollection)
            {
                _rangeObservableCollection = rangeObservableCollection;
                _rangeObservableCollection.SuppressNotification = true;
            }

            private bool disposedValue;
            private readonly RangeObservableCollection<T> _rangeObservableCollection;

            private void Dispose(bool disposing)
            {
                if (!disposedValue)
                {
                    if (disposing)
                    {
                        // TODO: マネージド状態を破棄します (マネージド オブジェクト)
                        _rangeObservableCollection.SuppressNotification = false;

                        // Reset: 大幅な変更があった
                        _rangeObservableCollection.OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
                    }

                    // TODO: アンマネージド リソース (アンマネージド オブジェクト) を解放し、ファイナライザーをオーバーライドします
                    // TODO: 大きなフィールドを null に設定します
                    disposedValue = true;
                }
            }

            // // TODO: 'Dispose(bool disposing)' にアンマネージド リソースを解放するコードが含まれる場合にのみ、ファイナライザーをオーバーライドします
            // ~RangeObservableCollectionManager()
            // {
            //     // このコードを変更しないでください。クリーンアップ コードを 'Dispose(bool disposing)' メソッドに記述します
            //     Dispose(disposing: false);
            // }

            public void Dispose()
            {
                // このコードを変更しないでください。クリーンアップ コードを 'Dispose(bool disposing)' メソッドに記述します
                Dispose(disposing: true);
                GC.SuppressFinalize(this);
            }
        }
    }
}
