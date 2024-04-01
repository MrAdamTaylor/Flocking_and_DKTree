using UnityEngine;

namespace FlockSimulation
{
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
        
        [Header("Настройки скорости поворота")]
        [Range(2,10)] 
        [SerializeField] private float _rotateSpeed;
        
        [Range(2,10)] 
        [SerializeField] private float _radiusRotate;
        private float _timeCounter = 0;
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
                Vector3 velocity = _direction.normalized * (_speed * Time.deltaTime);
            
                var transform1 = transform;
                transform1.position = transform1.position + velocity;
            }
            else
            {
                //OldMethod();
                NewRotateMethod();
            }
            //NewRotateMethod();
        }

        private void NewRotateMethod()
        {
            _timeCounter += Time.deltaTime * _rotateSpeed;

            var position = transform.position;
            float x = position.x + Mathf.Cos(_timeCounter) * _radiusRotate;
            float y = position.y;
            float z = position.z + Mathf.Sin(_timeCounter) * _radiusRotate;

            position = new Vector3(x, y, z);
            transform.position = position;
        }

        private void OldMethod()
        {
            Vector3 avoidVec = Vector3.zero;
            for (int i = 0; i < _powerSpred; i++)
            {
                Vector3 vec = _pointNavigation.GetSpredVector(_neighbourDistance);
                if (_debug)
                {
                    _debugger.Vectors.Add(vec);
                }

                avoidVec = avoidVec + (this.transform.position - vec);
            }

            if (_debug)
            {
                _debugger.LowPoint = avoidVec;
            }

            Vector3 direction = (avoidVec + _pointNavigation) - transform.position;
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