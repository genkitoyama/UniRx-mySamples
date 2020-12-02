using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UniRx;

namespace Samples.Section4.Synthesizers
{
    public class MergeSample : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            Debug.Log("--- merge ---");

            var s1 = Observable.Range(10, 3);
            var s2 = Observable.Range(20, 3);

            //現在のフレーム数と出力結果をペアにして表示
            s1.Merge(s2)
              .Subscribe( x => Debug.Log($"{Time.frameCount}: {x}"));

            Debug.Log("--- merge IObservable<IObservable<T>> ---");

            //observableを扱うobservable
            IObservable<IObservable<int>> streams = Observable.Range(1, 3)
                                                              .Select( x => {
                                                                  return Observable.Range(x*100, 3);
                                                              });

            streams.Merge().Subscribe(x => Debug.Log(x));

            Debug.Log("--- merge IEnumerable<IObservable<T>> ---");

            IEnumerable<IObservable<int>> streams2 = new[]{ Observable.Range(100, 3), 
                                                            Observable.Range(200, 3), 
                                                            Observable.Range(300, 3), };

            streams2.Merge().Subscribe(x => Debug.Log(x));  //配列内のobservableを1つのobservableに


        }

        // Update is called once per frame
        void Update()
        {
            
        }
    }
}
