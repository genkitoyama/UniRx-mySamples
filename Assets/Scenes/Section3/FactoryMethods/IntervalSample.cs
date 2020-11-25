using System;
using UnityEngine;

using UniRx;

namespace Samples.Section3.FactoryMethods
{

    public class IntervalSample : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            //interval, subscribeしてから1s間隔で発行する（最初に1s待つ）
            Observable.Interval(TimeSpan.FromSeconds(1))
                      .Subscribe()
                      .AddTo(this);

            //timer, subscribe直後にメッセージを発行する
            Observable.Timer(TimeSpan.Zero, TimeSpan.FromSeconds(1))
                      .Subscribe()
                      .AddTo(this);
        }

        // Update is called once per frame
        void Update()
        {
            
        }
    }

}