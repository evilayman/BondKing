using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Raskulls.Variables
{
    [CreateAssetMenu(fileName = "NewVector3Variable", menuName = "Raskulls/Variables/TransformVariable")]
    public class TransformVariable : ScriptableObject
    {
        public Transform Value;
    }
}
