using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Tank : MonoBehaviour
{
    [SerializeField] private TankInput _tankIpuntSystem;
    [SerializeField] private Transform aim;
    private TankGun _gun;
    private TankMoveAlt _move;
    private void Awake()
    {
        _move = new TankMoveAlt(1, 0.5f, 15);
        _gun = new TankGun(6, 9.81f, -90, 45);

    }
    
    private void Shoot()
    {
        _gun.beta = transform.rotation.eulerAngles.z;
        aim.position = new Vector3(_gun.GetCoordinates().y + transform.position.x, _gun.GetCoordinates().x + transform.position.y, 0);
    }
   
    private void Update()
    {
        _gun.alpha += _tankIpuntSystem.GetGunValue().x;
        Vector2 input = _tankIpuntSystem.GetMoveValue();
        transform.Rotate(0f, 0f, _move.Rotate(input.x, 0.1f), Space.Self);
        transform.Translate(_move.Move(input.y, 0.1f), Space.Self);

        Shoot();
    }


}



