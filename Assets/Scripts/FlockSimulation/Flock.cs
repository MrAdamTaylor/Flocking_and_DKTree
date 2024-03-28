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
        }
    }
}
