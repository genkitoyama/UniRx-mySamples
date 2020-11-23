using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UniRx;

namespace Samples.Section3.ReactiveProperty
{
    public class ReactiveCollectionSample : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            var rc = new ReactiveCollection<int>();

            //要素が増えたとき
            rc.ObserveAdd()
              .Subscribe( (CollectionAddEvent<int> a) =>
              {
                  Debug.Log($"Add [{a.Index}]: {a.Value}");
              });

            //要素が削除されたとき
            rc.ObserveRemove()
              .Subscribe( (CollectionRemoveEvent<int> r) =>
              {
                  Debug.Log($"Remove [{r.Index}]: {r.Value}");
              });

            //要素が更新されたとき
            rc.ObserveReplace()
              .Subscribe( (CollectionReplaceEvent<int> r) =>
              {
                  Debug.Log($"Replace [{r.Index}]: {r.OldValue} -> {r.NewValue}");
              });

            //要素数が変化したとき
            rc.ObserveCountChanged()
              .Subscribe( (int c) =>
              {
                  Debug.Log($"Count {c}");
              });

            //要素のインデックスが変更されたとき
            rc.ObserveMove()
              .Subscribe( (CollectionMoveEvent<int> x) =>
              {
                  Debug.Log($"Move [{x.Value}]: {x.OldIndex} -> {x.NewIndex}");
              });


            rc.Add(1);
            rc.Add(2);
            rc.Add(3);

            rc[1] = 5;

            rc.RemoveAt(0);

            //Dispose時に各observableにOnCompletedが発行される
            rc.Dispose(); 
        }

        // Update is called once per frame
        void Update()
        {
            
        }
    }
}