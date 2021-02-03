using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Threading;
using System.Threading.Tasks;

namespace Samples.Section6
{
    public class SynchronizationContextSample1 : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            Debug.Log("main thread ID: " + this.GetCurrentThreadId());

            this.DoAsync();
        }

        private int GetCurrentThreadId()
        {
            return Thread.CurrentThread.ManagedThreadId;
        }

        private async Task DoAsync()
        {
            Debug.Log("before await: " + this.GetCurrentThreadId());

            await Task.Run(() => 
            {
                Debug.Log("task.run: " + this.GetCurrentThreadId());
            });
            // .ConfigureAwait(false);  //Taskに対してこれを指定すれば、await後もスレッドプールのコンテキストを維持する(SynchronizationContextを無視する)

            Debug.Log("after await: " + this.GetCurrentThreadId());

        }
    }    
}

