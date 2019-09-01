using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace Labrynth.Prototype
{
    [CreateAssetMenu(fileName="PlatformDataStream", menuName="Prototype/PlatformDataStream")]
    public class PlatformDataStream : ScriptableObject
    {
        public Vector3ReactiveProperty PlatformNormal = new Vector3ReactiveProperty(Vector3.zero);
    }
}