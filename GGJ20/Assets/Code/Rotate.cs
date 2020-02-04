using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    public bool shouldRotate;
    public float rotateSpeed;
    public Vector3 rotateAxis;

    private void Start()
    {

    }
    // Update is called once per frame
    void Update()
    {
        if (shouldRotate)
            transform.Rotate(Vector3.Scale(rotateAxis, new Vector3(rotateSpeed, rotateSpeed, rotateSpeed)));
    }
}
