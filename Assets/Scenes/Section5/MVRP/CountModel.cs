using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UniRx;

namespace Samples.Section5.MVRP
{
    public class CountModel : MonoBehaviour
    {
        [SerializeField]
        private IntReactiveProperty current = new IntReactiveProperty(0);

        //presenter向けに公開するプロパティ
        public IReadOnlyReactiveProperty<int> Current => current;

        //整数値の更新
        public void UpdateCount(int value)
        {
            this.current.Value = Mathf.Clamp(value, 0, 100);
        }
    }

}