using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

namespace Labrynth.Prototype
{
    public class JoystickController : MonoBehaviour
    {
        [SerializeField]
        private JoystickDataStream _dataStream;

        public void Awake()
        {
            this.FixedUpdateAsObservable()
                .Select(CaptureJoystick)
                .Where(JoystickStateNotEqual)
                .Subscribe(SaveJoystickState);
        }

        private Vector2 CaptureJoystick(Unit _)
        {
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");

            return new Vector2 (horizontal, vertical);
        }

        private bool JoystickStateNotEqual(Vector2 joystick)
        {
            return !joystick.Equals(_dataStream.Joystick.Value);
        }

        private void SaveJoystickState(Vector2 joystickState)
        {
            _dataStream.Joystick.SetValueAndForceNotify(joystickState);
        }        
    }
}

