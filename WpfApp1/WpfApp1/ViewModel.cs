using Entity;
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

        public bool IsAutoSave
        {
            get { return _model.IsAutoSave; }
            set
            {
                _model.IsAutoSave = value;
            }
        }

        public ReactivePropertySlim<string> Text { get; }
        public ReactivePropertySlim<int> Number { get; }
        public ReactivePropertySlim<bool> Bool { get; }
        public ReactivePropertySlim<SomeEnum> SomeEnum { get; }

        public AsyncReactiveCommand InitCommand { get; }
        public AsyncReactiveCommand ShowCommand { get; }
        public AsyncReactiveCommand SaveCommand { get; }

        public ReactivePropertySlim<List<ComboBoxItemDisplayValue<SomeEnum>>> ComboBoxItems { get; private set; }


        private void InitMediator()
        {
            Bool.Subscribe(x =>
            {
                InitComboBoxItems();
            });
        }
    }
}
