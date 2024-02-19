using Entity;
using Microsoft.Extensions.DependencyInjection;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;
using Repository;
using RepositoryMonitor;
using System.Windows;
using Usecase;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private RepositoryMonitorView _repositoryMonitorView;

        public MainWindow()
        {
            // DI
            var services = new ServiceCollection();
            services.AddSingleton<IXXRepository, InMemoryXXRepository>();
            services.AddTransient<SaveLoadUsecase>();
            services.AddTransient<InitUsecase>();
            services.AddTransient<Model>();
            services.AddTransient<RepositoryMonitorView>();
            var provider = services.BuildServiceProvider();

            _model = provider.GetRequiredService<Model>();

            _repositoryMonitorView = provider.GetRequiredService<RepositoryMonitorView>();

            Text = _model.Text.ToReactivePropertySlimAsSynchronized(x => x.Value);
            Number = _model.Number.ToReactivePropertySlimAsSynchronized(x => x.Value);
            Bool = _model.Bool.ToReactivePropertySlimAsSynchronized(x => x.Value);
            SomeEnum = _model.SomeEnum.ToReactivePropertySlimAsSynchronized(x => x.Value);

            InitCommand = new AsyncReactiveCommand();
            InitCommand.Subscribe(async () =>
            {
                await Task.Delay(500);

                _model.Init();
            });

            SaveCommand = new AsyncReactiveCommand();
            SaveCommand.Subscribe(async () =>
            {
                await Task.Delay(500);

                _model.Save();
            });

            ComboBoxItems = new ReactivePropertySlim<List<ComboBoxItemDisplayValue<SomeEnum>>>();
            InitComboBoxItems();

            InitMediator();

            InitializeComponent();
        }

        protected override void OnContentRendered(EventArgs e)
        {
            base.OnContentRendered(e);

            _repositoryMonitorView.Owner = this;
            _repositoryMonitorView.Show();
        }

        private void InitComboBoxItems()
        {
            var comboBoxItemList = new List<ComboBoxItemDisplayValue<SomeEnum>>();
            comboBoxItemList.Add(new ComboBoxItemDisplayValue<SomeEnum>(Entity.SomeEnum.Dog, Entity.SomeEnum.Dog.GetText()));
            if (_model.Bool.Value)
            {
                comboBoxItemList.Add(new ComboBoxItemDisplayValue<SomeEnum>(Entity.SomeEnum.Cat, Entity.SomeEnum.Cat.GetText()));
            }
            comboBoxItemList.Add(new ComboBoxItemDisplayValue<SomeEnum>(Entity.SomeEnum.Elephant, Entity.SomeEnum.Elephant.GetText()));
            comboBoxItemList.Add(new ComboBoxItemDisplayValue<SomeEnum>(Entity.SomeEnum.Pig, Entity.SomeEnum.Pig.GetText()));

            ComboBoxItems.Value = new List<ComboBoxItemDisplayValue<SomeEnum>>(comboBoxItemList);
        }
    }
}