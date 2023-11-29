using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SolarSystemRotation : MonoBehaviour
{
    public Transform Sun;
    public float speed;
    public Vector3 axis;

    void Start()
    {
        axis = new Vector3(0,Random.Range(0f, 1f), Random.Range(0f, 1f));
        speed = Random.Range(5f, 100f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.RotateAround(Sun.position, axis, speed * Time.deltaTime);
    }
}
