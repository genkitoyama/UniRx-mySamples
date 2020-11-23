using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UniRx;

namespace Samples.Section3.ReactiveProperty
{
    public class ReactivePropertySample : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            var health = new ReactiveProperty<int>(100);

            Debug.Log("current value: " + health.Value);

            //ReactivePropertyを直接Subscribeできる
            //Subscribeしたタイミングで現在の値が自動的に発行される
            health.Subscribe(
                x => Debug.Log("notified value: " + x),
                () => Debug.Log("on completed")
            );

            //setすると同時にOnNextが発行される
            health.Value = 50;

            Debug.Log("current value: " + health.Value);

            //Disposeすると同時にOnCompletedが発行される
            health.Dispose();
        }

        // Update is called once per frame
        void Update()
        {
            
        }
    }
}
