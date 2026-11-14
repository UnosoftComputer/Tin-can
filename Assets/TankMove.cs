using UnityEngine;

public class TankMove : MonoBehaviour
{
    [Header("Tracks")]
    [SerializeField] private Track _left;
    [SerializeField] private Track _right;
    [Header("Movent settings")]
    [SerializeField] private float _motorTorque = 1500f;
    [SerializeField] private float _brakeTorque = 3000f;

    public void Left(float direction)
    {
        if (direction == 0)
        {
            _left.Stop(_brakeTorque);
        }
        else
        {
            _left.Stop(0);
            _left.Move(direction * _motorTorque);
        }
    }  
    public void Right(float direction)
    {
        if (direction == 0)
        {
            _right.Stop(_brakeTorque);
        }
        else
        {
            _right.Stop(0);
            _right.Move(direction * _motorTorque);
        }
    }
}
