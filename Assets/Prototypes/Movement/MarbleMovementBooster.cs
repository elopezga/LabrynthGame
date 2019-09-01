using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UniRx;
using UniRx.Triggers;

namespace Labrynth.Prototype
{
    public class MarbleMovementBooster : MonoBehaviour
    {
        [SerializeField]
        private JoystickDataStream _joystickStream;

        [SerializeField]
        private PlatformDataStream _platformStream;

        [SerializeField]
        private float _ForceMagnitude;
        [SerializeField]
        private float _MaxSpeed;

        private Rigidbody _rigidBody;

        public void Awake()
        {
            _rigidBody = GetComponent<Rigidbody>();
            
            // Added force should be based on how much you tilt

            IObservable<Vector3> velocity = this.FixedUpdateAsObservable()
                .Select(_ => _rigidBody.velocity);

            velocity.Subscribe(_ => {
                Vector3 joystick3D = new Vector3(_joystickStream.Joystick.Value.x, 0f, _joystickStream.Joystick.Value.y);
                Vector3 force = Vector3.ProjectOnPlane(joystick3D, _platformStream.PlatformNormal.Value);
                float forceMagnitudeBasedOnTilt = Vector3.Cross(Vector3.up, _platformStream.PlatformNormal.Value).magnitude;
                float forceMagnitudeNormalized = Mathf.InverseLerp(0f, 0.2f, forceMagnitudeBasedOnTilt); // <-- Needs to be max angle allowed to tilt
                float magnitude = Mathf.Lerp(0, _MaxSpeed, forceMagnitudeNormalized);
                _rigidBody.AddForce(force * magnitude);

                // Don't exceeed max speed
                _rigidBody.velocity = Vector3.ClampMagnitude(_rigidBody.velocity, _MaxSpeed);

                /* Debug.Log($"Velocity {_rigidBody.velocity.magnitude}");
                Debug.Log($"Magnitude {magnitude}"); */
                Debug.DrawRay(transform.position, force * magnitude);
            });
        }
    }
}