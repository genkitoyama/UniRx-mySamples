using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UniRx;

//これを追加することで各イベントをobservableに変換できる
using UniRx.Triggers;

namespace Samples.Section3.FactoryMethods
{
    public class TriggerSample : MonoBehaviour
    {
        //自分とは別のgameobject
        [SerializeField]
        private GameObject childGameObject;

        // Start is called before the first frame update
        void Start()
        {
            //自身のupdateをobservableに変換
            this.UpdateAsObservable()
                .Subscribe( _ => {
                    this.transform.position += Vector3.forward * Time.deltaTime;
                });

            //他のgameobjectのonCollisionEnterをobservableに変換
            childGameObject.OnCollisionEnterAsObservable()
                            .Subscribe( collision => {
                                Debug.Log("collide to " + collision.gameObject.name);
                            }).AddTo(this); //他のgameobjectを利用するならAddToしたほうがよい

            //自身のondesrtoyをobservableに変換
            this.OnDestroyAsObservable()
                .Subscribe( _ => {
                    Debug.Log("destroyed");
                });
        }

        // Update is called once per frame
        void Update()
        {
            
        }
    }
}
