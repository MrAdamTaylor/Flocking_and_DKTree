using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAroundPoint : MonoBehaviour
{
    [SerializeField] private bool _debug;
    [SerializeField] private  Vector3 _pointNavigation;

    private Vector3 _direction;
    [SerializeField] private float _speed = 5f;

    private RotateAroundDebugger _debugger;

    [Range(2,10)]
    [SerializeField] private float _positionSpred;

    [Range(2,10)]
    [SerializeField]private int _powerSpred;
    
    [Range(5,15)]
    [SerializeField] private float _indentation;

    [Range(1, 10)] 
    [SerializeField] private float _neighbourDistance;


    private Vector3 _avoidStartPoint;
    private void Start()
    {
        _avoidStartPoint = _pointNavigation.GetVectorWithYDifference(_indentation);
        if (_debug)
        {
            _debugger = gameObject.AddComponent<RotateAroundDebugger>();
            _debugger.CirclePoint = _pointNavigation;
            _debugger.StartAvoidPoint = _avoidStartPoint;
        }
    }

    private void LateUpdate()
    {
        if (_debug)
        {
            _debugger.DrawRay(this.transform.position, this.transform.forward);
        }

        _direction = _pointNavigation - this.transform.position;
        this.transform.LookAt(_pointNavigation);
        if (_direction.magnitude > 20)
        {
            Vector3 velocity = _direction.normalized * _speed * Time.deltaTime;
            this.transform.position = this.transform.position + velocity;
        }
        else
        {
            //Vector3 vavoid = _pointNavigation.GetVectorWithYDifference(_indentation);
            Vector3 vavoid = Vector3.zero;
            for (int i = 0; i < _powerSpred; i++)
            {
                Vector3 vec = _pointNavigation.GetSpredVector(_neighbourDistance);
                if (_debug)
                {
                    _debugger.Vectors.Add(vec);
                }

                vavoid = vavoid + (this.transform.position - vec);
            }

            if (_debug)
            {
                _debugger.LowPoint = vavoid;
            }

            Vector3 direction = (vavoid + _pointNavigation) - transform.position;
            if (_debug)
            {
                _debugger.FinalDirection = direction;
            }

            if (direction != Vector3.zero)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction),
                    _speed * Time.deltaTime);
            }
            this.transform.Translate(0, 0, _speed * Time.deltaTime);
        }
    }
}

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
