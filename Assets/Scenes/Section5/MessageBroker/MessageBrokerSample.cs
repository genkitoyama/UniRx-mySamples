using System;
using UnityEngine;

using UniRx;

namespace Samples.Section5.MessageBrokers
{
    public class MessageBrokerSample : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            var messageBroker = UniRx.MessageBroker.Default;
            IMessagePublisher publisher = messageBroker;
            IMessageReceiver receiver = messageBroker;

            //型を指定して購読できる
            receiver.Receive<int>().Subscribe(x => Debug.Log("int message: " + x)).AddTo(this);
            receiver.Receive<string>().Subscribe(x => Debug.Log("string message: " + x)).AddTo(this);
            receiver.Receive<Exception>().Subscribe(x => Debug.Log("Exception: " + x)).AddTo(this);

            //任意の型を発行できる
            publisher.Publish(123);
            publisher.Publish("hello");
            publisher.Publish(new Exception("exception"));
        }

        // Update is called once per frame
        void Update()
        {
            
        }
    }
}
