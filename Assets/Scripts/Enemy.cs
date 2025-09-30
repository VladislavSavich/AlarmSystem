using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Enemy : MonoBehaviour
{
    [SerializeField] private Vector3 _targetPosition;

    private float _speed = 1.5f;
    private Vector3 _direction;

    private void Update()
    {
        _direction = (_targetPosition - transform.position).normalized;
        transform.position += _direction * _speed * Time.deltaTime;
        transform.rotation = Quaternion.LookRotation(_direction);
    }
}
