using System;
using System.IO;
using UnityEngine;

using UniRx;

namespace Samples.Section3.FactoryMethods
{
    public class StartSample : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
           Observable.Start( () => {
                using (var r = new StreamReader(Application.dataPath + "/Resources/Text/data.txt"))
                {
                    // Debug.Log(Application.dataPath + "/Resources/Text/data.txt");
                    return r.ReadToEnd();
                }
            }).Subscribe( x => Debug.Log(x) );

        }

        // Update is called once per frame
        void Update()
        {
            
        }
    }
}