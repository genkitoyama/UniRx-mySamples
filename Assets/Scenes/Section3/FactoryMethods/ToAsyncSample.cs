using System;
using System.IO;
using UnityEngine;

using UniRx;

namespace Samples.Section3.FactoryMethods
{
    public class ToAsyncSample : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            //Observable.ToAsync の返り値は Func<IObservable<T>>
            Func<IObservable<string>> fileReadFunc;

            //スレッドプール上でファイルを読み込む処理を実行する
            fileReadFunc = Observable.ToAsync( () => {
                using (var r = new StreamReader(Application.dataPath + "/Resources/Text/data.txt"))
                {
                    Debug.Log(Application.dataPath + "/Resources/Text/data.txt");
                    return r.ReadToEnd();
                }
            }, Scheduler.ThreadPool);

            //非同期処理を開始するには明示的にメソッド呼び出しする必要がある
            fileReadFunc().Subscribe( x => Debug.Log(x) );

            //invokeでもよい
            // fileReadFunc.Invoke().Subscribe();
        }

        // Update is called once per frame
        void Update()
        {
            
        }
    }
}