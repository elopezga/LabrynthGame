using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UniRx;

namespace Labrynth.Prototype
{
    [RequireComponent(typeof(BoardMovementFromInputDataTransformer))]
    public class BoardMovementFromInputService : MonoBehaviour
    {
        private BoardMovementFromInputDataTransformer _dataTransformer;
        public void Awake()
        {
            _dataTransformer = GetComponent<BoardMovementFromInputDataTransformer>();

            _dataTransformer.Joystick.Subscribe(joystick => {
                Debug.Log(joystick);
            });
        }
    }
}

