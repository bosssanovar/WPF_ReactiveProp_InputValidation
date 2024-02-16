using Reactive.Bindings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WpfApp1
{
    internal class Model
    {
        private const string Text_InitValue = "Init Text";
        private const int Number_InitValue = 100;

        public ReactivePropertySlim<String> Text { get; }

        public ReactivePropertySlim<int> Number { get; }

        public Model()
        {
            Text = new ReactivePropertySlim<String>(Text_InitValue);

            Number = new ReactivePropertySlim<int>(Number_InitValue);
        }

        internal void Init()
        {
            Text.Value = Text_InitValue;
            Number.Value = Number_InitValue;
        }

        internal void ShowModelData()
        {
            MessageBox.Show($"Text:{Text.Value}, Number:{Number.Value}");
        }
    }
}
