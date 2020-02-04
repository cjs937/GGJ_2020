using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ObjectType
{
    BATTERY,
    CAPACITOR
}

public class SlottedObject : MonoBehaviour
{
    public float lerpSpeed = 2.0f;
    public ObjectType objectType;
    
    [HideInInspector]
    public Rigidbody rigidbody;
    PlayerInteractionScript player;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        player = FindObjectOfType<PlayerInteractionScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if(player.heldObj == this)
        {
            //Lerp object towards player if it is currently being held by them
            Vector3 followPos = player.followPos.position;
            followPos.y = transform.position.y;

            //Rigidbody position is used so that collisions will still work on these objects
            rigidbody.position = Vector3.Lerp(transform.position, followPos, Time.deltaTime * lerpSpeed);
        }
    }
}
