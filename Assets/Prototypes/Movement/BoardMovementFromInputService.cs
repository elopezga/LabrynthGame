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

            _dataTransformer.Joystick.Subscribe(joystick => {
                transform.rotation = Quaternion.AngleAxis(Joystick2RotationAngle(joystick.x), Vector3.left)
                    * Quaternion.AngleAxis(Joystick2RotationAngle(joystick.y), Vector3.forward);
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

