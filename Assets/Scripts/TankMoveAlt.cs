using UnityEngine;

public class TankMoveAlt 
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
    public TankMoveAlt(float _forwardSpeed, float _backwardSpeed, float _rotateSpeed)
    {
        forwardSpeed = _forwardSpeed;
        backwardSpeed = _backwardSpeed;
        rotateSpeed = _rotateSpeed;
    }
    public Vector2 Move(float lever, float trigger)
    {
        float direct = ApplyDeadzone(lever, trigger);
        float speed = direct >= 0f ? forwardSpeed : backwardSpeed;
        return Vector2.up * (direct * speed * Time.deltaTime);
    }


    public float Rotate(float wheel, float trigger)
    {
        float direct = ApplyDeadzone(wheel, trigger);
        return direct * rotateSpeed * Time.deltaTime;
    }

    private static float ApplyDeadzone(float value, float trigger)
    {
        return Mathf.Abs(value) < trigger ? 0f : value;
    }
}
