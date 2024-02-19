using Entity;
using Reactive.Bindings;
using System;
using System.Diagnostics;
using System.Reactive.Linq;
using System.Windows;
using Usecase;

namespace WpfApp1
{
    public class Model
    {
        public ReactivePropertySlim<string> Text { get; } = new ReactivePropertySlim<string>();

        public ReactivePropertySlim<int> Number { get; } = new ReactivePropertySlim<int>();

        public ReactivePropertySlim<bool> Bool { get; } = new ReactivePropertySlim<bool>();

        public ReactivePropertySlim<SomeEnum> SomeEnum { get; } = new ReactivePropertySlim<SomeEnum>();

        private readonly SaveLoadUsecase _saveLoadUsecase;

        private readonly InitUsecase _initUsecase;

        public Model(SaveLoadUsecase saveLoadUsecase, InitUsecase initUsecase)
        {
            _saveLoadUsecase = saveLoadUsecase;
            _saveLoadUsecase.OnSomeEnumChanged += SaveLoadUsecase_OnSomeEnumChanged;
            _initUsecase = initUsecase;

            LoadEntity();

            Text.Where(input => TextVO.IsValid(input))
                .Subscribe(validValue =>
                {
                    // TODO K.I : ここをDRY
                    // TODO K.I : Usecaseを設計しなおし
                    var entity = _saveLoadUsecase.Load();
                    entity.Text = new(validValue);
                    _saveLoadUsecase.Save(entity);
                });
            Text.Where(input => !TextVO.IsValid(input))
                .Subscribe(InvalidValue =>
                {
                    var currected = TextVO.CurrectValue(InvalidValue);
                    Text.Value = currected;
                });

            Number.Where(input => NumberVO.IsValid(input))
                .Subscribe(validValue =>
                {
                    var entity = _saveLoadUsecase.Load();
                    entity.Number = new(validValue);
                    _saveLoadUsecase.Save(entity);
                });
            Number.Where(input => !NumberVO.IsValid(input))
                .Subscribe(InvalidValue =>
                {
                    var currected = NumberVO.CurrectValue(InvalidValue);
                    Number.Value = currected;
                });

            Bool.Subscribe(value =>
                {
                    var entity = _saveLoadUsecase.Load();
                    entity.Bool = new(value);
                    _saveLoadUsecase.Save(entity);
                });

            SomeEnum.Subscribe(value =>
                {
                    var entity = _saveLoadUsecase.Load();
                    entity.SomeEnum = new(value);
                    _saveLoadUsecase.Save(entity);
                });
        }

        private void SaveLoadUsecase_OnSomeEnumChanged()
        {
            // TODO K.I : デバッグ用
            Debug.WriteLine("SaveLoadUsecase_OnSomeEnumChanged");

            var entity = _saveLoadUsecase.Load();
            SomeEnum.Value = entity.SomeEnum.Content;
        }

        private void LoadEntity()
        {
            var entity = _saveLoadUsecase.Load();

            Text.Value = entity.Text.Content;
            Number.Value = entity.Number.Content;
            Bool.Value = entity.Bool.Content;
            SomeEnum.Value = entity.SomeEnum.Content;
        }

        internal void Init()
        {
            _initUsecase.Init();
            LoadEntity();
        }

        internal void ShowModelData()
        {
            var entity = _saveLoadUsecase.Load();
            var text = entity.Text.Content;
            var number = entity.Number.Content;
            var b = entity.Bool.Content;
            var someEnum = entity.SomeEnum.Content;

            MessageBox.Show($"XXEntity Data\n\nText : {text}\n Number : {number}\n Bool : {b}\n SomeEnum : {someEnum.GetText()}");
        }
    }
}
