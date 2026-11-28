using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Collider2D))]
public class FireHandle : MonoBehaviour, IPointerClickHandler
{
    #region PRIVATE_VARIBLES
    [SerializeField] private float _pullDistance = 0.5f;
    [SerializeField] private TweenSettings _pullTween = new TweenSettings(0.1f, Ease.OutQuad);
    [SerializeField] private TweenSettings _returnTween = new TweenSettings(0.3f, Ease.OutBack);
    [SerializeField] private UnityEvent _onShoot;

    private bool _isPulled;
    private Vector3 _restPosition;
    private float _shift;
    private Tween _tween;
    #endregion

    #region PUBLIC_VARIBLES
    public float pullDistance
    {
        get { return _pullDistance; }
        set { _pullDistance = value > 0 ? value : 0.5f; }
    }
    public bool isPulled
    {
        get { return _isPulled; }
        private set { _isPulled = value; }
    }
    public UnityEvent OnShoot
    {
        get { return _onShoot; }
    }
    #endregion

    private void Awake()
    {
        _restPosition = transform.localPosition;
        _shift = 0f;
    }

    public void OnPointerClick(PointerEventData e)
    {
        Fire();
    }

    public void Fire()
    {
        if (isPulled) return;
        isPulled = true;

        _tween?.Kill();
        _tween = DOTween.To(() => _shift, SetShift, pullDistance, _pullTween.duration)
                        .SetEase(_pullTween.ease)
                        .SetTarget(this)
                        .OnComplete(OnPulled);
    }

    private void OnPulled()
    {
        _onShoot?.Invoke();

        _tween?.Kill();
        _tween = DOTween.To(() => _shift, SetShift, 0f, _returnTween.duration)
                        .SetEase(_returnTween.ease)
                        .SetTarget(this)
                        .OnComplete(OnReturned);
    }

    private void OnReturned()
    {
        isPulled = false;
    }

    private void SetShift(float s)
    {
        _shift = s;
        transform.localPosition = _restPosition + Vector3.down * _shift;
    }

    private void OnDestroy()
    {
        _tween?.Kill();
    }
}