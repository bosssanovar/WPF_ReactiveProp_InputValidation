using Reactive.Bindings;
using System.ComponentModel;

namespace WpfApp1
{
    public partial class MainWindow : INotifyPropertyChanged
    {
#pragma warning disable CS0067 // イベント 'MainWindow.PropertyChanged' は使用されていません
        public event PropertyChangedEventHandler? PropertyChanged;
#pragma warning restore CS0067 // イベント 'MainWindow.PropertyChanged' は使用されていません

        private readonly Model _model;

        public ReactiveProperty<string> Text { get; }
        public ReactiveProperty<int> Number { get; }

        public AsyncReactiveCommand InitCommand { get; }
        public AsyncReactiveCommand ShowCommand { get; }
    }
}
