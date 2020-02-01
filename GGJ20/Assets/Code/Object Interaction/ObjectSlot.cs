using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSlot : MonoBehaviour
{
    public Transform lerpStart;
    public Transform lerpEnd;
    SlottedObject slottedObj;
    bool lerping = false;
    float currentLerpTime;
    public float lerpTime;
    public ObjectType slotType;

    // Update is called once per frame
    void Update()
    {
        if(lerping)
        {
            currentLerpTime += Time.deltaTime;

            slottedObj.transform.position = Vector3.Lerp(lerpStart.position, lerpEnd.position, currentLerpTime / lerpTime);

            if (currentLerpTime >= lerpTime)
                lerping = false;
        }
    }

    public void ReturnToSlot(SlottedObject slottedObject)
    {
        slottedObj = slottedObject;
        lerping = true;
        currentLerpTime = 0;
        slottedObject.transform.rotation = transform.rotation;
        slottedObject.GetComponent<Collider>().enabled = false;
        slottedObject.rigidbody.isKinematic = true;
    }
}
