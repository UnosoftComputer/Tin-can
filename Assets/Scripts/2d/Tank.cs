using UnityEngine;
using UnityEngine.InputSystem;

public class Tank : MonoBehaviour
{
    [SerializeField] private HeightCarver _heightCarver;
    [SerializeField] private Vector2 playerPosition;
    private InputSystem_Actions _inputActions;
    private TankGun _gun;
    private void Awake()
    {
        _inputActions = new InputSystem_Actions();
        _gun = new TankGun();
        _gun.alpha = 10;
        _gun.beta = -90;
    }
    private void OnEnable() 
    {
        _inputActions.Player.Enable();
        //_inputActions.Player.Attack.performed += Shoot;
    }
    private void OnDisable() 
    {
        //_inputActions.Player.Attack.performed -= Shoot;
        _inputActions.Player.Disable(); 
    }
    
    private void Shoot()//InputAction.CallbackContext obj)
    {
        _heightCarver.ApplyForce(new Vector2(_gun.GetCoordinates().y + playerPosition.x, -_gun.GetCoordinates().x + playerPosition.y));
        Debug.Log(new Vector2(_gun.GetCoordinates().y + playerPosition.x, -_gun.GetCoordinates().x + playerPosition.y));
    }
    private void Update()
    {
        //_gun.alpha = _gun.alpha < 90 ? _gun.alpha + 0.05f : _gun.alpha;
        _gun.beta = _gun.beta < 90 ? _gun.beta + 0.05f : _gun.beta;
        Shoot();
    }


}
