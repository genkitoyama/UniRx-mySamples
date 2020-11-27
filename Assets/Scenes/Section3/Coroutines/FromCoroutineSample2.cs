using System;
using System.Collections;
using System.Threading;
using UnityEngine;

using UniRx;

namespace Samples.Section3.Coroutines
{
    public class FromCoroutineSample2 : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            //cancellation token を利用する場合
            Observable.FromCoroutine( token => WaitingCoroutine(token), publishEveryYield: true)
                      .Subscribe( _ => Debug.Log("OnNext"),
                                 () => Debug.Log("OnCompleted"))
                      .AddTo(this);

        }

        //cancellation token を受け取る
        private IEnumerator WaitingCoroutine(CancellationToken token)
        {
            Debug.Log("coroutine start");

            //observableをコルーチンとして待ち受ける場合、コルーチンが停止したタイミングで yield return で待ち受けているobservableも止まって欲しい
            //そのためのcancellation token
            yield return Observable.Timer(TimeSpan.FromSeconds(3))
                                   .ToYieldInstruction(token);

            Debug.Log("coroutine finish");
        }

        // Update is called once per frame
        void Update()
        {
            
        }
    }
}