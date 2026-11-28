using UnityEngine;

public class TankMoveAlt
{
    #region PRIVATE_VARIBLES
    private float _forwardSpeed;
    private float _backwardSpeed;
    private float _rotateSpeed;

    private float _accelTime;
    private float _coastTime;
    private float _brakeTime;
    private float _rotateAccelTime;
    private float _rotateCoastTime;
    private float _throttle;
    private float _steering;
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
        private set { _accelTime = value >= 0 ? value : 0; }
    }
    public float coastTime
    {
        get { return _coastTime; }
        private set { _coastTime = value >= 0 ? value : 0; }
    }
    public float brakeTime
    {
        get { return _brakeTime; }
        private set { _brakeTime = value >= 0 ? value : 0; }
    }

    public float rotateAccelTime
    {
        get { return _rotateAccelTime; }
        private set { _rotateAccelTime = value >= 0 ? value : 0; }
    }
    public float rotateCoastTime
    {
        get { return _rotateCoastTime; }
        private set { _rotateCoastTime = value >= 0 ? value : 0; }
    }

    public float throttle { get { return _throttle; } }
    public float steering { get { return _steering; } }
    #endregion

    public TankMoveAlt(float _forwardSpeed, float _backwardSpeed, float _rotateSpeed,
                       float _accelTime = 0.2f, float _coastTime = 0.6f, float _brakeTime = 0.2f,
                       float _rotateAccelTime = 0.1f, float _rotateCoastTime = 0.15f)
    {
        forwardSpeed = _forwardSpeed;
        backwardSpeed = _backwardSpeed;
        rotateSpeed = _rotateSpeed;
        accelTime = _accelTime;
        coastTime = _coastTime;
        brakeTime = _brakeTime;
        rotateAccelTime = _rotateAccelTime;
        rotateCoastTime = _rotateCoastTime;
    }

    public Vector2 Move(float lever, float trigger)
    {
        float target = ApplyDeadzone(lever, trigger);
        _throttle = Approach(_throttle, target, accelTime, coastTime, brakeTime);

        float speed = _throttle >= 0f ? forwardSpeed : backwardSpeed;
        return Vector2.up * (_throttle * speed * Time.deltaTime);
    }

    public float Rotate(float wheel, float trigger)
    {
        float target = ApplyDeadzone(wheel, trigger);
        _steering = Approach(_steering, target, rotateAccelTime, rotateCoastTime, rotateAccelTime);

        return _steering * rotateSpeed * Time.deltaTime;
    }

    public void Stop()
    {
        _throttle = 0f;
        _steering = 0f;
    }

    private static float Approach(float current, float target,
                                  float accel, float coast, float brake)
    {
        bool reversing = current != 0f && target != 0f
                         && Mathf.Sign(current) != Mathf.Sign(target);
        bool releasing = !reversing && Mathf.Abs(target) < Mathf.Abs(current);

        float time = reversing ? brake : releasing ? coast : accel;
        float rate = 1f / Mathf.Max(time, 0.01f);

        return Mathf.MoveTowards(current, target, rate * Time.deltaTime);
    }

    private static float ApplyDeadzone(float value, float trigger)
    {
        return Mathf.Abs(value) < trigger ? 0f : value;
    }
}