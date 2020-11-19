using System;
using System.Threading;
using UnityEngine;

using UniRx;

namespace Samples.Section2.Schedulers
{
    public class TimersSample : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            //1s経過した直後のupdate()と同じタイミングで呼ばれる
            Observable.Timer(TimeSpan.FromSeconds(1), Scheduler.MainThread)
                      .Subscribe( x => Debug.Log("1 second has passed in main thread"))
                      .AddTo(this);

            //未指定の場合はMainThreadScheduler指定と同じになる
            Observable.Timer(TimeSpan.FromSeconds(1))
                      .Subscribe( x => Debug.Log("1 second has passed with non arguments"))
                      .AddTo(this);

            //1s経過直後のフレームのレンダリング後に実行される
            Observable.Timer(TimeSpan.FromSeconds(1), Scheduler.MainThreadEndOfFrame)
                      .Subscribe( x => Debug.Log("1 second has passed in main thread end of frame"))
                      .AddTo(this);

            //currentThreadの場合はそのまま同じスレッドで実行される、この場合は新しくスレッドを作ってタイマーを実行
            new Thread( () => 
            {
                Observable.Timer(TimeSpan.FromSeconds(1), Scheduler.CurrentThread)
                          .Subscribe( x => Debug.Log("1 second has passed in new thread"))
                          .AddTo(this);
            }).Start();
        }
    }    
}