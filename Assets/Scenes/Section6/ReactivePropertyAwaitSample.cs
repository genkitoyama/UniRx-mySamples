using System;
using UnityEngine;
using System.Threading;
using System.Threading.Tasks;
using UniRx;

namespace Samples.Section6
{
    public class ReactivePropertyAwaitSample : MonoBehaviour
    {
        public readonly BoolReactiveProperty isDeadReactiveProperty = new BoolReactiveProperty(false);

        // Start is called before the first frame update
        void Start()
        {
            _ = this.CheckHealthChangeAsync();
        }

        //死亡フラグが有効になったらgameobjectを削除する
        private async Task CheckHealthChangeAsync()
        {
            await this.isDeadReactiveProperty;
            Destroy(this.gameObject);
        }

        private void OnDestroy()
        {
            //disposeすることで、Awaiterごと破棄される
            this.isDeadReactiveProperty.Dispose();
        }
    }
}