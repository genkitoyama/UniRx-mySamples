using System;
using System.Collections;
using UnityEngine;

using UniRx;
using UniRx.Triggers;

namespace Samples.Section4.Filters
{
    public class FiltersSample : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            Debug.Log("--- where ---");

            Observable.Range(0, 10)
                      .Where( x => x%2 == 0)    //偶数のみ許可
                      .Subscribe( x => Debug.Log(x));

            Debug.Log("--- oftype ---");

            object[] objs = {1, "hoge", 1f, "fuga", 'z', 0.1};

            objs.ToObservable()
                .OfType<object, string>()   //stringのみ
                .Subscribe( x => Debug.Log(x));

            Debug.Log("--- ignore elements ---");

            Observable.Range(0, 10)
                      .IgnoreElements()     //OnNextをすべて遮断
                      .Subscribe( x => Debug.Log(x),
                                 () => Debug.Log("OnCompleted"));

            Debug.Log("--- distinct ---");

            var array = new[]{1, 2, 2, 3, 1, 1, 2, 2, 3};

            array.ToObservable()
                 .Distinct()    //過去にすでに発行しているメッセージは無視
                 .Subscribe( x => Debug.Log(x));

            this.OnCollisionEnterAsObservable() //過去に衝突したことがあるgameobjectは無視
                .Distinct( x => x.gameObject)   //collision型からgameobjectのみ取り出して比較する
                .Subscribe( x => Debug.Log(x.gameObject.name));

            Debug.Log("--- distinct until changed ---");

            array.ToObservable()
                 .DistinctUntilChanged()    //直前と同じ値のメッセージは無視
                 .Subscribe( x => Debug.Log(x));

            Debug.Log("--- first ---");

            Observable.Range(0, 10)
                      .First( x => x%3 == 0)    //条件をみたす最初のメッセージだけ通す
                      .Subscribe( x => Debug.Log(x),
                                 () => Debug.Log("OnCompleted"));

            Debug.Log("--- first or default ---");

            Observable.Range(0, 10)
                      .FirstOrDefault( x => x%3 == 0)
                      .Subscribe( x => Debug.Log(x),
                                 () => Debug.Log("OnCompleted"));

            Observable.Empty<int>()
                      .FirstOrDefault() //空でもエラーにはならない（0が出力される）
                      .Subscribe( x => Debug.Log(x),
                                 () => Debug.Log("OnCompleted"));

            Debug.Log("--- last ---");

            var array2 = new[]{1, 3, 4, 7, 2, 5, 9};

            array2.ToObservable()
                  .Last()       //最後のやつだけ
                  .Subscribe( x => Debug.Log(x),
                             ex => Debug.LogError(ex),
                             () => Debug.Log("OnCompleted"));

            Debug.Log("--- single ---");

            Observable.Range(0, 10)
                      .Single( x => x == 5)     //5のみ通過させる
                      .Subscribe( x => Debug.Log(x),
                                 ex => Debug.LogError(ex),
                                 () => Debug.Log("OnCompleted"));

            Observable.Range(0, 10)
                      .Single( x => x%2 == 0)     //2つ以上通過した場合はエラーになる
                      .Subscribe( x => Debug.Log(x),
                                 ex => Debug.LogError(ex),
                                 () => Debug.Log("OnCompleted"));

            Debug.Log("--- skip ---");

            Observable.Range(0, 10)
                      .Skip(3)  //最初の3個を無視
                      .Subscribe( x => Debug.Log(x));

            Debug.Log("--- skip while ---");

            array2.ToObservable()
                  .SkipWhile( x => x < 5)  //5より小さい間は無視（一旦外れればそれ以降は全部通す） 
                  .Subscribe( x => Debug.Log(x));

            Debug.Log("--- skip until ---");

            //カメラに描画されると発行されるイベント
            var onBecameVisible = this.OnBecameVisibleAsObservable();

            //カメラに描画されたタイミングから移動を開始
            this.UpdateAsObservable()
                .SkipUntil(onBecameVisible)
                .Subscribe( _ => {
                    this.transform.position += Vector3.forward * Time.deltaTime;
                });

            Debug.Log("--- take ---");

            array2.ToObservable()
                  .Take(3)  //最初から3つ
                  .Subscribe( x => Debug.Log(x),
                                 ex => Debug.LogError(ex),
                                 () => Debug.Log("OnCompleted"));

            Debug.Log("--- take while ---");

            array2.ToObservable()
                  .TakeWhile( x => x < 5)  //5より小さい間は通過させる（一度でも5以上になったら終了）
                  .Subscribe( x => Debug.Log(x),
                             () => Debug.Log("OnCompleted"));

            Debug.Log("--- take until ---");

            //このgameobjectがdestroyされるまでカウントアップし続ける
            Observable.Interval(TimeSpan.FromSeconds(1))
                      .TakeUntil(this.OnDestroyAsObservable())
                      .Subscribe( x => Debug.Log(x));

            Debug.Log("--- take until destroy ---");

            //このgameobjectがdestroyされるまでカウントアップし続ける
            Observable.Interval(TimeSpan.FromSeconds(1))
                      .TakeUntilDestroy(this.gameObject)
                      .Subscribe( x => Debug.Log(x));

            Debug.Log("--- take last ---");

            array2.ToObservable()
                  .TakeLast(3)  //最後から3つ
                  .Subscribe( x => Debug.Log(x),
                                 ex => Debug.LogError(ex),
                                 () => Debug.Log("OnCompleted"));

            Debug.Log("--- throttle ---");

            //gameobjectが静止するまでまってからその座標を取り出す
            this.transform.ObserveEveryValueChanged( x => x.position)
                          .Throttle(TimeSpan.FromSeconds(1))
                          .Subscribe( x => Debug.Log(x));

            Debug.Log("--- throttle first ---");

            //zキーが押しっぱなしになっているときに0.5s間隔でメッセージを発行する
            this.UpdateAsObservable()
                .Where( _ => Input.GetKey(KeyCode.Z))
                .ThrottleFirst(TimeSpan.FromSeconds(0.5f))  //1メッセージ通過のたびに0.5s遮断する
                .Subscribe( _ => Debug.Log("pressed Z key"));

            Debug.Log("---  ---");

        }

        // Update is called once per frame
        void Update()
        {
            
        }
    }
   
}