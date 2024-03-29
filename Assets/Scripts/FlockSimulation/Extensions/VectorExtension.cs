using UnityEngine;

public static class VectorExtension
{
    public static Vector3 ExcludeY(this Vector3 vec)
    {
        Vector3 newVec = new Vector3(vec.x, 0, vec.z);
        return newVec;
    }
    
    public static Vector3 ExcludeZ(this Vector3 vec)
    {
        Vector3 newVec = new Vector3(vec.x, vec.y,0);
        return newVec;
    }
    
    public static Vector3 ExcludeX(this Vector3 vec)
    {
        Vector3 newVec = new Vector3(0, vec.y,vec.z);
        return newVec;
    }

    public static Vector3 GetVectorWithYDifference(this Vector3 vec, float difference)
    {
        Vector3 newVec = vec;
        float yPos = newVec.y;
        newVec.y = yPos - difference;
        Debug.Log($"Y -- Before: {vec}, After: {newVec}");
        return newVec;
    }
    
    public static Vector3 GetSpredVector(this Vector3 vec, float distance)
    {
        Vector3 newVec = vec;
        float xPos = vec.x;
        float leftBoundX = xPos - distance;
        float rightBoundX = xPos + distance;
        newVec.x = xPos + Random.Range(leftBoundX, rightBoundX);
        float yPos = vec.y;
        float leftBoundY = yPos - distance;
        float rightBoundY = yPos + distance;
        newVec.y = yPos + Random.Range(leftBoundY, rightBoundY);
        float zPos = vec.y;
        float leftBoundZ = zPos - distance;
        float rightBoundZ = zPos + distance;
        newVec.z = zPos + Random.Range(leftBoundZ, rightBoundZ);
        Debug.Log($"Before: {vec}, After: {newVec}");
        return newVec;
    }
}