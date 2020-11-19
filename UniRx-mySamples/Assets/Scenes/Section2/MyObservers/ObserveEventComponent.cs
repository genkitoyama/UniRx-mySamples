using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UniRx;

namespace Samples.Section2.MyObservers
{
    public class ObserveEventComponent : MonoBehaviour 
    {
        [SerializeField]
        private CountDownEventProvider countDownEventProvider;

        //observerのインスタンス
        private PrintLogObserver<int> printLogObserver;

        private IDisposable disposable;

        private void Start()
        {
            printLogObserver = new PrintLogObserver<int>();

            //subjectの Subscribe を呼び出して、observerを登録する
            disposable = countDownEventProvider.CountDownObservable
                                               .Subscribe(printLogObserver);
        }

        private void OnDestroy()
        {
            //gameobject破棄時にイベント購読を中断する
            disposable?.Dispose();
        }
    }
   
}