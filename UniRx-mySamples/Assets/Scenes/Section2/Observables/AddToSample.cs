using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UniRx;

namespace Samples.Section2.Observables
{
    public class AddToSample : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            //5フレごとにメッセージを発行するobsevable
            Observable.IntervalFrame(5)
                      .Subscribe(_ => Debug.Log("DO!"))
                      //このgameobjectのOnDestroyに連動して自動でDisposeさせる
                      .AddTo(this);
        }
    }    
}


