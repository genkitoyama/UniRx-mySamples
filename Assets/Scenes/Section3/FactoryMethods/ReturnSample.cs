using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UniRx;

namespace Samples.Section3.FactoryMethods
{
    public class ReturnSample : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            Observable.Return(100)
                      .Subscribe(
                          x => Debug.Log("OnNext: " + x),
                          ex => Debug.LogError("OnError: " + ex.Message),
                          () => Debug.Log("OnCompleted")
                        );
            
            //startからcount個連続でメッセージを発行
            // Observable.Range(start: 0, count:5);

            //指定したvalueをrepeatCount個 発行
            // Observable.Repeat(value: "yes", repeatCount: 3);

            //intを扱うobservableからOnErrorを発行
            // Observable.Throw<int>(new Exception("error"));

            //OnCompletedのみ
            // Observable.Empty<int>()

            //なにもしない
            // Observable.Never<int>()

            //「乱数を返すobservable」を、subscribeされるたびに生成
            var rand = Observable.Defer( () => 
            {
                var randomValue = UnityEngine.Random.Range(0, 100);
                return Observable.Return(randomValue);
            });
            rand.Subscribe(x => Debug.Log(x));
            rand.Subscribe(x => Debug.Log(x));
            rand.Subscribe(x => Debug.Log(x));

            //配列をobservableに変換
            var strArray = new []{"apple", "banana", "lemon"};
            strArray.ToObservable()
                    .Subscribe(
                        x => Debug.Log("OnNext: " + x),
                        ex => Debug.LogError("OnError: " + ex.Message),
                        () => Debug.Log("OnCompleted")
                    );

        }

        // Update is called once per frame
        void Update()
        {
            
        }
    }
}