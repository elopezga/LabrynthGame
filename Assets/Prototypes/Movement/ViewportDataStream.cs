using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UniRx;

namespace Labrynth.Prototype
{
    [CreateAssetMenu(fileName="ViewportDataStream", menuName="Prototype/ViewPortDataStream")]
    public class ViewportDataStream : ScriptableObject
    {
        public Vector3ReactiveProperty Forward = new Vector3ReactiveProperty(Vector3.zero);
    }
}
