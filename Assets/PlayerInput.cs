using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    
    [SerializeField] private TankMove _tankMove;

    private Vector2 _moveInputVector;
    private InputSystem_Actions _inputActions;
    private void OnEnable()
    {
        _inputActions.Enable();
    }
    private void OnDisable()
    {
        _inputActions.Disable();
    }
    void Awake()
    {
        _inputActions = new InputSystem_Actions();
    }
    private void Update()
    {
        _moveInputVector = _inputActions.Player.Move.ReadValue<Vector2>();
        _tankMove.Left(_moveInputVector.x);
        _tankMove.Right(_moveInputVector.y);
    }



}
