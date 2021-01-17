using System;
using UnityEngine;

using UniRx;

namespace Samples.Section5.MessageBrokers
{
    //Events
    public interface IEnemyEvent
    {
        int EnemyId { get; }
    }
    public struct EnemyDeadEvent : IEnemyEvent
    {
        public int EnemyId { get; private set; }

        public EnemyDeadEvent(int enemyId) : this()
        {
            EnemyId = enemyId;
        }
    }

    public interface IPlayerEvent
    {
        int PlayerId { get; }
    }
    public struct PlayerDeadEvent : IPlayerEvent
    {
        public int PlayerId { get; private set; }

        public PlayerDeadEvent(int id)
        {
            PlayerId = id;
        }
    }

    //Typed Message Broker Generic
    public abstract class MessageBroker<TBase> : MessageBroker
    {
        public new void Publish<T>(T meessage) where T : TBase
        {
            base.Publish(meessage);
        }

        public new IObservable<T> Receive<T>() where T : TBase
        {
            return base.Receive<T>();
        }
    }

    public class EnemyEventMessageBroker : MessageBroker<IEnemyEvent>
    {
    }

    public class PlayerEventMessageBroker : MessageBroker<IPlayerEvent>
    {
    }

    public class TypedMessageBrokerSample : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            //IEnemyEventについてのみ扱える
            var enemyMessageBroker = new EnemyEventMessageBroker().AddTo(this);
            enemyMessageBroker.Receive<EnemyDeadEvent>().Subscribe(x => Debug.Log(x + " :enemy died"));
            enemyMessageBroker.Publish(new EnemyDeadEvent(1));

            //IPlayerEventについてのみ扱える
            var playerMessageBroker = new PlayerEventMessageBroker().AddTo(this);
            playerMessageBroker.Receive<PlayerDeadEvent>().Subscribe(x => Debug.Log(x + " :player died"));
            playerMessageBroker.Publish(new PlayerDeadEvent(1));
        }

        // Update is called once per frame
        void Update()
        {
            
        }
    }
}
