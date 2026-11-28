using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

public class SteeringWheel : MonoBehaviour,
    IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    [SerializeField] private float maxAngle = 90f;
    [SerializeField] private TweenSettings returnTween = new(0.35f, Ease.OutBack);

    public float Value => angle / maxAngle;   

    private float angle, lastPointerAngle;
    private Camera cam;
    private Tween tween;

    void Awake() => cam = Camera.main;

    float PointerAngle(PointerEventData e)
    {
        Vector2 d = (Vector2)cam.ScreenToWorldPoint(e.position)
                  - (Vector2)transform.position;
        return Mathf.Atan2(d.y, d.x) * Mathf.Rad2Deg;
    }

    public void OnPointerDown(PointerEventData e)
    {
        tween?.Kill();
        lastPointerAngle = PointerAngle(e);
    }

    public void OnDrag(PointerEventData e)
    {
        float a = PointerAngle(e);
        SetAngle(angle + Mathf.DeltaAngle(lastPointerAngle, a));
        lastPointerAngle = a;
    }

    public void OnPointerUp(PointerEventData e)
    {
        tween?.Kill();
        tween = DOTween.To(() => angle, SetAngle, 0f, returnTween.duration)
                       .SetEase(returnTween.ease)
                       .SetTarget(this);
    }

    void SetAngle(float a)
    {
        angle = Mathf.Clamp(a, -maxAngle, maxAngle);
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    void OnDestroy() => tween?.Kill();
}