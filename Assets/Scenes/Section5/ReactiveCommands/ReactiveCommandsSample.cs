using System;
using UnityEngine;
using UnityEngine.UI;

using UniRx;

namespace Samples.Section5.ReactiveCommands
{
    public class ReactiveCommandsSample : MonoBehaviour
    {
        [SerializeField] private Toggle toggle;
        [SerializeField] private Button button;

        // Start is called before the first frame update
        void Start()
        {
            //toggleに連動
            var reactiveCommand = this.toggle.OnValueChangedAsObservable()
                                             .ToReactiveCommand(false);

            //butonのクリックとReactiveCommand<Unit>.Execute()呼び出し
            //ReactiveCommand<Unit>.CanExecute と Button.Interactable を管理
            reactiveCommand.BindTo(this.button);
            reactiveCommand.Subscribe(_ => Debug.Log("clicked"));

            // reactiveCommand.BindToOnClick(this.button, _ => Debug.Log("clicked"));
        }

        // Update is called once per frame
        void Update()
        {
            
        }
    }
}
