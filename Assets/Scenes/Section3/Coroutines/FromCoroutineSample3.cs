using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UniRx;

namespace Samples.Section3.Coroutines
{
    public class FromCoroutineSample3 : MonoBehaviour
    {
        //長押し判定の閾値
        private readonly float longPressThresholdSeconds = 1f;

        // Start is called before the first frame update
        void Start()
        {

            //一定時間の長押しを検知する
            Observable.FromCoroutine<bool>(observer => this.LongPushCoroutine(observer))
                      .DistinctUntilChanged()       //連続して重複したメッセージを除去
                      .Subscribe( x => Debug.Log(x) )
                      .AddTo(this);

        }

        //スペースキーの長押しを検知するコルーチン、一定時間長押しされていたらtrueを返す
        private IEnumerator LongPushCoroutine(IObserver<bool> observer)
        {
            var isPushed = false;
            var lastPushTime = Time.time;

            while (true)
            {
                if(Input.GetKey(KeyCode.Space))
                {
                    //押した直後
                    if(!isPushed)
                    {
                        lastPushTime = Time.time;
                        isPushed = true;
                    }
                    else
                    {
                        //一定時間経過したら
                        if(Time.time - lastPushTime > this.longPressThresholdSeconds)
                        {
                            observer.OnNext(true);
                        }
                    }
                }
                else
                {
                    //離した直後
                    if(isPushed)
                    {
                        observer.OnNext(false);
                        isPushed = false;
                    }
                }

                yield return null;
            }

        }

    }
}