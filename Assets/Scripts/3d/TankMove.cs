using UnityEngine;

public class TankMove : MonoBehaviour
{
    [Header("Tracks")]
    [SerializeField] private Track _left;
    [SerializeField] private Track _right;

    [Header("Movement settings")]
    [SerializeField] private float _motorTorque = 1500f;
    [SerializeField] private float _turnTorque = 2000f; 
    [SerializeField] private float _brakeTorque = 3000f;
    [SerializeField] private float _inputDeadzone = 0.1f;

    public void Drive(float forward, float steer)
    {
        bool isTurning = steer != 0f;
        float baseTorque = (forward == 0f) ? _turnTorque : _motorTorque;
        float leftSpeed = Mathf.Clamp(forward + steer, -1f, 1f);
        float rightSpeed = Mathf.Clamp(forward - steer, -1f, 1f);
        if (Mathf.Abs(forward) < _inputDeadzone) forward = 0f;
        if (Mathf.Abs(steer) < _inputDeadzone) steer = 0f;
        if (forward == 0f && steer == 0f)
        {
            _left.SetTurning(isTurning);
            _right.SetTurning(isTurning);
            Stop();
            return;
        }

        _left.SetTurning(isTurning);
        _right.SetTurning(isTurning);

        _left.Move(leftSpeed * baseTorque);
        _right.Move(rightSpeed * baseTorque);
    }

    public void Stop()
    {
        _left.Stop(_brakeTorque);
        _right.Stop(_brakeTorque);
    }
}