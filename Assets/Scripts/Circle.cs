using UnityEngine;
using UnityEngine.EventSystems;

public class Circle : MonoBehaviour,
    IPointerDownHandler, IDragHandler
{

    [SerializeField] private float influenceCoefficient = 90f;
    public float Value => -angle * (influenceCoefficient/1000);   

    private float angle, lastPointerAngle;
    private Camera cam;

    void Awake() => cam = Camera.main;

    float PointerAngle(PointerEventData e)
    {
        Vector2 d = (Vector2)cam.ScreenToWorldPoint(e.position)
                  - (Vector2)transform.position;
        return Mathf.Atan2(d.y, d.x) * Mathf.Rad2Deg;
    }

    public void OnPointerDown(PointerEventData e)
    {
        lastPointerAngle = PointerAngle(e);
    }

    public void OnDrag(PointerEventData e)
    {
        float a = PointerAngle(e);
        SetAngle(angle + Mathf.DeltaAngle(lastPointerAngle, a));
        lastPointerAngle = a;
    }


    void SetAngle(float a)
    {
        angle = a;                                   
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

}