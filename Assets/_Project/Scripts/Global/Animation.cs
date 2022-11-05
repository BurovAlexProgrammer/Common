using DG.Tweening;
using UnityEngine;

namespace _Project.Scripts.Global
{
    public static class GlobalAnimation
    {
        public static float GetDuration(this AnimationCurve curve)
        {
            return curve[curve.keys.Length - 1].time;
        }

        public static Sequence Reset(this Sequence sequence, bool complete = true)
        {
            sequence?.Kill(complete);
            return DOTween.Sequence();
        }
    }
}