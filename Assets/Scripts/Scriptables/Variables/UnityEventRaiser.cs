using UnityEngine;
using UnityEngine.Events;

namespace Raskulls.Variables
{
    public class UnityEventRaiser : MonoBehaviour
    {
        public UnityEvent OnEnableEvent;

        public void OnEnable()
        {
            OnEnableEvent.Invoke();
        }
    }
}