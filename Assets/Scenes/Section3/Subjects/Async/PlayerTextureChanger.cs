using System.Collections;
using System;
using UnityEngine;

using UniRx;

namespace Samples.Section3.Subjects.Async
{
    //playerのテクスチャを変更する
    public class PlayerTextureChanger : MonoBehaviour
    {
        [SerializeField]
        private GameResourceProvider gameResourceProvider;

        // Start is called before the first frame update
        void Start()
        {
            //playerのテクスチャが読み込み完了次第、テクスチャを変更する
            this.gameResourceProvider.PlayerTextureAsync
                                     .Subscribe(SetMyTexture)
                                     .AddTo(this);
        }

        private void SetMyTexture(Texture newTexture)
        {
            var r = this.GetComponent<Renderer>();
            r.sharedMaterial.mainTexture = newTexture;
        }
    }
}

