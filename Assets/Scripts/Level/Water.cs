using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour
{
    private const float CellSize = 10;

    public float GetArea()
    {
        return transform.localScale.x * transform.localScale.z * CellSize;
    }
}
