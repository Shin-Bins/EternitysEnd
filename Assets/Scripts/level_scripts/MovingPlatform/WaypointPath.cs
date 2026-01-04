using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WaypointPath : MonoBehaviour
{
  public Transform GetWaypoint(int waypointIndex)
    {
        return transform.GetChild(waypointIndex);
    }

    public int GetNextWaypointIndex(int currentWaypointIndex)
    {
        int nextWaypointIndex = currentWaypointIndex + 1;

        if (nextWaypointIndex == transform.childCount)
        {
            nextWaypointIndex = 0;
        }

        return nextWaypointIndex;
    }

    public int GetPreviousWaypointIndex(int currentWaypointIndex)
    {
        int previousWaypointIndex = currentWaypointIndex - 1;
        if (previousWaypointIndex < 0)
        {
            previousWaypointIndex = transform.childCount - 1;
        }
        return previousWaypointIndex;
    }
}
