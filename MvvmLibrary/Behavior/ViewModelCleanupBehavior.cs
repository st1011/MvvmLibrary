using Microsoft.Xaml.Behaviors;
using System;
using System.Windows;

namespace MvvmLibrary.Behavior
{
    /// <summary>
    /// ViewModel(DataContext)がIDisposeを実装する場合
    /// Window.ClosedでViewModelをDisposeします
    /// </summary>
    public class ViewModelCleanupBehavior : Behavior<Window>
    {
        protected override void OnAttached()
        {
            base.OnAttached();
            AssociatedObject.Closed -= OnWindowClosed;
            AssociatedObject.Closed += OnWindowClosed;
        }
        protected override void OnDetaching()
        {
            base.OnDetaching();
            AssociatedObject.Closed -= OnWindowClosed;
        }

        private void OnWindowClosed(object sender, EventArgs e)
        {
            (AssociatedObject.DataContext as IDisposable)?.Dispose();
        }
    }
}
