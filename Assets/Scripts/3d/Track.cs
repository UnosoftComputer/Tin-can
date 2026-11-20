using System.Collections.Generic;
using UnityEngine;

public class Track : MonoBehaviour
{
    [Header("Wheels")]
    [SerializeField] private List<WheelCollider> _wheels = new List<WheelCollider>();

    [Header("Friction settings")]
    [SerializeField] private float _moveStiffness = 1.5f;
    [SerializeField] private float _turnStiffness = 0.06f;

    private bool _isTurning;

    private void Awake()
    {
        ApplySidewaysFriction(_moveStiffness);
    }

    public void Move(float motorTorque)
    {
        foreach (var wheel in _wheels)
        {
            wheel.brakeTorque = 0f;         
            wheel.motorTorque = motorTorque;
        }
    }

    public void Stop(float brakeTorque)
    {
        foreach (var wheel in _wheels)
        {
            wheel.motorTorque = 0f;
            wheel.brakeTorque = brakeTorque;
        }
    }

    public void SetTurning(bool isTurning)
    {
        if (_isTurning == isTurning) return;
        _isTurning = isTurning;
        ApplySidewaysFriction(isTurning ? _turnStiffness : _moveStiffness);
    }

    private void ApplySidewaysFriction(float stiffness)
    {
        foreach (var wheel in _wheels)
        {
            var friction = wheel.sidewaysFriction;
            friction.stiffness = stiffness;
            wheel.sidewaysFriction = friction;
        }
    }
}