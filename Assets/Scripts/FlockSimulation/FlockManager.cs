using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace FlockSimulation
{
    public class FlockManager : MonoBehaviour
    {
        public static FlockManager FM;
        public GameObject fishPrefab;
        public int numFish = 20;
        public GameObject[] allFish;

        public Vector3 swimLimits = new Vector3(5, 5, 5);

        [Header("Fish Settings")] 

        [Range(0.0f, 5.0f)] 
        public float MinSpeed;

        [Range(0.0f, 5.0f)] 
        public float MaxSpeed;

        [Range(1.0f, 10.0f)] 
        public float NeighbourDistance;

        [Range(1.0f, 5.0f)] 
        public float RotationSpeed;

        
        
        public void Awake()
        {
            FM = this;
        }

        private void Start()
        {
            allFish = new GameObject[numFish];
            for (int i = 0; i < numFish; i++)
            {
                Vector3 pos = this.transform.position + new Vector3(Random.Range(-swimLimits.x, swimLimits.x),
                    Random.Range(-swimLimits.y, swimLimits.y),
                    Random.Range(-swimLimits.z, swimLimits.z));
                allFish[i] = Instantiate(fishPrefab, pos, Quaternion.identity);
                //allFish[i].AddComponent<Flock>();
            }

            
        }

    }
}
