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
                .Select(CaptureForward)
                .Where(CameraForwardNotEqual)
                .Subscribe(forward => _dataStream.Forward.SetValueAndForceNotify(forward));

            this.FixedUpdateAsObservable()
                .Select(_ => _camera.transform.right)
                .Where(_ => !_.Equals(_dataStream.Right.Value))
                .Subscribe(right => _dataStream.Right.SetValueAndForceNotify(right));
        }

        private Vector3 CaptureForward(Unit _)
        {
            return _camera.transform.forward;
        }

        private bool CameraForwardNotEqual(Vector3 cameraForward)
        {
            return !cameraForward.Equals(_dataStream.Forward.Value);
        }

        private void SaveCameraState(Vector3 forwardState)
        {
            _dataStream.Forward.SetValueAndForceNotify(forwardState);
        }
    }
}