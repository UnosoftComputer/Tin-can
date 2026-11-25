using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Tank : MonoBehaviour
{
    [SerializeField] private Slider whell;
    [SerializeField] private Slider lever;
    [SerializeField] private Vector2 playerPosition;
    private TankGun _gun;
    private TankMove _move;
    private void Awake()
    {
        _move = new TankMove(1, 0.5f, 15);
        _gun = new TankGun();
        _gun.alpha = 10;
        _gun.beta = -90;
    }
    
    private void Shoot(InputAction.CallbackContext obj)
    {
        Debug.Log(new Vector2(_gun.GetCoordinates().y + playerPosition.x, -_gun.GetCoordinates().x + playerPosition.y));
    }
   
    private void Update()
    {
        Vector2 input = new Vector2(whell.value, lever.value);
        transform.Rotate(0f, 0f, _move.Rotate(input, 0.1f), Space.Self);
        transform.Translate(_move.Move(input, 0.1f), Space.Self);
    }


}



