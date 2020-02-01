﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlottedObject : MonoBehaviour
{
    public float lerpSpeed = 2.0f;
    public float maxDistance = 5f;
    Rigidbody rigidbody;
    PlayerInteractionScript player;
    bool followPlayer = true;


    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        player = FindObjectOfType<PlayerInteractionScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if(followPlayer)
        {
            Vector3 followPos = player.followPos.position;
            followPos.y = transform.position.y;

            rigidbody.position = Vector3.Lerp(transform.position, followPos, Time.deltaTime * lerpSpeed);

            if (Vector3.Distance(transform.position, followPos) > maxDistance)
                followPlayer = false;
        }
    }
}
