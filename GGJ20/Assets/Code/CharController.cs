using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharController : MonoBehaviour
{

    public GameObject playerVisual;
    float forwardMoveSpeed = 0f;
    float horizontalMoveSpeed = 0f;
    float verticalMoveSpeed = 0f;
    float rightTurnSpeed = 0f;
    float leftTurnSpeed = 0f;
    float turnSpeed = 0f;
    int turnSign = 0;
    float resetTurnSpeed = 0f;
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
    string verticalAxis;
    // Start is called before the first frame update
    void Start()
    {
        if (Application.platform == RuntimePlatform.WindowsPlayer || Application.platform == RuntimePlatform.WindowsEditor)
            verticalAxis = "RS_Vertical_PC";
        else
            verticalAxis = "RS_Vertical";

        Debug.Log(verticalAxis);
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
        verticalMoveSpeed = setVerticalDirectionSpeed(verticalMoveSpeed, verticalAxis);
    }

    void calcMovement()
    {
        rightMovement = transform.right * horizontalMoveSpeed * Time.deltaTime;
        forwardMovement = transform.forward * forwardMoveSpeed * Time.deltaTime;
        verticalMovement = transform.up * verticalMoveSpeed * Time.deltaTime;
    }

    void applyRotation()
    {
        if (Input.GetAxis("RS_Horizontal") > 0)
        {
            turnSign = 1;

            rightTurnSpeed += 0.5f * Input.GetAxis("RS_Horizontal");

            if (rightTurnSpeed > 5)

                rightTurnSpeed = 5;

            turnSpeed = rightTurnSpeed;
            leftTurnSpeed = resetTurnSpeed;
        }

        else if (Input.GetAxis("RS_Horizontal") < 0)

        {
            turnSign = -1;

            leftTurnSpeed += -1 * 0.5f * Input.GetAxis("RS_Horizontal");

            if (leftTurnSpeed > 5)

                leftTurnSpeed = 5;

            turnSpeed = leftTurnSpeed;
            rightTurnSpeed = resetTurnSpeed;
        }

        else
        {           
            rightTurnSpeed -= 0.5f;
            leftTurnSpeed -= 0.5f;
            turnSpeed -= 0.5f;

            if (rightTurnSpeed < 0)

                rightTurnSpeed = 0;

            if (leftTurnSpeed < 0)

                leftTurnSpeed = 0;

            if (turnSpeed < 0)

                turnSpeed = 0;

           // Debug.Log(turnSpeed);
        }

        if (Input.GetAxis("RS_Horizontal") != 0)
            rotateDir = transform.forward + transform.right * Time.deltaTime * Input.GetAxis("RS_Horizontal") * turnSpeed;
        else
        {
            //Debug.Log(turnSign);
            if (turnSign > 0)
                rotateDir = transform.forward + transform.right * Time.deltaTime * Mathf.Abs(turnSign) * turnSpeed;
            else
                rotateDir = transform.forward + transform.right * Time.deltaTime * -1 * Mathf.Abs(turnSign) * turnSpeed;

        }

        transform.forward = rotateDir;

        zRotation += -1 * Input.GetAxis("LS_Horizontal");
        xRotation += Input.GetAxis("LS_Vertical");

        if (xRotation > 10)
            xRotation = 10;

        if(xRotation < -10)
            xRotation = -10;

        if (zRotation > 10)
            zRotation = 10;

        if (zRotation < -10)
            zRotation = -10;

        if (Input.GetAxis("LS_Horizontal") == 0)

            zRotation += (-1.0f  * Mathf.Sign(zRotation));

        if (Input.GetAxis("LS_Vertical") == 0)

            xRotation += (-1.0f * Mathf.Sign(xRotation));

        if ((Mathf.Abs(zRotation) < 1.1) && (Input.GetAxis("LS_Horizontal") == 0))

            zRotation = 0;

        if ((Mathf.Abs(xRotation) < 1.1) && (Input.GetAxis("LS_Vertical") == 0))

            xRotation = 0;

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

        if (Mathf.Abs(directionSpeed) < 1)
            directionSpeed = 0;

        return directionSpeed;
    }

    float setVerticalDirectionSpeed (float directionSpeed, string thumbstick)
    {
        directionSpeed += 2 * Input.GetAxis(thumbstick);

        if (directionSpeed > 25)
            directionSpeed = 25;

        else if (directionSpeed < -25)
            directionSpeed = -25;

        if (Input.GetAxis(thumbstick) == 0)

            directionSpeed += (2 * slow * -1 * Mathf.Sign(directionSpeed));

        if (Mathf.Abs(directionSpeed) < 2)
            directionSpeed = 0;

        return directionSpeed;
    }
}
