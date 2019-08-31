using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UniRx;

namespace Labrynth.Prototype
{
    [CreateAssetMenu(fileName="JoystickDataStream", menuName="Prototype/JoystickDataStream")]
    public class JoystickDataStream : ScriptableObject
    {
        public Vector2ReactiveProperty Joystick = new Vector2ReactiveProperty(Vector2.zero);
    }
}

