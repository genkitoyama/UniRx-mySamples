using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UniRx;

namespace Samples.Section3.Coroutines
{
    public class FromCoroutineValueSample : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            Observable.FromCoroutineValue<Vector3>(PositionCoroutine, false)
                      .Subscribe( x => Debug.Log(x),
                                 () => Debug.Log("OnCompleted"))
                      .AddTo(this);

        }

        private IEnumerator PositionCoroutine()
        {
            var positions = new[]{
                new Vector3(0,0,0),
                new Vector3(0,1,0),
                new Vector3(0,0,1),
                new Vector3(1,1,1),
            };

            foreach (var p in positions)
            {
                yield return p;
            }
        }

        // Update is called once per frame
        void Update()
        {
            
        }
    }
}