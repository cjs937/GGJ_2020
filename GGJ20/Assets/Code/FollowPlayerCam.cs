using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayerCam : MonoBehaviour
{
    public float smoothingSpeed;
    public Vector3 followOffset;

    Transform cameraMask;
    PlayerInteractionScript player;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerInteractionScript>();

        //Creates a new object for the camera mask on its own
        cameraMask = new GameObject("CameraMask").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if(player)
        {
            //Camera mask is set to the actual desired camera position & rotation
            cameraMask.position = player.followPos.position;
            cameraMask.position += followOffset;
            cameraMask.LookAt(player.followPos);

            //Camera interpolates to smoothly match the camera mask
            transform.position = Vector3.Lerp(transform.position, cameraMask.position, smoothingSpeed * Time.deltaTime);
            transform.rotation = Quaternion.Lerp(transform.rotation, cameraMask.rotation, smoothingSpeed * Time.deltaTime);
        }
    }
}
