using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    public bool shouldRotate;
    public float rotateSpeed;

    // Update is called once per frame
    void Update()
    {
        if (shouldRotate)
            transform.Rotate(new Vector3(0, rotateSpeed, 0));
    }
}
