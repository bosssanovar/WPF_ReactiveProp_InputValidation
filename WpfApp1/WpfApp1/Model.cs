using Entity;
using Reactive.Bindings;
using System.Reactive.Linq;
using System.Windows;
using Usecase;

namespace WpfApp1
{
    public class Model
    {
        public ReactivePropertySlim<string> Text { get; } = new ReactivePropertySlim<string>();

        public ReactivePropertySlim<int> Number { get; } = new ReactivePropertySlim<int>();

        private readonly SaveLoadUsecase _saveLoadUsecase;

        private readonly InitUsecase _initUsecase;

        public Model(SaveLoadUsecase saveLoadUsecase, InitUsecase initUsecase)
        {
            _saveLoadUsecase = saveLoadUsecase;
            _initUsecase = initUsecase;

            LoadEntity();

            Text.Where(input => TextVO.IsValid(input))
                .Subscribe(validValue =>
                {
                    // TODO K.I : ここをDRY
                    // TODO K.I : Usecaseを設計しなおし
                    // TODO K.I : 他コントロールも実装
                    // TODO K.I : 他値補正も検討
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
        }

        private void LoadEntity()
        {
            var entity = _saveLoadUsecase.Load();

            Text.Value = entity.Text.Content;
            Number.Value = entity.Number.Content;
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

            MessageBox.Show($"XXEntity Data\n\nText : {text}\n Number : {number}");
        }
    }
}
