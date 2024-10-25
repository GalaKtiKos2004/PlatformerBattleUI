using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatroler : MonoBehaviour, IMovable
{
    [SerializeField] private Transform _player;
    [SerializeField] private List<Transform> _waypoints;
    [SerializeField] private PlayerFinder _finder;
    [SerializeField] private float _speed = 3f;
    [SerializeField] private float _delay = 5f;

    private Transform _target;

    private WaitForSeconds _wait;

    private float _currentSpeed;

    private int _currentWaypoint = 0;

    public event Action<float> Moved;

    private void OnEnable()
    {
        _finder.Collide += ChangeTarget;
    }

    private void Awake()
    {
        _wait = new WaitForSeconds(_delay);
        _currentSpeed = _speed;
        _target = _waypoints[_currentWaypoint];
    }

    private void Update()
    {
        Move();

        if (transform.position.x == _waypoints[_currentWaypoint].position.x)
        {
            if (_currentWaypoint == _waypoints.Count - 1)
            {
                StartCoroutine(CountPatrolDelay());
            }
            else
            {
                _currentWaypoint++;
                _target = _waypoints[_currentWaypoint];
            }
        }
    }

    private void OnDisable()
    {
        _finder.Collide -= ChangeTarget;
    }

    private void Move()
    {
        FlipTowardsTarget();

        Moved?.Invoke(_currentSpeed);
        transform.position = new Vector3(
            Mathf.MoveTowards(transform.position.x, _target.position.x, _currentSpeed * Time.deltaTime),
            transform.position.y, transform.position.z);
    }

    private void FlipTowardsTarget()
    {
        Quaternion rotationLeftAngle = Quaternion.Euler(0f, 0f, 0f);
        Quaternion rotationRightAngle = Quaternion.Euler(0f, 180f, 0f);

        if (_target == null)
            return;

        if (_target.position.x > transform.position.x)
        {
            transform.rotation = rotationRightAngle;
        }
        else if (_target.position.x < transform.position.x)
        {
            transform.rotation = rotationLeftAngle;
        }
    }

    private void ChangeTarget()
    {
        if (_target.TryGetComponent(out Waypoint _))
        {
            _waypoints.Reverse();
            _currentWaypoint = 0;

            _currentSpeed = _speed;

            StopAllCoroutines();

            _target = _player.transform;
        }
        else
        {
            _target = _waypoints[_currentWaypoint];
        }
    }

    private IEnumerator CountPatrolDelay()
    {
        Vector3 rotation = new Vector3(0f, 180f, 0f);

        _currentSpeed = 0;
        _currentWaypoint = 0;

        yield return _wait;

        _waypoints.Reverse();
        _currentSpeed = _speed;
    }
}