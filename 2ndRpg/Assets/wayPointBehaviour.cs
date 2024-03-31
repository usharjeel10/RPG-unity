using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.control
{
    public class wayPointBehaviour : MonoBehaviour
    {
        private float waypointRadius = 0.3f;
        private int nextWayPoint;
        private void OnDrawGizmos()
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                nextWayPoint = getNextIndex(i);
                Gizmos.DrawSphere(getWaypoint(i), waypointRadius);
                Gizmos.DrawLine(getWaypoint(i),getWaypoint(nextWayPoint));
            }
        }
        public int getNextIndex(int i)
        {
            if (i + 1 == transform.childCount)
            {
                return 0;
            }
            return i + 1;
        }
        public Vector3 getWaypoint(int i)
        {
            return transform.GetChild(i).position;
        }
    }
}