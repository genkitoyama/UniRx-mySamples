using System.IO;
using System.Threading.Tasks;
using UnityEngine;

using UniRx;

namespace Samples.Section2.Schedulers
{
    public class ObserveOnSample : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            //ファイルをスレッドプール上で読み込む
            var task = Task.Run( () => File.ReadAllText(@"data.txt"));

            //task => observable変換、このときの実行コンテキストはスレッドプール
            task.ToObservable()
                //メインスレッドに切替え
                .ObserveOn(Scheduler.MainThread)
                .Subscribe( x => Debug.Log(x));　//ここに到達した時点でメインスレッドに切り替わっている
        }
    }    
}