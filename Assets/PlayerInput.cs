using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] private TankMove _tankMove;

    private InputSystem_Actions _inputActions;
    private Vector2 _moveInputVector;

    private void Awake()
    {
        _inputActions = new InputSystem_Actions();
    }

    private void OnEnable() => _inputActions.Player.Enable();
    private void OnDisable() => _inputActions.Player.Disable();

    private void Update()
    {
        _moveInputVector = _inputActions.Player.Move.ReadValue<Vector2>();
    }

    private void FixedUpdate()
    {
        if (_tankMove == null) return;
        _tankMove.Drive(_moveInputVector.y, _moveInputVector.x);
    }
}