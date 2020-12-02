using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

using Cysharp.Threading.Tasks;

using UniRx;

namespace Samples.Section4.Synthesizers
{
    public class SelectManySample : MonoBehaviour
    {
        //ダウンロードボタン
        [SerializeField]
        private Button downloadButton;

        //URI入力欄
        [SerializeField]
        private InputField uriInputField;

        // Start is called before the first frame update
        void Start()
        {
            Debug.Log("--- select many ---");

            //ボタンクリックされたら、指定URIに対してhttp通信を行う
            this.downloadButton.OnClickAsObservable()
                               .Select( _ => this.uriInputField.text)   //入力されたuriを取得
                               .SelectMany( uri => this.FetchAsync(uri).ToObservable()) //指定されたuriに通信
                               .Subscribe( x => Debug.Log(x));  //結果を表示

            Debug.Log("--- select many another observable ---");

            var uris = new[]{ "https://unity3d.com/jp",
                              "https://www.google.co.jp",
                              "https://www.bing.com", };

            uris.ToObservable()
                .SelectMany( uri => this.TryGetAsync(uri).ToObservable(), (uri, body) => (uri, body))   //uriとその通信結果をペアにする
                .Subscribe( x => {
                    var (uri, body) = x;
                    Debug.Log($"{uri}への通信結果: {body}");
                });

        }

        private async UniTask<string> FetchAsync(string uri)
        {
            using(var uwr = UnityWebRequest.Get(uri))
            {
                await uwr.SendWebRequest();

                return uwr.downloadHandler.text;
            }
        }

        //unitaskで通信してその成否を返す
        private async UniTask<bool> TryGetAsync(string uri)
        {
            using(var uwr = UnityWebRequest.Get(uri))
            {
                try
                {
                    await uwr.SendWebRequest();
                    return true;
                }
                catch(Exception e)
                {
                    Debug.LogError(e);
                    return false;
                }
            }
        }

        // Update is called once per frame
        void Update()
        {
            
        }
    }
}
