using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempPlayerMovement : MonoBehaviour
{
    float movespeed = 4f;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W))
            transform.Translate(Vector3.forward * movespeed * Time.deltaTime);

        if (Input.GetKey(KeyCode.A))
            transform.Translate(Vector3.right * -1 * movespeed * Time.deltaTime);

        if (Input.GetKey(KeyCode.S))
            transform.Translate(Vector3.forward * -1 * movespeed * Time.deltaTime);

        if (Input.GetKey(KeyCode.D))
            transform.Translate(Vector3.right * movespeed * Time.deltaTime);
    }
}
