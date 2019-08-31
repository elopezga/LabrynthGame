using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UniRx;

namespace Labrynth.Prototype
{
    public class BoardMovementFromInputDataTransformer : MonoBehaviour
    {
        [SerializeField]
        private JoystickDataStream _dataStream;

        public ReactiveProperty<Vector2> Joystick = new Vector2ReactiveProperty(Vector2.zero);

        private void Awake()
        {
            Joystick = _dataStream.Joystick;
        }
    }
}

