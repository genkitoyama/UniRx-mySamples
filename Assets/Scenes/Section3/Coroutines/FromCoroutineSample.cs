using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UniRx;

namespace Samples.Section3.Coroutines
{
    public class FromCoroutineSample : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            //コルーチンの終了をobservableで待ち受ける
            Observable.FromCoroutine(WaitingCoroutine, publishEveryYield: true)
                      .Subscribe( _ => Debug.Log("OnNext"),
                                 () => Debug.Log("OnCompleted"))
                      .AddTo(this);

            //省略記法
            // WaitingCoroutine().ToObservable().Subscribe();

        }

        private IEnumerator WaitingCoroutine()
        {
            Debug.Log("coroutine start");

            yield return new WaitForSeconds(3);

            Debug.Log("coroutine finish");
        }

        // Update is called once per frame
        void Update()
        {
            
        }
    }
}