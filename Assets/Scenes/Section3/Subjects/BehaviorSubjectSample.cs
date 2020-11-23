using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UniRx;

namespace Samples.Section3.Subjects
{
    public class BehaviorSubjectSample : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            var behaviorSubject = new BehaviorSubject<int>(0);

            behaviorSubject.OnNext(1);

            behaviorSubject.Subscribe(
                x => Debug.Log("on next: " + x),
                ex => Debug.LogError("on error: " + ex),
                () => Debug.Log("on completed")
            );

            behaviorSubject.OnNext(2);

            Debug.Log("lats value: " + behaviorSubject.Value);

            behaviorSubject.OnNext(3);

            behaviorSubject.OnCompleted();

            //disposeを忘れない
            behaviorSubject.Dispose();
        }

        // Update is called once per frame
        void Update()
        {
            
        }
    }
}


