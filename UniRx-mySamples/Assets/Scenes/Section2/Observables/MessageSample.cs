using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UniRx;

namespace Samples.Section2.Observables
{
    public class MessageSample : MonoBehaviour
    {
        [SerializeField]
        private float countTimeSeconds = 3f;

        //時間切れを通知するobservable
        public IObservable<Unit> OnTimeUpAsyncSubject => onTimeUpAsyncSubject;

        //asyncsubject: メッセージを一度だけ発行できるsubject
        private readonly AsyncSubject<Unit> onTimeUpAsyncSubject = new AsyncSubject<Unit>();

        private IDisposable disposable;

        // Start is called before the first frame update
        void Start()
        {
            //時間経過したらメッセージを通知する
            disposable = Observable.Timer(TimeSpan.FromSeconds(this.countTimeSeconds))
                                   .Subscribe(_ =>
                                   {
                                        //発火したらUnit型のメッセージを発行する
                                        onTimeUpAsyncSubject.OnNext(Unit.Default);
                                        onTimeUpAsyncSubject.OnCompleted();
                                   });
        }

        private void OnDestroy()
        {
            //observableがまだ動いていたら止める
            disposable?.Dispose();

            onTimeUpAsyncSubject.Dispose();
        }
    }    
}


