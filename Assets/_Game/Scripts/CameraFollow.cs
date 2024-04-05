using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float speed = 10f;
    public Vector3 offset;
    void Start()
    {
        target = FindObjectOfType<HeroController>().transform;
    }

    
    void FixedUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, target.position + offset, Time.fixedDeltaTime * speed);
    }
}
