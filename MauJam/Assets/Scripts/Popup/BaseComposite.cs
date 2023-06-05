using UnityEngine;

namespace Core._Common.Helpers
{
    public abstract class BaseComposite<T> : MonoBehaviour where T : Component
    {
        private T _target;
        protected T Target
        {
            get
            {
                _target ??= GetComponent<T>();
                return _target;
            }
            set => _target = value;
        }
    }
}