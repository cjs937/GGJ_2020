using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayerCam : MonoBehaviour
{
    public float smoothingSpeed;
    public Vector3 followOffset;
    PlayerInteractionScript player;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerInteractionScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if(player)
        {
            Vector3 lerpPos = player.followPos.position;
            //lerpPos.y = transform.position.y;
            lerpPos += followOffset;

            transform.position = Vector3.Lerp(transform.position, lerpPos, smoothingSpeed * Time.deltaTime);
            transform.LookAt(player.followPos);
        }
    }
}
