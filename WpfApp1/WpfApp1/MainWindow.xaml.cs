using Entity;
using Microsoft.Extensions.DependencyInjection;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;
using Repository;
using System.Windows;
using Usecase;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            // DI
            var services = new ServiceCollection();
            services.AddSingleton<IXXRepository, InMemoryXXRepository>();
            services.AddTransient<SaveLoadUsecase>();
            services.AddTransient<InitUsecase>();
            services.AddTransient<Model>();
            var provider = services.BuildServiceProvider();

            _model = provider.GetRequiredService<Model>();

            Text =
                _model.Text.ToReactivePropertyAsSynchronized(x => x.Value);

            Number = _model.Number.ToReactivePropertyAsSynchronized(x => x.Value);

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