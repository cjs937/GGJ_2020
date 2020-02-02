using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharController : MonoBehaviour
{

    public GameObject playerVisual;
    float forwardMoveSpeed = 0f;
    float horizontalMoveSpeed = 0f;
    float verticalMoveSpeed = 0f;
    const int maxSpeed = 12;
    const int minSpeed = -12;
    const int maxTilt = 5;
    const int minTilt = 0;
    float xRotation = 0f;
    float zRotation = 0f;
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
        forwardMoveSpeed = setDirectionSpeed(forwardMoveSpeed, "LS_Vertical");
        horizontalMoveSpeed = setDirectionSpeed(horizontalMoveSpeed, "LS_Horizontal");
        verticalMoveSpeed = setDirectionSpeed(verticalMoveSpeed, "RS_Vertical");
    }

    void calcMovement()
    {
        rightMovement = transform.right * horizontalMoveSpeed * Time.deltaTime;
        forwardMovement = transform.forward * forwardMoveSpeed * Time.deltaTime;
        verticalMovement = transform.up * verticalMoveSpeed * Time.deltaTime;
    }

    void applyRotation()
    {
        rotateDir = transform.forward + transform.right * Time.deltaTime * Input.GetAxis("RS_Horizontal") * 5;
        transform.forward = rotateDir;

        zRotation += -1 * (10f / 60f) * Input.GetAxis("LS_Horizontal");
        xRotation += (10f / 60f) * Input.GetAxis("LS_Vertical");

        if (xRotation > 10)
            xRotation = 10;

        if(xRotation < -10)
            xRotation = -10;

        if (zRotation > 10)
            zRotation = 10;

        if (zRotation < -10)
            zRotation = -10;

        if (Input.GetAxis("LS_Horizontal") == 0)

            zRotation += ((10/60) * -1 * Mathf.Sign(zRotation));

        if (Input.GetAxis("LS_Vertical") == 0)

            xRotation += ((10/60) * -1 * Mathf.Sign(xRotation));

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