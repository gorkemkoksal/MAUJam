using System;
using DG.Tweening;
using UnityEngine;

namespace Core.Menu.Popup
{
    [CreateAssetMenu(fileName = "PopupSettings", menuName = "MAU_Jam/Popup", order = 0)]
    public class PopupSettings : ScriptableObject
    {
        public AnimationSet<Vector2> scale;
        public AnimationSet<float> alpha;
        public AnimationSet<float> blackout;
    }

    [Serializable]
    public class AnimationSet<T>
    {
        public AnimationValues<T> show, hide;
    }

    [Serializable]
    public class AnimationValues<T>
    {
        public T initial;
        public T final;
        public float duration;
        public Ease ease;
    }
}