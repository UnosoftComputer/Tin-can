using UnityEngine;

public class TankMove
{
    #region PRIVATE_VARIBLES
    private float _forwardSpeed;
    private float _backwardSpeed;
    private float _rotateSpeed;
    private float _accelTime;
    private float _coastTime;

    private float _currentSpeed;    
    private float _currentAngular;  
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
    public float accelTime
    {
        get { return _accelTime; }
        set { _accelTime = Mathf.Max(0f, value); }
    }
    public float coastTime
    {
        get { return _coastTime; }
        set { _coastTime = Mathf.Max(0f, value); }
    }
    #endregion

    public TankMove(float _forwardSpeed, float _backwardSpeed, float _rotateSpeed,
                    float accelTime = 2f, float coastTime = 3f)
    {
        forwardSpeed = _forwardSpeed;
        backwardSpeed = _backwardSpeed;
        rotateSpeed = _rotateSpeed;
        this.accelTime = accelTime;
        this.coastTime = coastTime;
    }

    public Vector2 Move(Vector2 Input, float trigger)
    {
        float left = ApplyDeadzone(Input.x, trigger);
        float right = ApplyDeadzone(Input.y, trigger);
        return MoveInternal((left + right) * 0.5f);
    }

    public Vector2 Move(float throttle, float trigger)
    {
        return MoveInternal(ApplyDeadzone(throttle, trigger));
    }

    private Vector2 MoveInternal(float throttle)
    {
        float targetSpeed = throttle * (throttle >= 0f ? forwardSpeed : backwardSpeed);
        float maxSpeed = Mathf.Max(forwardSpeed, backwardSpeed);

        _currentSpeed = Approach(_currentSpeed, targetSpeed, maxSpeed);
        return Vector2.up * (_currentSpeed * Time.deltaTime);
    }
    public float Rotate(Vector2 Input, float trigger)
    {
        float left = ApplyDeadzone(Input.x, trigger);
        float right = ApplyDeadzone(Input.y, trigger);
        return RotateInternal((left - right) * 0.5f);
    }

    public float Rotate(float steering, float trigger)
    {
        return RotateInternal(ApplyDeadzone(steering, trigger));
    }

    private float RotateInternal(float steering)
    {
        float targetAngular = steering * rotateSpeed;

        _currentAngular = Approach(_currentAngular, targetAngular, rotateSpeed);
        return _currentAngular * Time.deltaTime;
    }
    public void Stop()
    {
        _currentSpeed = 0f;
        _currentAngular = 0f;
    }
    private float Approach(float current, float target, float maxValue)
    {
        bool speedingUp = Mathf.Abs(target) > Mathf.Abs(current)
                          && (current == 0f || Mathf.Sign(target) == Mathf.Sign(current));

        float time = speedingUp ? accelTime : coastTime;
        float rate = time > 0f ? maxValue / time : float.PositiveInfinity;

        return Mathf.MoveTowards(current, target, rate * Time.deltaTime);
    }

    private static float ApplyDeadzone(float value, float trigger)
    {
        return Mathf.Abs(value) < trigger ? 0f : value;
    }
}
