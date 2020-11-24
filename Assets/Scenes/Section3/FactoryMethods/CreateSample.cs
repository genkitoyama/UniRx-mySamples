using System;
using System.Threading.Tasks;
using UnityEngine;

using UniRx;

namespace Samples.Section3.FactoryMethods
{
    public class CreateSample : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            //Aからはじまるアルファベットを一定時間ごとに順番に生成する
            var observable = Observable.Create<char>( observer => {
                //IDisposableとCancellationTokenがくっついたもの、Disposeされるとキャンセル状態になる
                var disposable = new CancellationDisposable();

                Task.Run(async () => {
                    for (int i = 0; i < 26; i++)
                    {
                        await Task.Delay(TimeSpan.FromSeconds(1), disposable.Token);
                        //文字を発行
                        observer.OnNext((char)('A' + i));
                    }
                    observer.OnCompleted();
                }, disposable.Token);

                //subscribeが中断されたら連動してcancellationTokenもキャンセル状態になる
                return disposable;
            });

            observable.Subscribe(
                x => Debug.Log("OnNext: " + x),
                ex => Debug.LogError("OnError: " + ex.Message),
                () => Debug.Log("OnCompleted")
            ).AddTo(this);
        }

        // Update is called once per frame
        void Update()
        {
            
        }
    }
}
