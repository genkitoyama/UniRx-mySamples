using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using UniRx;

namespace Samples.Section3.uGUIs
{
    public class UGuiEventConvert : MonoBehaviour
    {
        [SerializeField]
        private Toggle toggle;

        // Start is called before the first frame update
        void Start()
        {
            this.toggle.isOn = false;

            //uGUIイベントを変換するパターン
            this.toggle.onValueChanged.AsObservable()
                                      .Subscribe( x => Debug.Log("current state(AsObservable): " + x));

            //拡張メソッド、subscribeした瞬間に初期値が発行される
            this.toggle.OnValueChangedAsObservable()
                       .Subscribe( x => Debug.Log("current state(augmented method): " + x));

            Debug.Log("---");

            this.toggle.isOn = true;
        }

        // Update is called once per frame
        void Update()
        {
            
        }
    }
}
