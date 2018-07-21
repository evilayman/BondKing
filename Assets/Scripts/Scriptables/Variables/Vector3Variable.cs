using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Raskulls.Variables
{
    [CreateAssetMenu(fileName = "NewVector3Variable", menuName = "Raskulls/Variables/Vector3Variable")]
    public class Vector3Variable : ScriptableObject
    {
        public Vector3 Value;
    }
}
