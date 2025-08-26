using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class BanditMovement : MonoBehaviour
{
    [SerializeField] private Transform _wayPoints;
    [SerializeField] private float _stoppingDistance;
    [SerializeField] private float _stopDelay;
    [SerializeField] private float _speed;
    [SerializeField] private List<Transform> _targets;

    private Coroutine _move;
        
    private void OnEnable()
    {
        if (TryGetPlaces())
        {
            if (_move == null)
            {
                _move = StartCoroutine(Move());
            }
        }
    }

    private void OnDisable()
    {
        if( _move != null)
        {
            StopCoroutine(_move);
            _move = null;
        }
    }

    [ContextMenu("Refresh Waypoints")]
    private bool TryGetPlaces()
    {
        int childCount = _wayPoints.childCount;
        if (_wayPoints == null || childCount == 0)
            return false;

        for (int i = 0; i < childCount; i++)
            _targets.Add(_wayPoints.GetChild(i));

        return _targets.Count > 0;
    }

    private bool IsCloseEnough(Vector3 currentWayPoint, Vector3 currentPosition)
    {
        return (currentWayPoint - currentPosition).sqrMagnitude <= _stoppingDistance * _stoppingDistance;
    }

    private IEnumerator Move()
    {
        WaitForSeconds waitForSeconds = new WaitForSeconds(_stopDelay);

        int currentWaypointIndex = 0;

        while (isActiveAndEnabled)
        {

            while (!IsCloseEnough(transform.position, _targets[currentWaypointIndex].position))
            {
                transform.position = Vector3.MoveTowards(transform.position, _targets[currentWaypointIndex].position, _speed * Time.deltaTime);

                yield return null;
            }

            currentWaypointIndex = ++currentWaypointIndex % _targets.Count;

            yield return waitForSeconds;
        }
    }
}
