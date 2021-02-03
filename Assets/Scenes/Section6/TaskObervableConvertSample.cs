using System;
using UnityEngine;
using System.Threading;
using System.Threading.Tasks;
using UniRx;

namespace Samples.Section6
{
    public class TaskObervableConvertSample : MonoBehaviour
    {
        public IObservable<T> ToObservable<T>(Task<T> task)
        {
            return task.ToObservable();
        }

        public Task<T> ToTask<T>(IObservable<T> observable)
        {
            //長さが1固定ならこれでok
            return observable.ToTask();
        }

        public Task<T> ToTask2<T>(IObservable<T> observable)
        {
            //Observableから変換されたTaskは、元のObservableのOnCompletedメッセージが発行されたタイミングで完了します。
            //もし長さが1とは限らないときは、onCompleteが発行される条件を明示しないといけない
            return observable.Take(1).ToTask();
        }
    }
}
