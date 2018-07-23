using UnityEngine;

namespace Raskulls.Variables
{
    [CreateAssetMenu(fileName ="NewIntVariable",menuName = "Raskulls/Variables/IntVariable")]
    public class IntVariable : ScriptableObject
    {
#if UNITY_EDITOR
        [Multiline]
        public string DeveloperDescription = "";
#endif
        public int Value;
    }
}