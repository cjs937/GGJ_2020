using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractionScript : MonoBehaviour
{
    public Transform followPos;
    public float objectFindRadius;

    [HideInInspector]
    public SlottedObject heldObj;
    public float maxDistance = 5f;

    public Material goodMaterial;
    public Material badMaterial;

    LineRenderer lineRenderer;
    GameManager gameManager;

    private void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        //Hides the line until its needed
        lineRenderer.enabled = false;
        gameManager = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Interact"))
        {
            if (heldObj == null)
                PickUpObject();
            else
                DropObject(true);
        }

        UpdateTractorBeam();
    }

    public void PickUpObject()
    {
        //Finds all colliders in a certain radius
        Collider[] objectsInRange = Physics.OverlapSphere(transform.position, objectFindRadius);
        float distanceToClosest = float.MaxValue;
        SlottedObject closestObject = null;

        //Loops through to find the closest slotted object to the player
        foreach (Collider nearbyObj in objectsInRange)
        {
            //Casts to a slotted object to check if this collider is valid for distance checking
            SlottedObject slottedObj = nearbyObj.GetComponent<SlottedObject>();

            if (slottedObj == null || !slottedObj.enabled)
                continue;

            float distance = Vector3.Distance(followPos.transform.position, slottedObj.transform.position);
            
            //If this slotted obj is closer than the last one in the loop then the logic is updated accordingly
            if (distance < distanceToClosest)
            {
                closestObject = slottedObj;
                distanceToClosest = distance;
            }
        }

        if(closestObject)
        {
            heldObj = closestObject;
        }
    }

    public void DropObject(bool manualDrop)
    {
        if (!heldObj)
            return;

        bool didScore = false;

        if (manualDrop)
        {
            Collider[] objectsInRange = Physics.OverlapSphere(transform.position, objectFindRadius);

            foreach (Collider nearbyObj in objectsInRange)
            {
                ObjectSlot objectSlot = nearbyObj.GetComponent<ObjectSlot>();

                if (objectSlot == null)
                    continue;

                //If the held item slot type is the same as this slot, and it has not been used yet, then put the held item into the slot
                if (objectSlot.slotType == heldObj.objectType && !objectSlot.occupied)
                {
                    heldObj.enabled = false;

                    objectSlot.ReturnToSlot(heldObj);

                    didScore = true;
                }
            }
        }

        heldObj = null;
        if(didScore)
        {
            gameManager.AddToScore();

        }
    }

    //Updates the line renderer for the tractor beam and moves the object to match the player's position
    void UpdateTractorBeam()
    {
        lineRenderer.enabled = !(heldObj == null);

        if (heldObj == null)
            return;

        float distance = Vector3.Distance(heldObj.transform.position, followPos.position);
        if (distance > maxDistance)
        {
            DropObject(false);

            return;
        }

        //Set the two ends of the line to follow the player and the held object
        lineRenderer.SetPosition(0, followPos.transform.position);
        lineRenderer.SetPosition(1, heldObj.transform.position);

        //Lerp the color of the line based on distance
        lineRenderer.material.Lerp(goodMaterial, badMaterial, distance / maxDistance);
    }
}
