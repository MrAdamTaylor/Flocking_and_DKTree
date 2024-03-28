using FlockSimulation;
using UnityEditor.ShaderKeywordFilter;
using UnityEngine;

namespace FlockSimulation
{
    public class Flock : MonoBehaviour
    {
        private float _speed;
        void Start()
        {
            _speed = Random.Range(FlockManager.FM.MinSpeed, FlockManager.FM.MaxSpeed);
        }

        // Update is called once per frame
        void Update()
        {
            ApplyRules();
            this.transform.Translate(0, 0, _speed * Time.deltaTime);
        }

        private void ApplyRules()
        {
            GameObject[] gos;
            gos = FlockManager.FM.allFish;

            Vector3 vcentre = Vector3.zero;
            Vector3 vavoid = Vector3.zero;

            float gSpeed = 0.01f;
            float nDistance;
            int groupSize = 0;

            foreach (GameObject go in gos)
            {
                if (go != this.gameObject)
                {
                    nDistance = Vector3.Distance(go.transform.position, this.transform.position);
                    if (nDistance <= FlockManager.FM.NeighbourDistance)
                    {
                        vcentre += go.transform.position;
                        groupSize++;

                        if (nDistance < 1.0f)
                        {
                            vavoid = vavoid + (this.transform.position - go.transform.position);
                        }

                        Flock anotherFlock = go.GetComponent<Flock>();
                        gSpeed = gSpeed + anotherFlock._speed;
                    }
                }
            }

            if (groupSize > 0)
            {
                vcentre = vcentre / groupSize;
                _speed = gSpeed / groupSize;

                Vector3 direction = (vcentre + vavoid) - transform.position;
                if (direction != Vector3.zero)
                {
                    transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction),
                        FlockManager.FM.RotationSpeed * Time.deltaTime);
                }
            }
        }
    }
}
