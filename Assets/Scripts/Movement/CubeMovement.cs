using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeMovement : MonoBehaviour
{
    private float x;
    private float y;
    private float z;

    public float deltaX;

    void Awake()
    {
        x = transform.position.x;
        y = transform.position.y;
        z = transform.position.z;
    }

    void Update()
    {
        x += deltaX * Time.deltaTime;
        transform.position = new Vector3(x,y,z);
    }
}
