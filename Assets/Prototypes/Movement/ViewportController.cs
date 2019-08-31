using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

namespace Labrynth.Prototype
{
    [RequireComponent(typeof(Camera))]
    public class ViewportController : MonoBehaviour
    {
        [SerializeField]
        private ViewportDataStream _dataStream;

        private Camera _camera;

        public void Awake()
        {
            _camera = GetComponent<Camera>();

            this.FixedUpdateAsObservable()
                .Select(_ => _camera.transform.forward)
                .Subscribe(forward => _dataStream.Forward.SetValueAndForceNotify(forward));
        }
    }
}