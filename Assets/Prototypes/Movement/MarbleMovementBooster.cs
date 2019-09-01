using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UniRx;

namespace Labrynth.Prototype
{
    public class MarbleMovementBooster : MonoBehaviour
    {
        [SerializeField]
        private JoystickDataStream _joystickStream;

        [SerializeField]
        private float magnitude;

        private Rigidbody _rigidBody;

        public void Awake()
        {
            _rigidBody = GetComponent<Rigidbody>();

            _joystickStream.Joystick.Subscribe(_ => {
                Vector3 joystick3D = new Vector3(_.x, _.y, 0f);
                // transform.up should be platform normal
                Vector3 velocity = Vector3.ProjectOnPlane(joystick3D, transform.up);
                Debug.DrawRay(transform.position, velocity * magnitude, Color.red);
                _rigidBody.AddForce(velocity * magnitude);
            });
        }
    }
}