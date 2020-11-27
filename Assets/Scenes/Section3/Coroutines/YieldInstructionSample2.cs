using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using UniRx;

namespace Samples.Section3.Coroutines
{
    public class YieldInstructionSample2 : MonoBehaviour
    {
        [SerializeField]
        private Button moveButton;

        // Start is called before the first frame update
        void Start()
        {
            StartCoroutine(MoveCoroutine());
        }

        private IEnumerator MoveCoroutine()
        {
            while (true)
            {
                //ボタンが押されるまで待つ
                //OnClickAsObservable は長さが無限長なので、Take(1)で長さを1に制限する
                yield return this.moveButton.OnClickAsObservable()
                                            .Take(1)
                                            .ToYieldInstruction();

                var start = Time.time;

                while (Time.time - start <= 1f)
                {
                    this.transform.position += Vector3.forward * Time.deltaTime;
                    yield return null;
                }
            }
        }

        // Update is called once per frame
        void Update()
        {
            
        }
    }
}
