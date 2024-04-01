using System.Collections.Generic;
using UnityEngine;

public class RotateAroundDebugger : MonoBehaviour
{
    public Vector3 CirclePoint;

    public Vector3 LowPoint;

    public List<Vector3> Vectors = new List<Vector3>();
    public Vector3 StartAvoidPoint { get; set; }
    public Vector3 FinalDirection { get; set; }

    public void DrawRay(Vector3 startPosition, Vector3 finishPosition)
    {
        Debug.DrawRay(startPosition, finishPosition, Color.cyan, 50);
    }

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(CirclePoint, 1.5f);

        if (StartAvoidPoint != Vector3.zero)
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawSphere(StartAvoidPoint, 1.5f);
        }

        if (LowPoint != Vector3.zero)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(LowPoint, 1.5f);
        }

        if (FinalDirection != Vector3.zero)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(LowPoint, 2.5f);
        }

        if (Vectors.Count > 0)
        {
            Gizmos.color = Color.red;
            for (int i = 0; i < Vectors.Count; i++)
            {
                Gizmos.DrawWireSphere(Vectors[i], 1.0f);
                
            }
        }
    }
}