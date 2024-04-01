using System;
using System.Collections.Generic;
using FlockSimulation;
using UnityEditor.ShaderKeywordFilter;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

namespace FlockSimulation
{
    public class Flock : MonoBehaviour
    {
        public bool Debug;
        
        private float _speed;

        public List<Vector3> CenterVectors = new List<Vector3>(); 
        public List<Vector3> AvoidVectors = new List<Vector3>();
        private Vector3 _center;
        private Vector3 _avoidVector;
        private FlockDrawer _drawer;

        void Start()
        {
            if (Debug)
            {
                _drawer = gameObject.AddComponent<FlockDrawer>();
            }

            _speed = Random.Range(FlockManager.FM.MinSpeed, FlockManager.FM.MaxSpeed);
        }
        
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

            //Vector3 vcentre = new Vector3(3,2,1);
            //Vector3 vavoid = new Vector3(3,2,1);
            
            float gSpeed = 0.01f;
            float nDistance;
            int groupSize = 0;

            
            
            foreach (GameObject go in gos)
            {
                if (go != this.gameObject)
                {
                    if (Debug)
                    {
                        _drawer.CurrentPosition = this.transform.position;
                    }

                    nDistance = Vector3.Distance(go.transform.position, this.transform.position);
                    if (Debug)
                    {
                        _drawer.NeighboursVectors.Add(go.transform.position);
                    }

                    //DrawRey(go.transform.position, transform.position, Color.blue);
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
                if (Debug)
                {
                    _drawer.Center = vcentre;
                    _drawer.AvoidVector = vavoid;
                }

                //_center = vcentre;
                //_avoidVector = vavoid;
                _speed = gSpeed / groupSize;

                Vector3 direction = (vcentre + vavoid) - transform.position;
                if (Debug)
                {
                    _drawer.MiddlePosition = (vcentre + vavoid);
                    _drawer.DirectionWithoutAvoid = vcentre - transform.position;
                    _drawer.DirectionWithoutCenter = vavoid - transform.position;
                    _drawer.FinalDirection = direction;
                }

                //DrawRey((vcentre+vavoid), transform.position, Color.cyan);
                if (direction != Vector3.zero)
                {
                    transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction),
                        FlockManager.FM.RotationSpeed * Time.deltaTime);
                }
            }
        }
        
    }
}
