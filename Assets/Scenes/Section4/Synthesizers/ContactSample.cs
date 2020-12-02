using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UniRx;

namespace Samples.Section4.Synthesizers
{
    public class ContactSample : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            Debug.Log("--- contact ---");

            var s1 = Observable.Range(0, 3);
            var s2 = Observable.Range(10, 3);

            s1.Concat(s2)
              .Subscribe(x => Debug.Log(x));

            Debug.Log("--- contact IObservable<IObservable<T>> ---");

            // //observableを扱うobservable
            IObservable<IObservable<int>> streams = Observable.Range(1, 3)
                                                              .Select( x => {
                                                                  return Observable.Range(x*100, 3);
                                                              });

            streams.Concat().Subscribe(x => Debug.Log(x));

            Debug.Log("--- contact IEnumerable<IObservable<T>> ---");

            IEnumerable<IObservable<int>> streams2 = new[]{ Observable.Range(100, 3), 
                                                            Observable.Range(200, 3), 
                                                            Observable.Range(300, 3), };

            streams2.Concat().Subscribe(x => Debug.Log(x));  //配列内のobservableを1つのobservableに


        }

        // Update is called once per frame
        void Update()
        {
            
        }
    }
}
