using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] private WaypointPath _waypointPath;
    [SerializeField]private float _speed;
    private int _targetWaypointIndex;
    private Transform _previousWaypoint;
    private Transform _targetWaypoint;
    public int platformStep;//Thsi is just how many waypoints the platform will move at once if not on auto

    private float _timeToWaypoint;
    private float _elapsedTime;

    private bool isMoving = false;
    public bool isAuto = false;

    void Start()
    {
       TargetNextWaypoint();
    }

    void FixedUpdate()
    {
         if (!isAuto && _elapsedTime >= _timeToWaypoint) 
         {
             isMoving = false;
             return;
         }
        _elapsedTime += Time.deltaTime;
        isMoving = true;
        float elapsedPercentage = _elapsedTime / _timeToWaypoint;
        elapsedPercentage = Mathf.SmoothStep(0, 1, elapsedPercentage);
        transform.position = Vector3.Lerp(_previousWaypoint.position, _targetWaypoint.position, elapsedPercentage);
        transform.rotation = Quaternion.Lerp(_previousWaypoint.rotation, _targetWaypoint.rotation, elapsedPercentage);

        if (elapsedPercentage >= 1 && isAuto)
        {
            TargetNextWaypoint();
        }
        else if (elapsedPercentage >= 1)
        {
            isMoving = false;
        }
    }

    public void TargetNextWaypoint()
    {
        _previousWaypoint = _waypointPath.GetWaypoint(_targetWaypointIndex);
        _targetWaypointIndex = _waypointPath.GetNextWaypointIndex(_targetWaypointIndex);
        _targetWaypoint = _waypointPath.GetWaypoint(_targetWaypointIndex);

        _elapsedTime = 0;

        float distanceToWaypoint = Vector3.Distance(_previousWaypoint.position, _targetWaypoint.position);
        _timeToWaypoint = distanceToWaypoint / _speed;
    }

    public void MoveForward(int numberOfWaypoints)
    {
        if (isMoving) return;
        StartCoroutine(MoveStepsForward(numberOfWaypoints));
    }

    private IEnumerator MoveStepsForward(int steps)
    {
        for (int i = 0; i < steps; i++)
        {
            TargetNextWaypoint();
        
             yield return new WaitUntil(() => _elapsedTime >= _timeToWaypoint);
         }
    }

    public void MoveBackward(int numberOfWaypoints)
    {
        if (isMoving) return;
        StartCoroutine(MoveStepsBackward(numberOfWaypoints));
    }

    private IEnumerator MoveStepsBackward(int steps)
    {
        for (int i = 0; i < steps; i++)
        {
            TargetPreviousWaypoint();
        
         yield return new WaitUntil(() => _elapsedTime >= _timeToWaypoint);
        }
    }

    public void TargetPreviousWaypoint()
    {
        _previousWaypoint = _waypointPath.GetWaypoint(_targetWaypointIndex);
        _targetWaypointIndex = _waypointPath.GetPreviousWaypointIndex(_targetWaypointIndex);
        _targetWaypoint = _waypointPath.GetWaypoint(_targetWaypointIndex);
        _elapsedTime = 0;
        float distanceToWaypoint = Vector3.Distance(_previousWaypoint.position, _targetWaypoint.position);
        _timeToWaypoint = distanceToWaypoint / _speed;
    }
}