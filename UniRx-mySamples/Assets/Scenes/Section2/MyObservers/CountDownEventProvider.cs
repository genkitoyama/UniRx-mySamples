using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UniRx;

namespace Samples.Section2.MyObservers
{
    //指定秒数カウントしてイベント通知
    public class CountDownEventProvider: MonoBehaviour 
    {
        [SerializeField]
        private int countSeconds = 10;

        //subjectのインスタンス
        private Subject<int> subject;

        //subjectのinterfaceのみ公開する
        public IObservable<int> CountDownObservable => subject;

        private void Awake()
        {
            subject = new Subject<int>();

            StartCoroutine(CountCoroutine());
        }

        //カウントの都度メッセージを発行するcoroutine
        private IEnumerator CountCoroutine()
        {
            var current = countSeconds;

            while(current > 0)
            {
                subject.OnNext(current);
                current--;

                yield return new WaitForSeconds(1f);
            }

            subject.OnNext(0);
            subject.OnCompleted();
        }

        private void OnDestroy()
        {
            //gameobjectが破棄されたらsubjectも解放する
            subject.Dispose();    
        }
    }
   
}