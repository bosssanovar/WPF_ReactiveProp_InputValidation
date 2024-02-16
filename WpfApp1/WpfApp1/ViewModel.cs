using Reactive.Bindings;
using System.ComponentModel;

namespace WpfApp1
{
    public partial class MainWindow : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        private Model _model;

        public ReactiveProperty<string> Text { get; }
        public ReactiveProperty<int> Number { get; }

        public AsyncReactiveCommand InitCommand { get; }
        public AsyncReactiveCommand ShowCommand { get; }
    }
}
