using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UniRx;

namespace Samples.Section2.MyObservers
{
    //受信したメッセージをログに出力する
    public class PrintLogObserver<T> : IObserver<T>
    {
        public void OnCompleted()
        {
            Debug.Log("OnCompleted");
        }

        public void OnError(Exception error)
        {
            Debug.Log(error);
        }

        public void OnNext(T value)
        {
            Debug.Log(value);
        }
    }
   
}