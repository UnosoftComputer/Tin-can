using UnityEngine;

public class TankGun
{
    private const float _freeFallAcceleration = 9.81f;
    private const float _speed = 2f;
    private float _beta;
    private float _alpha;
    public float beta
    {
        get
        {
            return _beta;
        }
        set 
        { 
            _beta = (value >= -90) && (90 > value) ? value : 90; 
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
            _alpha = (value >= 0) && (90 > value) ? value : 45;
        }
    }

    private float GetLenght()
    {
        return (Mathf.Pow(_speed,2)*Mathf.Sin(Mathf.Deg2Rad*2*alpha)) / _freeFallAcceleration;
    }
    public Vector2 GetCoordinates()
    {
        return new Vector2(GetLenght()* Mathf.Cos(Mathf.Deg2Rad * beta), GetLenght() * Mathf.Sin(Mathf.Deg2Rad * beta));
    }



}
