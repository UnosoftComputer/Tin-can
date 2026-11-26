using UnityEngine;

public class TankGun
{
    #region PRIVATE_VARIBLES
    private float _freeFallAcceleration = 9.81f;
    private float _speed = 5f;
    private float _beta;
    private float _alpha;
    #endregion
    #region PUBLIC_VARIBLES
    public float beta
    {
        get
        {
            return _beta;
        }
        set 
        { 
            _beta = (value > -360) && (360 > value) ? value : 0; 
        }
    }

    public float alpha
    {
        get
        {
            return _alpha;
        }
        set
        {
            _alpha = (value > 1) && (90 > value) ? value : 45;
        }
    }
    #endregion

    public TankGun(float speed = 5f, float freeFallAcceleration = 9.81f,float beta = -90, float alpha = 45)
    {
        _speed = speed;
        _freeFallAcceleration = freeFallAcceleration;
        this.beta = beta;
        this.alpha = alpha;
    }
    private float GetLength()
    {
        return (Mathf.Pow(_speed,2)*Mathf.Sin(Mathf.Deg2Rad*2*alpha)) / _freeFallAcceleration;
    }
    public Vector2 GetCoordinates()
    {
        return new Vector2(GetLength()* Mathf.Cos(Mathf.Deg2Rad * beta), -GetLength() * Mathf.Sin(Mathf.Deg2Rad * beta));
    }



}
