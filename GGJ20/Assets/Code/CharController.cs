    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharController : MonoBehaviour
{

	public GameObject playerVisual;
	float verticalMoveSpeed = 0f;
	float horizontalMoveSpeed = 0f;
	const int maxSpeed = 25;
	const int minSpeed = 0;

	Vector3 forward, right, rotateDir, rightMovement, upMovement;

    // Start is called before the first frame update
    void Start()
    {
		//forward = Camera.main.transform.forward;
		//forward.y = 0;
		//forward = Vector3.Normalize(forward);
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
		
		//moveSpeed += Input.GetAxis("LS_Vertical");

		//if (moveSpeed > maxSpeed)
		//	moveSpeed = maxSpeed;

		//else if (moveSpeed < minSpeed)
		//	moveSpeed = minSpeed;

		//Vector3 rightMovement = transform.right * moveSpeed * Time.deltaTime * Input.GetAxis("RS_Horizontal");

		//Vector3 upMovement;

		//if (Input.GetAxis("RS_Vertical") >= 0)

		//	upMovement = transform.forward * (moveSpeed + (moveSpeed * Input.GetAxis("RS_Vertical"))) * Time.deltaTime;

		////	Vector3 upMovement = transform.forward * (moveSpeed / 2) * Time.deltaTime;

		//else if (Input.GetAxis("RS_Vertical") < 0)

		//	upMovement = transform.forward * (moveSpeed + ((moveSpeed * Input.GetAxis("RS_Vertical")))/2) * Time.deltaTime;

		//else

		//	upMovement = transform.forward * moveSpeed * Time.deltaTime;

		//Vector3 upMovement = transform.forward * moveSpeed * Time.deltaTime * Input.GetAxis("RS_Vertical");
		////Vector3 heading = Vector3.Normalize(rightMovement + upMovement);

		////transform.forward = heading;
		//rb.MovePosition(transform.position + rightMovement + upMovement);

		//rb.velocity += (rightMovement + upMovement); //* Time.deltaTime;
		//rb.velocity += upMovement;

		//transform.position += rightMovement;
		//transform.position += upMovement;

		//Vector3 rotateDir = transform.forward + transform.right * Time.deltaTime * Input.GetAxis("LS_Horizontal") * 5;

		//transform.forward = rotateDir;

        
	}

    void setMoveSpeed()
	{
		verticalMoveSpeed += Input.GetAxis("LS_Vertical");

		if (verticalMoveSpeed > maxSpeed)
			verticalMoveSpeed = maxSpeed;

		else if (verticalMoveSpeed < minSpeed)
			verticalMoveSpeed = minSpeed;
	}
    
    void calcMovement()
	{
		rightMovement = transform.right * verticalMoveSpeed * Time.deltaTime * Input.GetAxis("RS_Horizontal");

		if (Input.GetAxis("RS_Vertical") >= 0)

			upMovement = transform.forward * (verticalMoveSpeed + (verticalMoveSpeed * Input.GetAxis("RS_Vertical"))) * Time.deltaTime;

		//	Vector3 upMovement = transform.forward * (moveSpeed / 2) * Time.deltaTime;

		else if (Input.GetAxis("RS_Vertical") < 0)

			upMovement = transform.forward * (verticalMoveSpeed + ((verticalMoveSpeed * Input.GetAxis("RS_Vertical"))) / 2) * Time.deltaTime;

		else

			upMovement = transform.forward * verticalMoveSpeed * Time.deltaTime;
	}
    
    void applyRotation ()
	{
		rotateDir = transform.forward + transform.right * Time.deltaTime * Input.GetAxis("LS_Horizontal") * 5;
		transform.forward = rotateDir;

	    float zRotation = -45f * Input.GetAxis("RS_Horizontal");
        //float xRotation = 45f * Input.GetAxis("RS_Vertical");

        //playerVisual.transform.rotation = Quaternion.Euler(0.0f, 0.0f, zRotation);
        //playerVisual.transform.rotation =
        playerVisual.transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles + new Vector3(0.0f, 0.0f, zRotation));
		//transform.Rotate(xRotation, 0.0f, 0.0f, Space.Self);
	}

    void applyMovement ()
	{
		transform.position += rightMovement;
		transform.position += upMovement;
	}
}
