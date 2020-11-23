using System;
using UniRx;

namespace Samples.Section3.ReactiveProperty
{
    public enum Fruit
    {
        Apple,
        Banana,
        Peach,
        Melon,
        Orange,
    }

    [Serializable]
    public class FruitsReactiveProperty : ReactiveProperty<Fruit>
    {
        public FruitsReactiveProperty()
        {

        }

        public FruitsReactiveProperty(Fruit init) : base(init)
        {

        }
    }
}
