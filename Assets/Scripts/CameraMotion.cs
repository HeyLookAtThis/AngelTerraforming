using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMotion : MonoBehaviour
{
    [SerializeField] private Transform _target;

    private Vector3 _interval = new Vector3(0, 7, -5);
    private float _delay = 2.9f;

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, _target.position + _interval, _delay * Time.deltaTime);
    }
}
