using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Octree
{
    public OctreeNode RootNode;

    public Octree(GameObject[] worldObjects, float minNodeSize)
    {
        if (worldObjects.Length == 0)
        {
            throw new Exception("Необходимо добавить хотя - бы один объект для работы KD-дерева");
        }
        else
        {
            Bounds bounds = new Bounds();

            for (int i = 0; i < worldObjects.Length; i++)
            {
                bounds.Encapsulate(worldObjects[i].GetComponent<Collider>().bounds);
            }

            float maxSize = Mathf.Max(new float[] { bounds.size.x, bounds.size.y, bounds.size.z });
            Vector3 sizeVector = new Vector3(maxSize, maxSize, maxSize) * 0.5f;
            bounds.SetMinMax(bounds.center - sizeVector, bounds.center + sizeVector);
            Debug.Log($"Min Porog: {bounds.center + sizeVector},  Max Porog: {bounds.center -sizeVector}");
            RootNode = new OctreeNode(bounds, minNodeSize);
            AddObjects(worldObjects);
        }


        
    }

    public void AddObjects(GameObject[] worldObjects)
    {
        for (int i = 0; i < worldObjects.Length; i++)
        {
            RootNode.AddObject(worldObjects[i]);
        }
    }
}
