using System.Collections.Generic;
using UnityEngine;

public class Track : MonoBehaviour
{
    [Header("Rinks")]
    [SerializeField] private List<WheelCollider> rinks;
    [Header("Rinks settings")]
    [SerializeField] private float _stiffnessFactor = 0.06f;
    private void Awake()
    {
        Init();
    }
    public void Move(float motorTorque)
    {
        foreach (var wheel in rinks) 
        { 
            wheel.motorTorque = motorTorque;
        }
    }
    public void Stop(float brakeTorque)
    {
        foreach (var wheel in rinks)
        {
            wheel.brakeTorque = brakeTorque;
        }
    }
    private void Init()
    {
        foreach (var wheel in rinks)
        {
            SetWheelSidewaysStiffness(wheel,_stiffnessFactor);
        }
    }
    private void SetWheelSidewaysStiffness(WheelCollider wheel, float factor)
    {
        WheelFrictionCurve friction = wheel.sidewaysFriction;
        friction.stiffness = factor;
        wheel.sidewaysFriction = friction;
    }

}
