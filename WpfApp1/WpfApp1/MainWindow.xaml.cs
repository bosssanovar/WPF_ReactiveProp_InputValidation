using Reactive.Bindings;
using Reactive.Bindings.Extensions;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            Text =
                _model.Text.ToReactivePropertyAsSynchronized(
                    model => model.Value,       // propertySelector
                    model => model,             // convert
                    value =>                    // convertBack
                    {
                        if (value.Length > 10) return value.Substring(0, 10);
                        return value;
                    });

            Number = _model.Number.ToReactivePropertyAsSynchronized(
                    model => model.Value,       // propertySelector
                    model => model,             // convert
                    value =>                    // convertBack
                    {
                        if (value > 500) return 500;
                        return value;
                    });

            InitCommand = new AsyncReactiveCommand();
            InitCommand.Subscribe(async () =>
            {
                await Task.Delay(2000);

                _model.Init();
            });

            ShowCommand = new AsyncReactiveCommand();
            ShowCommand.Subscribe(async () =>
            {
                await Task.Delay(1000);

                _model.ShowModelData();
            });

            InitializeComponent();
        }
    }
}