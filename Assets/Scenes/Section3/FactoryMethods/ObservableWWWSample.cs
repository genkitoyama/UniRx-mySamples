using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UniRx;

namespace Samples.Section3.FactoryMethods
{
    public class ObservableWWWSample : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            var uri = "https://unity3d.com/jp";

            //結果のみ受け取る場合（deprecatedになっている）
            ObservableWWW.Get(uri).Subscribe( x => Debug.Log(x) );

            //通信終了時にwwwごと受け取る場合（deprecatedになっている）
            // ObservableWWW.Get(uri).Subscribe( www => Debug.Log(www.text) );
        }

        // Update is called once per frame
        void Update()
        {
            
        }
    }
}