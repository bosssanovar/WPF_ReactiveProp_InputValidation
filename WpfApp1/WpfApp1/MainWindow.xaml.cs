﻿using Entity;
using Microsoft.Extensions.DependencyInjection;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;
using Repository;
using RepositoryMonitor;
using System.Diagnostics;
using System.Windows;
using Usecase;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly RepositoryMonitorView _repositoryMonitorView;

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

            Text = _model.Entity.ToReactivePropertySlimAsSynchronized(
                x => x.Value,
                x => x.Text.Content,
                x =>
                {
                    Debug.WriteLine("Text ConvertBack");

                    var currected = TextVO.CurrectValue(x);
                    var entity = _model.Entity.Value.Clone();
                    entity.Text = new(currected);
                    return entity;
                });

            Number = _model.Entity.ToReactivePropertySlimAsSynchronized(
                x => x.Value,
                x => x.Number.Content,
                x =>
                {
                    Debug.WriteLine("Number ConvertBack");

                    var currected = NumberVO.CurrectValue(x);
                    var entity = _model.Entity.Value.Clone();
                    entity.Number = new(currected);
                    return entity;
                });

            Bool = _model.Entity.ToReactivePropertySlimAsSynchronized(
                x => x.Value,
                x => x.Bool.Content,
                x =>
                {
                    Debug.WriteLine("Bool ConvertBack");

                    var currected = BoolVO.CurrectValue(x);
                    var entity = _model.Entity.Value.Clone();
                    entity.Bool = new(currected);
                    return entity;
                });

            SomeEnum = _model.Entity.ToReactivePropertySlimAsSynchronized(
                x => x.Value,
                x => x.SomeEnum.Content,
                x =>
                {
                    Debug.WriteLine("SomeEnum ConvertBack");

                    var currected = SomeEnumVO.CurrectValue(x);
                    var entity = _model.Entity.Value.Clone();
                    entity.SomeEnum = new(currected);
                    return entity;
                });

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
#pragma warning disable IDE0028 // コレクションの初期化を簡略化します
            var comboBoxItemList = new List<ComboBoxItemDisplayValue<SomeEnum>>();
#pragma warning restore IDE0028 // コレクションの初期化を簡略化します
            comboBoxItemList.Add(new ComboBoxItemDisplayValue<SomeEnum>(Entity.SomeEnum.Dog, Entity.SomeEnum.Dog.GetText()));
            if (Bool.Value)
            {
                comboBoxItemList.Add(new ComboBoxItemDisplayValue<SomeEnum>(Entity.SomeEnum.Cat, Entity.SomeEnum.Cat.GetText()));
            }
            comboBoxItemList.Add(new ComboBoxItemDisplayValue<SomeEnum>(Entity.SomeEnum.Elephant, Entity.SomeEnum.Elephant.GetText()));
            comboBoxItemList.Add(new ComboBoxItemDisplayValue<SomeEnum>(Entity.SomeEnum.Pig, Entity.SomeEnum.Pig.GetText()));

            ComboBoxItems.Value = new List<ComboBoxItemDisplayValue<SomeEnum>>(comboBoxItemList);
        }
    }
}