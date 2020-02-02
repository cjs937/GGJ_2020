using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharController : MonoBehaviour
{

    public GameObject playerVisual;
    float forwardMoveSpeed = 0f;
    float horizontalMoveSpeed = 0f;
    float verticalMoveSpeed = 0f;
    const int maxSpeed = 25;
    const int minSpeed = -25;
    const int stop = 0;
    const float slow = 0.5f;
    public float raycastDistance = 1f;
    public LayerMask raycastLayers;
    Vector3 rotateDir, rightMovement, forwardMovement, verticalMovement;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    void Move()
    {
        setMoveSpeed();
        calcMovement();
        applyMovement();
        applyRotation();
    }

    void setMoveSpeed()
    {
        forwardMoveSpeed = setDirectionSpeed(forwardMoveSpeed, "RS_Vertical");
        horizontalMoveSpeed = setDirectionSpeed(horizontalMoveSpeed, "RS_Horizontal");
        verticalMoveSpeed = setDirectionSpeed(verticalMoveSpeed, "LS_Vertical");
    }

    void calcMovement()
    {
        rightMovement = transform.right * horizontalMoveSpeed * Time.deltaTime;
        forwardMovement = transform.forward * forwardMoveSpeed * Time.deltaTime;
        verticalMovement = transform.up * verticalMoveSpeed * Time.deltaTime;
    }

    void applyRotation()
    {
        rotateDir = transform.forward + transform.right * Time.deltaTime * Input.GetAxis("LS_Horizontal") * 5;
        transform.forward = rotateDir;

        float zRotation = -5f * Input.GetAxis("RS_Horizontal");
        float xRotation = 5f * Input.GetAxis("RS_Vertical");

        playerVisual.transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles + new Vector3(xRotation, 0.0f, zRotation));
    }

    void applyMovement()
    {
        if (!Physics.Raycast(transform.position, rightMovement.normalized, raycastDistance, raycastLayers))
            transform.position += rightMovement;

        if (!Physics.Raycast(transform.position, forwardMovement.normalized, raycastDistance, raycastLayers))
            transform.position += forwardMovement;

        if (!Physics.Raycast(transform.position, verticalMovement.normalized, raycastDistance, raycastLayers))
            transform.position += verticalMovement;
    }

    float setDirectionSpeed(float directionSpeed, string thumbstick)
    {
            directionSpeed += Input.GetAxis(thumbstick);
     
            if (directionSpeed > maxSpeed)
                directionSpeed = maxSpeed;

            else if (directionSpeed < minSpeed)
                directionSpeed = minSpeed;
      
        if (Input.GetAxis(thumbstick) == 0)

            directionSpeed += (slow * -1 * Mathf.Sign(directionSpeed));

        return directionSpeed;
    }
}