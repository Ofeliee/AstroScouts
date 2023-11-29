using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitRotation : MonoBehaviour
{
    public float rotSpeed;
    public GameObject pivotObj;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.RotateAround(pivotObj.transform.position, new Vector3(0,0,1), rotSpeed * Time.deltaTime);
    }
}
