using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ocillirator : MonoBehaviour
{
    private float timeCounter = 0;

    private float speed;
    private float width;
    private float height;

    private void Start()
    {
        speed = 5;
        width = 4;
        height = 7;
    }

    private void Update()
    {
        timeCounter += Time.deltaTime;

        float x = Mathf.Cos(timeCounter);
        //float y = Mathf.Sin(timeCounter);
        //float y = 0;
        float y = Mathf.Cos(timeCounter);
        float z = 0;

        transform.position = new Vector3(x, y, z);
    }
}
