using System;
using DG.Tweening;

[Serializable]
public struct TweenSettings
{
    public float duration;
    public Ease ease;

    public TweenSettings(float duration, Ease ease)
    { this.duration = duration; this.ease = ease; }
}