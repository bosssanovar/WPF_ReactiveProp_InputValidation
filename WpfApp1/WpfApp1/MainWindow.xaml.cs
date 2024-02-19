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

            Text = _model.Text.ToReactivePropertySlimAsSynchronized(x => x.Value);
            Number = _model.Number.ToReactivePropertySlimAsSynchronized(x => x.Value);
            Bool = _model.Bool.ToReactivePropertySlimAsSynchronized(x => x.Value);
            SomeEnum = _model.SomeEnum.ToReactivePropertySlimAsSynchronized(x => x.Value);

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

            ComboBoxItems = new ReactivePropertySlim<List<ComboBoxItemDisplayValue<SomeEnum>>>();
            InitComboBoxItems();

            InitMediator();

            InitializeComponent();
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