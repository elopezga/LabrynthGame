using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UniRx;

namespace Labrynth.Prototype
{
    public class BoardMovementFromInputDataTransformer : MonoBehaviour
    {
        [SerializeField]
        private JoystickDataStream _joystickDataStream;

        [SerializeField]
        private ViewportDataStream _viewportDataStream;

        public ReactiveProperty<Vector2> Joystick = new Vector2ReactiveProperty(Vector2.zero);
        public ReactiveProperty<Vector3> CameraForward = new Vector3ReactiveProperty(Vector3.zero);

        private void Awake()
        {
            Joystick = _joystickDataStream.Joystick;
            CameraForward = _viewportDataStream.Forward;
        }
    }
}

