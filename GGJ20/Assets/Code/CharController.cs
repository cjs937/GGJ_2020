using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharController : MonoBehaviour
{
	float moveSpeed = 0f;
	const int maxSpeed = 25;
	const int minSpeed = 0;

	Vector3 forward, right;

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
		moveSpeed += Input.GetAxis("LS_Vertical");

		if (moveSpeed > maxSpeed)
			moveSpeed = maxSpeed;

		else if (moveSpeed < minSpeed)
			moveSpeed = minSpeed;

		Vector3 rightMovement = transform.right * moveSpeed * Time.deltaTime * Input.GetAxis("RS_Horizontal");

		Vector3 upMovement;

		if (Input.GetAxis("RS_Vertical") >= 0)

			upMovement = transform.forward * (moveSpeed + (moveSpeed * Input.GetAxis("RS_Vertical"))) * Time.deltaTime;

		//	Vector3 upMovement = transform.forward * (moveSpeed / 2) * Time.deltaTime;

		else if (Input.GetAxis("RS_Vertical") < 0)

			upMovement = transform.forward * (moveSpeed + ((moveSpeed * Input.GetAxis("RS_Vertical")))/2) * Time.deltaTime;

		else

			upMovement = transform.forward * moveSpeed * Time.deltaTime;

		//Vector3 upMovement = transform.forward * moveSpeed * Time.deltaTime * Input.GetAxis("RS_Vertical");
		////Vector3 heading = Vector3.Normalize(rightMovement + upMovement);

		////transform.forward = heading;
		//rb.MovePosition(transform.position + rightMovement + upMovement);

		//rb.velocity += (rightMovement + upMovement); //* Time.deltaTime;
		//rb.velocity += upMovement;

		transform.position += rightMovement;
		transform.position += upMovement;

		Vector3 rotateDir = transform.forward + transform.right * Time.deltaTime * Input.GetAxis("LS_Horizontal") * 5;

		transform.forward = rotateDir;

        
	}
}
