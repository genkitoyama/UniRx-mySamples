using System;
using Cysharp.Threading.Tasks;

using UnityEngine;
using UnityEngine.Networking;

using UniRx;

namespace Samples.Section3.FactoryMethods
{
    public class UnityWebRequestToObservable : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            //UniTaskからobservableに変換
            FetchAsync("https://unity.com/ja").ToObservable().Subscribe( x => Debug.Log(x) );
        }

        private async UniTask<string> FetchAsync(string uri)
        {
            using(var uwr = UnityWebRequest.Get(uri))
            {
                //awaitできる
                await uwr.SendWebRequest();
                return uwr.downloadHandler.text;
            }
        }
    }
}