using System.Collections;
using System;
using UnityEngine;

using UniRx;
using UniRx.Triggers;

namespace Samples.Section4.Converters
{
    public class ConvertersSample : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            Debug.Log("--- select ---");

            Observable.Range(1, 5)
                      .Select( x => x*10)   //メッセージの内容を10倍する
                      .Subscribe( x => Debug.Log(x));

            Debug.Log("--- cast ---");

            var objs = new object[]{ "hoge", "fuga", 'a', -1, "fuga", 'Z', 0.1};

            objs.ToObservable()
                .Cast<object, string>()     //string型にキャスト、'a'がcharなのでそこでエラー
                .Subscribe( x => Debug.Log(x),
                           ex => Debug.LogError(ex),
                           () => Debug.Log("OnCompleted"));

        }

        // Update is called once per frame
        void Update()
        {
            
        }
    }
}
