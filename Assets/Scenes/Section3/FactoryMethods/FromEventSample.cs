using System;

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

using UniRx;

namespace Samples.Section3.FactoryMethods
{
    public class FromEventSample : MonoBehaviour
    {
        //オリジナルのEventArgs
        public sealed class MyEventArgs: EventArgs
        {
            public int MyProperty { get; set; }
        }
        
        //MyEventArgsを扱うイベントハンドラ
        private event EventHandler<MyEventArgs> onEvent;

        //intを引数にとるAction
        private event Action<int> callBackAction;

        [SerializeField]
        private Button uiButton;

        private readonly CompositeDisposable disposables = new CompositeDisposable();

        // Start is called before the first frame update
        void Start()
        {
            //FromEventPattern
            //(sender, eventArgs)を両方使ってイベントをIObservable<MyEventArgs>に変換する
            Observable.FromEventPattern<EventHandler<MyEventArgs>, MyEventArgs>(
                h => h.Invoke,
                h => onEvent += h,
                h => onEvent -= h
            )
            .Subscribe().AddTo(disposables);

            //FromEvent, eventArgsのみを使ってイベントをIObservable<MyEventArgs>に変換する
            Observable.FromEvent<EventHandler<MyEventArgs>, MyEventArgs>(
                h => (sender, e) => h(e),
                h => onEvent += h,
                h => onEvent -= h
            )
            .Subscribe().AddTo(disposables);

            //Action<T>を使ったイベントもObservable<T>に変換できる
            Observable.FromEvent<int>(
                h => callBackAction += h,
                h => callBackAction -= h
            )
            .Subscribe().AddTo(disposables);

            //UnityAction
            Observable.FromEvent<UnityAction>(
                h => new UnityAction(h),
                h => uiButton.onClick.AddListener(h),
                h => uiButton.onClick.RemoveListener(h)
            )
            .Subscribe().AddTo(disposables);

            //ただし、UnityEventからObservableに変換する場合は
            //FromEventの糖衣構文として AsObservable が用意されている
            uiButton.onClick.AsObservable().Subscribe(x => Debug.Log("clicked: " + x)).AddTo(disposables);
        }


        private void OnDestroy()
        {
            //破棄されたときにまとめてdispose
            disposables.Dispose();
        }
    }
}
