using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

namespace Labrynth.Prototype
{
    public class PlatformDataPublisher : MonoBehaviour
    {
        [SerializeField]
        private PlatformDataStream _dataStream;

        private void Awake()
        {
            this.FixedUpdateAsObservable()
                .Subscribe(_ => {
                    _dataStream.PlatformNormal.SetValueAndForceNotify(transform.up);
                });
        }
    }
}