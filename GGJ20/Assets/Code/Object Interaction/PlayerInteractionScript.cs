using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractionScript : MonoBehaviour
{
    public Transform followPos;
    public float objectFindRadius;

    [HideInInspector]
    public SlottedObject heldObj;

    LineRenderer lineRenderer;

    private void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.enabled = false;

        //lineRenderer.positionCount = 4;

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
        Collider[] objectsInRange = Physics.OverlapSphere(transform.position, objectFindRadius);
        float distanceToClosest = float.MaxValue;
        SlottedObject closestObject = null;

        foreach (Collider nearbyObj in objectsInRange)
        {
            SlottedObject slottedObj = nearbyObj.GetComponent<SlottedObject>();

            if (slottedObj == null || !slottedObj.enabled)
                continue;

            float distance = Vector3.Distance(followPos.transform.position, slottedObj.transform.position);
            
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

        if (manualDrop)
        {
            Collider[] objectsInRange = Physics.OverlapSphere(transform.position, objectFindRadius);

            foreach (Collider nearbyObj in objectsInRange)
            {
                ObjectSlot objectSlot = nearbyObj.GetComponent<ObjectSlot>();

                if (objectSlot == null)
                    continue;

                if (objectSlot.slotType == heldObj.objectType)
                {
                    heldObj.enabled = false;

                    objectSlot.ReturnToSlot(heldObj);
                }
            }
        }

        heldObj = null;
    }

    void UpdateTractorBeam()
    {
        lineRenderer.enabled = !(heldObj == null);

        if (heldObj == null)
            return;

        lineRenderer.SetPosition(0, followPos.transform.position);
        lineRenderer.SetPosition(1, heldObj.transform.position);

        //Vector3 midPoint = (followPos.transform.position + heldObj.transform.position) / 2;
    }
}
