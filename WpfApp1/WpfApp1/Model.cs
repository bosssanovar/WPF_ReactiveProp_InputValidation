using Entity;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;
using System;
using System.Diagnostics;
using System.Reactive.Linq;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Windows;
using Usecase;

namespace WpfApp1
{
    public class Model
    {
        public bool IsAutoSave { get; set; }

        private ReactivePropertySlim<XXEntity> _entity;

        public ReactivePropertySlim<string> Text { get; }

        public ReactivePropertySlim<int> Number { get; }

        public ReactivePropertySlim<bool> Bool { get; }

        public ReactivePropertySlim<SomeEnum> SomeEnum { get; }

        private readonly SaveLoadUsecase _saveLoadUsecase;

        private readonly InitUsecase _initUsecase;

        public Model(SaveLoadUsecase saveLoadUsecase, InitUsecase initUsecase)
        {
            _saveLoadUsecase = saveLoadUsecase;
            _saveLoadUsecase.OnSomeEnumChanged += SaveLoadUsecase_OnSomeEnumChanged;
            _initUsecase = initUsecase;

            _entity = new ReactivePropertySlim<XXEntity>();
            LoadEntity();
            _entity.Subscribe(x =>
            {
                if (IsAutoSave)
                {
                    Debug.WriteLine("Auto Save");

                    _saveLoadUsecase.Save(x);
                }
            });

            Text = _entity.ToReactivePropertySlimAsSynchronized(
                x => x.Value,
                x => x.Text.Content,
                x =>
                {
                    Debug.WriteLine("Text ConvertBack");

                    var currected = TextVO.CurrectValue(x);
                    var entity = _entity.Value.Clone();
                    entity.Text = new(currected);
                    return entity;
                });

            Number = _entity.ToReactivePropertySlimAsSynchronized(
                x => x.Value,
                x => x.Number.Content,
                x =>
                {
                    Debug.WriteLine("Number ConvertBack");

                    var currected = NumberVO.CurrectValue(x);
                    var entity = _entity.Value.Clone();
                    entity.Number = new(currected);
                    return entity;
                });

            Bool = _entity.ToReactivePropertySlimAsSynchronized(
                x => x.Value,
                x => x.Bool.Content,
                x =>
                {
                    Debug.WriteLine("Bool ConvertBack");

                    var currected = BoolVO.CurrectValue(x);
                    var entity = _entity.Value.Clone();
                    entity.Bool = new(currected);
                    return entity;
                });

            SomeEnum = _entity.ToReactivePropertySlimAsSynchronized(
                x => x.Value,
                x => x.SomeEnum.Content,
                x =>
                {
                    Debug.WriteLine("SomeEnum ConvertBack");

                    var currected = SomeEnumVO.CurrectValue(x);
                    var entity = _entity.Value.Clone();
                    entity.SomeEnum = new(currected);
                    return entity;
                });
        }

        private void SaveLoadUsecase_OnSomeEnumChanged()
        {
            Debug.WriteLine("SaveLoadUsecase_OnSomeEnumChanged");

            SomeEnum.Value = _entity.Value.SomeEnum.Content;
        }

        private void LoadEntity()
        {
            Debug.WriteLine("LoadEntity");

            _entity.Value = _saveLoadUsecase.Load();
        }

        internal void Init()
        {
            Debug.WriteLine("Init");

            _initUsecase.Init();
            LoadEntity();
        }

        internal void ShowData()
        {
            var entity = _entity.Value;
            var text = entity.Text.Content;
            var number = entity.Number.Content;
            var b = entity.Bool.Content;
            var someEnum = entity.SomeEnum.Content;

            var sb = new StringBuilder();
            sb.AppendLine("Model Data : ");
            sb.AppendLine($"  Text : {text}");
            sb.AppendLine($"  Number : {number}");
            sb.AppendLine($"  Bool : {b}");
            sb.AppendLine($"  SomeEnum : {someEnum.GetText()}");

            sb.AppendLine();

            entity = _saveLoadUsecase.Load();
            text = entity.Text.Content;
            number = entity.Number.Content;
            b = entity.Bool.Content;
            someEnum = entity.SomeEnum.Content;

            sb.AppendLine("Repository Data : ");
            sb.AppendLine($"  Text : {text}");
            sb.AppendLine($"  Number : {number}");
            sb.AppendLine($"  Bool : {b}");
            sb.AppendLine($"  SomeEnum : {someEnum.GetText()}");

            MessageBox.Show(sb.ToString());
        }

        internal void Save()
        {
            Debug.WriteLine("Save");

            _saveLoadUsecase.Save(_entity.Value);
        }
    }
}
