using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UniRx;

namespace Samples.Section3.FactoryMethods
{
    public class WithMainThreadSchedulerSample : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            //main threadの場合、1frameに1つずつ発行
            Observable.Range(start: 0, count: 5, scheduler: Scheduler.MainThread)
                        .Subscribe( x => {
                            Debug.Log($"frame: {Time.frameCount}, value: {x}");
                        });

            //current threadの場合、1frame内で連続して発行
            Observable.Range(start: 0, count: 5, scheduler: Scheduler.CurrentThread)
                        .Subscribe( x => {
                            Debug.Log($"frame: {Time.frameCount}, value: {x}");
                        });
        }

        // Update is called once per frame
        void Update()
        {
            
        }
    }
}
