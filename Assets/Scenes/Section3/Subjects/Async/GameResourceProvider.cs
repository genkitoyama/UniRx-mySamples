using System.Collections;
using System;
using UnityEngine;

using UniRx;

namespace Samples.Section3.Subjects.Async
{
    //リソースを読み込んで管理する
    public class GameResourceProvider : MonoBehaviour
    {
        private readonly AsyncSubject<Texture> playerTextureAsyncSubject = new AsyncSubject<Texture>();

        public IObservable<Texture> PlayerTextureAsync => playerTextureAsyncSubject;

        // Start is called before the first frame update
        void Start()
        {
            StartCoroutine(LoadTexture());
        }

        private IEnumerator LoadTexture()
        {
            //textureを非同期で読み込み
            var resource = Resources.LoadAsync<Texture>("Textures/player2");

            yield return resource;

            //読み込み完了したら結果を通知
            playerTextureAsyncSubject.OnNext(resource.asset as Texture);

            playerTextureAsyncSubject.OnCompleted();
        }

        // Update is called once per frame
        void Update()
        {
            
        }
    }
}

