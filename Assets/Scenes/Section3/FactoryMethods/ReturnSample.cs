using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UniRx;

namespace Samples.Section3.FactoryMethods
{
    public class ReturnSample : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            Observable.Return(100)
                      .Subscribe(
                          x => Debug.Log("OnNext: " + x),
                          ex => Debug.LogError("OnError: " + ex.Message),
                          () => Debug.Log("OnCompleted")
                        );
        }

        // Update is called once per frame
        void Update()
        {
            
        }
    }
}