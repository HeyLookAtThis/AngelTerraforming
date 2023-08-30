using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour
{
    public float GetArea()
    {
        return GetComponent<Terrain>().terrainData.size.x * GetComponent<Terrain>().terrainData.size.z;
    }
}
