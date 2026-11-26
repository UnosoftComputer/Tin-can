using System;
using UnityEngine;
using UnityEngine.InputSystem;


public class TankInput : MonoBehaviour
{
    [SerializeField] private SteeringWheel steeringWheel;
    private InputSystem_Actions _inputSystem;
    private void Awake()
    {
        _inputSystem = new InputSystem_Actions();
    }

    void OnEnable()
    {
        _inputSystem.Enable();
    }
    void OnDisable()
    {
        _inputSystem.Disable();
    }
    public Vector2 GetGunValue()
    {
        return new Vector2(_inputSystem.Player.Move.ReadValue<Vector2>().x*0.01f,0);
    }

    public Vector2 GetMoveValue()
    {
        return new Vector2(-steeringWheel.Value, _inputSystem.Player.Move.ReadValue<Vector2>().y);
    }
}
