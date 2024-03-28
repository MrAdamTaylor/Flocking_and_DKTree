using UnityEngine;

public class OctreeNode 
{
    private Bounds _nodeBounds;
    private float _minSize;
    private Bounds[] _childBounds;
    private OctreeNode[] _children = null;


    public OctreeNode(Bounds b, float minNodeSize)
    {
        _nodeBounds = b;
        _minSize = minNodeSize;

        float quater = _nodeBounds.size.y / 4.0f;
        float childLength = _nodeBounds.size.y / 2f;
        Vector3 childSize = new Vector3(childLength, childLength, childLength);
        _childBounds = new Bounds[8];
        _childBounds[0] = new Bounds(_nodeBounds.center + new Vector3(-quater, quater, -quater), childSize);
        _childBounds[1] = new Bounds(_nodeBounds.center + new Vector3(quater, quater, -quater), childSize);
        _childBounds[2] = new Bounds(_nodeBounds.center + new Vector3(-quater, quater, quater), childSize);
        _childBounds[3] = new Bounds(_nodeBounds.center + new Vector3(quater, quater, quater), childSize);
        _childBounds[4] = new Bounds(_nodeBounds.center + new Vector3(-quater, quater, -quater), childSize);
        _childBounds[5] = new Bounds(_nodeBounds.center + new Vector3(-quater, -quater, -quater), childSize);
        _childBounds[6] = new Bounds(_nodeBounds.center + new Vector3(-quater, -quater, quater), childSize);
        _childBounds[7] = new Bounds(_nodeBounds.center + new Vector3(quater, -quater, quater), childSize);
    }

    public void Draw()
    {
        Gizmos.color = new Color(0, 1, 0);
        Gizmos.DrawWireCube(_nodeBounds.center, _nodeBounds.size);
        DrawChilds();
    }

    private void DrawChilds()
    {
        if (_children != null)
        {
            for (int i = 0; i < 8; i++)
            {
                if (_children[i] != null)
                {
                    _children[i].Draw();
                }
            }
        }
    }

    public void AddObject(GameObject worldObject)
    {
        DivideAndAdd(worldObject);
    }

    private void DivideAndAdd(GameObject worldObject)
    {
        if (_nodeBounds.size.y <= _minSize)
            return;
        
        _children ??= new OctreeNode[8];

        bool dividing = false;
        for (int i = 0; i < 8; i++)
        {
            if (_children[i] == null)
            {
                _children[i] = new OctreeNode(_childBounds[i], _minSize);
            }

            if (_childBounds[i].Intersects(worldObject.GetComponent<Collider>().bounds))
            {
                dividing = true;
                _children[i].DivideAndAdd(worldObject);
            }
        }

        if (dividing == false)
        {
            _children = null;
        }
    }
}
