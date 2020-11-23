using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UniRx;

namespace Samples.Section3.Subjects
{
    public class SubjectSample : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            var subject = new Subject<int>();

            subject.OnNext(1);

            subject.Subscribe(
                x => Debug.Log("on next: " + x),
                ex => Debug.LogError("on error: " + ex),
                () => Debug.Log("on completed")
            );

            subject.OnNext(2);
            subject.OnNext(3);

            subject.OnCompleted();

            //disposeを忘れない
            subject.Dispose();
        }

        // Update is called once per frame
        void Update()
        {
            
        }
    }
}


