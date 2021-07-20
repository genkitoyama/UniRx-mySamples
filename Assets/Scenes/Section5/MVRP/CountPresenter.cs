using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using UniRx;

namespace Samples.Section5.MVRP
{
    public class CountPresenter : MonoBehaviour
    {
        [SerializeField]
        private CountModel countModel;

        [SerializeField]
        private InputField inputField;
        [SerializeField]
        private Button upButton;
        [SerializeField]
        private Button downButton;
        [SerializeField]
        private Slider slider;

        /// <summary>
        /// Start is called on the frame when a script is enabled just before
        /// any of the Update methods is called the first time.
        /// </summary>
        private void Start()
        {
            //model => view
            this.countModel.Current
                .Subscribe(x => {
                    this.inputField.text = x.ToString();
                    this.slider.value = x;
                })
                .AddTo(this);

            //view => model
            //inputfield
            this.inputField.OnValueChangedAsObservable()
            .Select(x => {
                var isSucceeded = int.TryParse(x, out var value);
                return (isSucceeded, value);
            })
            .Where(x => x.isSucceeded)
            .Subscribe(x => this.countModel.UpdateCount(x.value))
            .AddTo(this);

            //slider
            this.slider.OnValueChangedAsObservable()
            .Subscribe(x => this.countModel.UpdateCount((int)x))
            .AddTo(this);

            //button
            Observable.Merge(
                this.upButton.OnClickAsObservable().Select(_ => +1),
                this.downButton.OnClickAsObservable().Select(_ => -1)
            )
            .Subscribe(value => {
                this.countModel.UpdateCount(this.countModel.Current.Value + value);
            })
            .AddTo(this);
        }
    }

}