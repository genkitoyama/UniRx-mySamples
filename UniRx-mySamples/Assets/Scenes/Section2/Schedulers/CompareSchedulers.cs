using System;
using UnityEngine;

using UniRx;

namespace Samples.Section2.Schedulers
{
    public class CompareSchedulers : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            //コルーチンを用いて3s計測する
            Observable.Timer(TimeSpan.FromSeconds(3), Scheduler.MainThread)
                      .Subscribe( x => Debug.Log("counted 3 seconds using coroutine in main thread"))
                      .AddTo(this);

            //メインスレッドをThread.Sleepして3s計測
            Observable.Timer(TimeSpan.FromSeconds(3), Scheduler.CurrentThread)
                      .Subscribe( x => Debug.Log("counted 3 seconds using Thread.Sleep to main thread"))
                      .AddTo(this);
        }
    }    
}