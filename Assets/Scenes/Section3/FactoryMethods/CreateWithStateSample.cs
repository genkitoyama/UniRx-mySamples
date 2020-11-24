using System;
using System.Threading.Tasks;
using UnityEngine;

using UniRx;

namespace Samples.Section3.FactoryMethods
{
    public class CreateWithStateSample : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            CreateCountObservable(10).Subscribe(x => Debug.Log(x));
        }

        //指定個数、連続した値を発行するobservableを作って返す
        private IObservable<int> CreateCountObservable(int count)
        {
            if(count <= 0)
            {
                return Observable.Empty<int>();
            }

            return Observable.CreateWithState<int, int>(
                state: count,
                subscribe: (maxCount, observer) => {
                    //maxCountに、countで指定した値が入っている
                    for (int i = 0; i < maxCount; i++)
                    {
                        observer.OnNext(maxCount);
                    }
                    observer.OnCompleted();
                    return Disposable.Empty;
                }
            );
        }

        // Update is called once per frame
        void Update()
        {
            
        }
    }
}
