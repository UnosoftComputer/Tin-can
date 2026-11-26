using UnityEngine;

public class TankMove 
{
    #region PRIVATE_VARIBLES
    private float _forwardSpeed;
    private float _backwardSpeed;
    private float _rotateSpeed;
    #endregion
    #region PUBLIC_VARIBLES
    public float forwardSpeed
    {
        get { return _forwardSpeed; }
        private set { _forwardSpeed = value > 0 ? value : 1; }
    }
    public float backwardSpeed
    {
        get { return _backwardSpeed; }
        private set { _backwardSpeed = value > 0 ? value : 1; }
    }
    public float rotateSpeed
    {
        get { return _rotateSpeed; }
        private set { _rotateSpeed = value > 0 ? value : 1; }
    }
    #endregion
    public TankMove(float _forwardSpeed, float _backwardSpeed, float _rotateSpeed)
    {
        forwardSpeed = _forwardSpeed;
        backwardSpeed = _backwardSpeed;
        rotateSpeed = _rotateSpeed;
    }
    public Vector2 Move(Vector2 Input, float trigger)
    {
        float left = ApplyDeadzone(Input.x, trigger);
        float right = ApplyDeadzone(Input.y, trigger);
        float throttle = (left + right) * 0.5f;
        float speed = throttle >= 0f ? forwardSpeed : backwardSpeed;
        return Vector2.up * (throttle * speed * Time.deltaTime);
    }


    public float Rotate(Vector2 Input, float trigger)
    {
        float left = ApplyDeadzone(Input.x, trigger);
        float right = ApplyDeadzone(Input.y, trigger);
        float steering = (left - right) * 0.5f;
        return steering * rotateSpeed * Time.deltaTime;
    }

    private static float ApplyDeadzone(float value, float trigger)
    {
        return Mathf.Abs(value) < trigger ? 0f : value;
    }
}
