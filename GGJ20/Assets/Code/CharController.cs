    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharController : MonoBehaviour
{

	public GameObject playerVisual;
	float verticalMoveSpeed = 0f;
	float horizontalMoveSpeed = 0f;
	const int maxSpeed = 25;
	const int minSpeed = -25;
    const int stop = 0;

	Vector3 rotateDir, rightMovement, upMovement;

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
		verticalMoveSpeed += Input.GetAxis("LS_Vertical");

		if (verticalMoveSpeed > maxSpeed)
			verticalMoveSpeed = maxSpeed;

		else if (verticalMoveSpeed < minSpeed)
			verticalMoveSpeed = minSpeed;

        if (Input.GetAxis("RS_Horizontal") != 0)
        {
            horizontalMoveSpeed += Input.GetAxis("RS_Horizontal");

            if (horizontalMoveSpeed > maxSpeed)
                horizontalMoveSpeed = maxSpeed;

            else if (horizontalMoveSpeed < minSpeed)
                horizontalMoveSpeed = minSpeed;
        }

        else
        {
            if (horizontalMoveSpeed > 0)

                horizontalMoveSpeed -= 1;

            else

                horizontalMoveSpeed += 1;

            if (Mathf.Abs(horizontalMoveSpeed) <= 1)

                horizontalMoveSpeed = 0;
        }
    }
    
    void calcMovement()
	{
		rightMovement = transform.right * horizontalMoveSpeed * Time.deltaTime;

        if (verticalMoveSpeed > 0)

            calcPositiveVerticalMoveSpeed();

        else if (verticalMoveSpeed < 0)

            calcNegativeVerticalMoveSpeed();
	}
    
    void applyRotation ()
	{
		rotateDir = transform.forward + transform.right * Time.deltaTime * Input.GetAxis("LS_Horizontal") * 5;
		transform.forward = rotateDir;

	    float zRotation = -22.5f * Input.GetAxis("RS_Horizontal");
        float xRotation = 22.5f * Input.GetAxis("RS_Vertical");
        
        playerVisual.transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles + new Vector3(xRotation, 0.0f, zRotation));
    }

    void applyMovement ()
	{
		transform.position += rightMovement;
		transform.position += upMovement;
	}

    void calcPositiveVerticalMoveSpeed()
    {
        if (Input.GetAxis("RS_Vertical") >= 0)

            upMovement = transform.forward * (verticalMoveSpeed + (verticalMoveSpeed * Input.GetAxis("RS_Vertical"))) * Time.deltaTime;

        else if (Input.GetAxis("RS_Vertical") < 0)

            upMovement = transform.forward * (verticalMoveSpeed + ((verticalMoveSpeed * Input.GetAxis("RS_Vertical"))) / 2) * Time.deltaTime;

        else

            upMovement = transform.forward * verticalMoveSpeed * Time.deltaTime;
    }

    void calcNegativeVerticalMoveSpeed()
    {
        if (Input.GetAxis("RS_Vertical") >= 0)

            upMovement = transform.forward * (verticalMoveSpeed - ((verticalMoveSpeed * Input.GetAxis("RS_Vertical"))) / 2) * Time.deltaTime;


        else if (Input.GetAxis("RS_Vertical") < 0)

            upMovement = transform.forward * (verticalMoveSpeed - (verticalMoveSpeed * Input.GetAxis("RS_Vertical"))) * Time.deltaTime;

        else

            upMovement = transform.forward * verticalMoveSpeed * Time.deltaTime;
    }
}
