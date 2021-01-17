using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

using UniRx;

namespace Samples.Section5.MessageBrokers
{
    internal class AsyncMessageBrokerSample : MonoBehaviour
    {
        [SerializeField] private Text text;

        // Start is called before the first frame update
        void Start()
        {
            var asyncMessageBroker = AsyncMessageBroker.Default;
            IAsyncMessagePublisher publisher = asyncMessageBroker;
            IAsyncMessageReceiver receiver = asyncMessageBroker;

            //購読者1. 受け取ったstringを3s表示した後に消す
            receiver.Subscribe<string>(x => {
                this.text.text = x;

                //3s後にメッセージが消えるまで完了しない
                return Observable.Timer(TimeSpan.FromSeconds(3))
                                 .ForEachAsync(_ => this.text.text = "");
            }).AddTo(this);

            //購読者2. 受け取ったstringを表示して即終了
            receiver.Subscribe<string>(x => {
                Debug.Log(x);

                return Observable.Return(Unit.Default);
            }).AddTo(this);

            var cancellationToken = this.GetCancellationTokenOnDestroy();
            this.PublishMessageAsync(publisher, cancellationToken).Forget();
        }

        private async UniTaskVoid PublishMessageAsync(IAsyncMessagePublisher publisher, CancellationToken ct)
        {
            await publisher.PublishAsync("hello").ToUniTask(cancellationToken: ct);
            await publisher.PublishAsync("world").ToUniTask(cancellationToken: ct);
            await publisher.PublishAsync("bye").ToUniTask(cancellationToken: ct);
        }
    }
}
