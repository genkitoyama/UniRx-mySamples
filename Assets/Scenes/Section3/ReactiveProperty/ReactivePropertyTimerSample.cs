using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UniRx;

namespace Samples.Section3.ReactiveProperty
{
    public class ReactivePropertyTimerSample : MonoBehaviour
    {
        //実体としてReactivePropertyを定義
        [SerializeField]
        private IntReactiveProperty current = new IntReactiveProperty(60);

        //現在のタイマーの値(readonly)
        public IReadOnlyReactiveProperty<int> CurrentTime => current;

        // Start is called before the first frame update
        void Start()
        {
            current.Subscribe(x => Debug.Log(x))
                   .AddTo(this);

            StartCoroutine(CountDownCoroutine());
        }

        private IEnumerator CountDownCoroutine()
        {
            while (current.Value > 0)
            {
                //1sごとに値を更新
                yield return new WaitForSeconds(1);
                current.Value--;
            }
        }

        // Update is called once per frame
        void Update()
        {
            
        }
    }
}