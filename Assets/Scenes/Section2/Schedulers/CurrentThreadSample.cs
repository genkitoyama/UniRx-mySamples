using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

using UniRx;

namespace Samples.Section2.Schedulers
{
    public class CurrentThreadSample : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            Debug.Log("main thread ID: " + Thread.CurrentThread.ManagedThreadId);

            var subject = new Subject<Unit>();
            subject.AddTo(this);

            //onnextメッセージを現行スレッドにて処理する
            subject.ObserveOn(Scheduler.Immediate)
                    .Subscribe( _ => 
                    {
                        Debug.Log("thread ID: " + Thread.CurrentThread.ManagedThreadId);
                    });

            //メインスレッドにてonnext発行
            subject.OnNext(Unit.Default);

            //別スレッドにてonnext発行
            Task.Run( () => subject.OnNext(Unit.Default));

            subject.OnCompleted();
        }
    }    
}


