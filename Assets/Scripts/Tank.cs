using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Tank : MonoBehaviour
{
    [SerializeField] private PolygonCollider2D boundsSource;
    [SerializeField] private TankInput _tankIpuntSystem;
    [SerializeField] private Transform aim;
    private TankGun _gun;
    private TankMoveAlt _move;
    private TankBounds _bounds;
    private void Awake()
    {
        _move = new TankMoveAlt(0.1f, 0.05f, 15);
        _gun = new TankGun(4, 9.81f, -90, 45);
        _bounds = new TankBounds(GetWorldPoints(boundsSource), 0.2f);
    }
    private Vector2[] GetWorldPoints(PolygonCollider2D source)
    {
        if (source == null) return new Vector2[0];

        Vector2[] local = source.GetPath(0);
        Vector2[] world = new Vector2[local.Length];
        for (int i = 0; i < local.Length; i++)
            world[i] = source.transform.TransformPoint(local[i] + source.offset);
        return world;
    }
    public void Shoot()
    {
        aim.position = new Vector3(_gun.GetCoordinates().y + transform.position.x, _gun.GetCoordinates().x + transform.position.y, 0);
    }
   
    private void Update()
    {
        _gun.alpha = _tankIpuntSystem.GetGunValue().x;
        _gun.beta = _tankIpuntSystem.GetGunValue().y+ transform.rotation.eulerAngles.z;
        Vector2 input = _tankIpuntSystem.GetMoveValue();
        Vector2 before = transform.position;

        transform.Rotate(0f, 0f, _move.Rotate(input.x, 0.1f), Space.Self);
        transform.Translate(_move.Move(input.y, 0.1f), Space.Self);

        Vector2 after = _bounds.Constrain(before, transform.position);
        transform.position = new Vector3(after.x, after.y, transform.position.z);

        
    }


}



