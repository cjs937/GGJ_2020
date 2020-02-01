using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractionScript : MonoBehaviour
{
    public Transform followPos;
    public float objectFindRadius;

    [HideInInspector]
    public SlottedObject heldObj;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Interact"))
        {
            Debug.Log("tyring");
            if (heldObj == null)
                PickUpObject();
            else
                DropObject();
        }            
    }

    public void PickUpObject()
    {
        Collider[] objectsInRange = Physics.OverlapSphere(transform.position, objectFindRadius);
        float distanceToClosest = float.MaxValue;
        SlottedObject closestObject = null;

        foreach (Collider nearbyObj in objectsInRange)
        {
            SlottedObject slottedObj = nearbyObj.GetComponent<SlottedObject>();

            if (slottedObj == null || !slottedObj.enabled)
                continue;

            float distance = Vector3.Distance(transform.position, slottedObj.transform.position);
            
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

    public void DropObject()
    {
        if (!heldObj)
            return;

        Collider[] objectsInRange = Physics.OverlapSphere(transform.position, objectFindRadius);

        foreach (Collider nearbyObj in objectsInRange)
        {
            ObjectSlot objectSlot = nearbyObj.GetComponent<ObjectSlot>();

            if (objectSlot == null)
                continue;

            if(objectSlot.slotType == heldObj.objectType)
            {
                heldObj.enabled = false;

                objectSlot.ReturnToSlot(heldObj);
            }         
        }

        heldObj = null;
    }
}
