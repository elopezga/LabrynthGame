using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UniRx;

namespace Labrynth.Prototype
{
    [RequireComponent(typeof(BoardMovementFromInputDataTransformer))]
    public class BoardMovementFromInputService : MonoBehaviour
    {
        [SerializeField]
        private float RotationAngleMax = 8f;

        private BoardMovementFromInputDataTransformer _dataTransformer;
        public void Awake()
        {
            _dataTransformer = GetComponent<BoardMovementFromInputDataTransformer>();

            _dataTransformer.BoardMovementReactive.Subscribe(_ => {
                Vector3 projectedForward = Vector3.ProjectOnPlane(_.CameraForward, Vector3.up);
                Vector3 projectedRight = Vector3.ProjectOnPlane(_.CameraRight, Vector3.up);

                transform.rotation = Quaternion.AngleAxis(Joystick2RotationAngle(_.Joystick.x), -1f * projectedForward)
                    * Quaternion.AngleAxis(Joystick2RotationAngle(_.Joystick.y), projectedRight);
            });
        }

        private float Joystick2RotationAngle(float joystickAxisValue)
        {
            float normal = Mathf.InverseLerp(-1f, 1f, joystickAxisValue);
            float lerped = Mathf.Lerp(-1f * RotationAngleMax, RotationAngleMax, normal);

            return lerped;
        }
    }
}

