using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Serialization;

public class FlockDrawer : MonoBehaviour
{
    public Vector3 Center { get; set; }
    public Vector3 AvoidVector { get; set; }

    public List<Vector3> NeighboursVectors = new List<Vector3>();

    public Vector3 CurrentPosition;

    [FormerlySerializedAs("Direction")] public Vector3 FinalDirection;

    [CanBeNull] private static FlockDrawer _instance;

    public static FlockDrawer Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new FlockDrawer();
            }
            return _instance;
        }
    }

    public Vector3 DirectionWithoutAvoid { get; set; }
    public Vector3 DirectionWithoutCenter { get; set; }
    public Vector3 MiddlePosition { get; set; }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DrawRey(Vector3 startPos, Vector3 finishPos, Color color)
    {
        Debug.DrawRay(startPos, finishPos, color, 3);
    }

public void OnDrawGizmos()
{
    if (Center != null && AvoidVector != null)
    {
        Gizmos.DrawWireSphere(Center, 0.5f);
        
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(AvoidVector, 0.3f);
        Gizmos.DrawRay(AvoidVector, FinalDirection);

        Gizmos.color = Color.green;
        Gizmos.DrawRay(AvoidVector, DirectionWithoutAvoid);
        
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(MiddlePosition, 0.5f);
        Gizmos.DrawRay(AvoidVector, DirectionWithoutCenter);
        
        
        Gizmos.color = Color.red;
        Gizmos.DrawRay(Center, FinalDirection);

        Gizmos.color = Color.green;
        Gizmos.DrawRay(Center, DirectionWithoutAvoid);
        
        Gizmos.color = Color.blue;
        Gizmos.DrawRay(Center, DirectionWithoutCenter);
        
        Gizmos.color = Color.cyan;
        Gizmos.DrawRay(CurrentPosition, FinalDirection);
        
        /*if (NeighboursVectors.Count != 0)
        {
            for (int i = 0; i < NeighboursVectors.Count; i++)
            {
               DrawRey(CurrentPosition, NeighboursVectors[i], Color.blue);
            }
        }*/
    }
}
}
