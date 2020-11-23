using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UniRx;

namespace Samples.Section3.Subjects
{
    public class ReplaySubjectSample : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            var subject = new ReplaySubject<int>(bufferSize: 3);

            for (int i = 0; i < 10; i++)
            {
                subject.OnNext(i);   
            }

            subject.OnCompleted();

            // errorもキャッシュできる
            // subject.OnError(new Exception("error!"));

            subject.Subscribe(
                x => Debug.Log("on next: " + x),
                ex => Debug.LogError("on error: " + ex),
                () => Debug.Log("on completed")
            );

            //disposeを忘れない
            subject.Dispose();
        }

        // Update is called once per frame
        void Update()
        {
            
        }
    }
}


