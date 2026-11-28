using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Collider2D))]
public class Breech : MonoBehaviour, IPointerClickHandler
{
    #region PRIVATE_VARIBLES
    [SerializeField] private float _openAngle = 90f;
    [SerializeField] private TweenSettings _openTween = new TweenSettings(0.5f, Ease.OutQuad);
    [SerializeField] private TweenSettings _closeTween = new TweenSettings(0.4f, Ease.InQuad);

    private bool _isOpen;
    private float _closedAngle;
    private float _angle;
    private Tween _tween;
    #endregion

    #region PUBLIC_VARIBLES
    public float openAngle
    {
        get { return _openAngle; }
        set { _openAngle = value != 0 ? value : 90f; }
    }
    public bool isOpen
    {
        get { return _isOpen; }
        private set { _isOpen = value; }
    }
    public event Action<bool> stateChanged;
    #endregion

    private void Awake()
    {
        _closedAngle = transform.localEulerAngles.z;
        _angle = _closedAngle;
    }

    public void OnPointerClick(PointerEventData e)
    {
        Toggle();
    }

    public void Toggle()
    {
        SetOpen(!isOpen);
    }

    public void SetOpen(bool open)
    {
        if (isOpen == open) return;
        isOpen = open;

        TweenSettings settings = open ? _openTween : _closeTween;
        float target = open ? _closedAngle + openAngle : _closedAngle;

        _tween?.Kill();
        _tween = DOTween.To(() => _angle, SetAngle, target, settings.duration)
                        .SetEase(settings.ease)
                        .SetTarget(this);

        stateChanged?.Invoke(open);
    }

    private void SetAngle(float a)
    {
        _angle = a;
        transform.localRotation = Quaternion.Euler(0, 0, _angle);
    }

    private void OnDestroy()
    {
        _tween?.Kill();
    }
}