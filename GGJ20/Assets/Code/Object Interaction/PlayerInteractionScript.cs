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

    LineRenderer lineRenderer;
    GameManager gameManager;

    private void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.enabled = false;
        gameManager = FindObjectOfType<GameManager>();

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

        bool didScore = false;

        if (manualDrop)
        {
            Collider[] objectsInRange = Physics.OverlapSphere(transform.position, objectFindRadius);

            foreach (Collider nearbyObj in objectsInRange)
            {
                ObjectSlot objectSlot = nearbyObj.GetComponent<ObjectSlot>();

                if (objectSlot == null)
                    continue;

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

    void UpdateTractorBeam()
    {
        lineRenderer.enabled = !(heldObj == null);

        if (heldObj == null)
            return;

        if (Vector3.Distance(heldObj.transform.position, followPos.position) > maxDistance)
        {
            DropObject(false);

            return;
        }

        lineRenderer.SetPosition(0, followPos.transform.position);
        lineRenderer.SetPosition(1, heldObj.transform.position);
    }
}
