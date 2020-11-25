using System;
using System.IO;
using UnityEngine;

using UniRx;

namespace Samples.Section3.FactoryMethods
{
    public class FromAsyncPatternSample : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            //ファイル名指定しreadした結果を返すデリゲート
            Func<string, string> readFile = fileName => {
                using (var r = new StreamReader(fileName))
                {
                    return r.ReadToEnd();
                }
            };

            //デリゲートを非同期実行し、結果をobservableで受け取れるように変換する（返り値もデリゲートになっている）
            Func<string, IObservable<string>> f = Observable.FromAsyncPattern<string, string>(readFile.BeginInvoke, readFile.EndInvoke);

            //デリゲートを実行したタイミングで非同期処理が開始される、内部でAsyncSubjectが使われている
            IObservable<string> readAsync = f(Application.dataPath + "/Resources/Text/data.txt");

            readAsync.Subscribe( x => Debug.Log(x) );
        }

        // Update is called once per frame
        void Update()
        {
            
        }
    }
}