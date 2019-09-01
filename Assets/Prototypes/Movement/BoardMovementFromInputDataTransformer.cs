using System;
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

        //public IObservable<BoardMovementData> BoardMovementDataObservable;
        public ReactiveProperty<BoardMovementData> BoardMovementReactive;

        private void Awake()
        {
            Joystick = _joystickDataStream.Joystick;
            CameraForward = _viewportDataStream.Forward;

            //_viewportDataStream.Right.Subscribe(_ => Debug.Log(_));

            BoardMovementReactive = new ReactiveProperty<BoardMovementData>(new BoardMovementData());

            _joystickDataStream.Joystick.Subscribe(_ => {
                BoardMovementData data = new BoardMovementData();
                data.Joystick = _;
                data.CameraForward = BoardMovementReactive.Value.CameraForward;
                data.CameraRight = BoardMovementReactive.Value.CameraRight;
                BoardMovementReactive.SetValueAndForceNotify(data);
            });

            _viewportDataStream.Forward.Subscribe(_ => {
                BoardMovementData data = new BoardMovementData();
                data.Joystick = BoardMovementReactive.Value.Joystick;
                data.CameraRight = BoardMovementReactive.Value.CameraRight;
                data.CameraForward = _;
                BoardMovementReactive.SetValueAndForceNotify(data);
            });

            _viewportDataStream.Right.Subscribe(_ => {
                BoardMovementData data = new BoardMovementData();
                data.Joystick = BoardMovementReactive.Value.Joystick;
                data.CameraRight = _;
                data.CameraForward = BoardMovementReactive.Value.CameraForward;
                BoardMovementReactive.SetValueAndForceNotify(data);
            });

            // 🚨!!Warning!!🚨 Camera right is not updating BoardMovementReactive so anything
            // that so procedures that depend on CameraForward being updated are not triggered!
        }
    }

    public class BoardMovementData
    {
        public Vector2 Joystick = Vector2.zero;
        public Vector3 CameraForward = Vector3.zero;
        public Vector3 CameraRight = Vector3.zero;
    }
}

