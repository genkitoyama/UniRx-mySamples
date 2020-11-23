using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UniRx;

namespace Samples.Section3.ReactiveProperty
{
    public class ReactivePropertySample2 : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            var health = new ReactiveProperty<int>(100);

            //subscribe直後のOnNextを無視
            // health.SkipLatestValueOnSubscribe();

            health.Subscribe(
                x => Debug.Log("notified value: " + x)
            );

            Debug.Log("< set value to 100 >");


            //現在の値と同じ値では通知はされない
            health.Value = 100;

            //強制的に値を通知
            Debug.Log("< force notify >");
            health.SetValueAndForceNotify(100);

            health.Dispose();
        }

        // Update is called once per frame
        void Update()
        {
            
        }
    }
}
