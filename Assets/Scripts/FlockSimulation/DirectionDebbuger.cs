using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionDebbuger : MonoBehaviour
{
    public void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(this.transform.position, this.transform.forward);
        Gizmos.color = Color.blue;
        Gizmos.DrawRay(this.transform.position, this.transform.up);
        Gizmos.color = Color.green;
        Gizmos.DrawRay(this.transform.position, this.transform.right);
    }
}
