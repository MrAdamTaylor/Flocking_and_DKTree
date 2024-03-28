using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateOctree : MonoBehaviour
{
    public GameObject[] worldObjects;
    public int nodeMinSize = 5;
    private Octree _octree;
    void Start()
    {
        _octree = new Octree(worldObjects, nodeMinSize);
    }

    //TODO - тут идут ошибки, потому что проверка не привязана к  прорисовке
    private void OnDrawGizmos()
    {
        if (Application.isPlaying)
        {
            _octree.RootNode.Draw();
        }
    }
    
}
