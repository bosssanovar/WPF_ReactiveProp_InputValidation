using Reactive.Bindings;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1
{
    public partial class MainWindow : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        private Model _model = new Model();

        public ReactiveProperty<string> Text { get; }
        public ReactiveProperty<int> Number { get; }

        public AsyncReactiveCommand InitCommand { get; }
        public AsyncReactiveCommand ShowCommand { get; }
    }
}
