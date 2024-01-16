using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.MagicLeap;

public class PlaceObject : MonoBehaviour
{
    public GameObject ObjectToPlace;
    private MLInput.Controller controller;
    // Start is called before the first frame update
    void Start()
    {
        MLInput.Start();
        //MLInput.OnControllerButtonDown += OnButtonDown;
        MLInput.OnTriggerDown += OnTriggerDown;
        controller = MLInput.GetController(MLInput.Hand.Left);
    }
     private void OnTriggerDown(byte controllerId, float triggerValue)
    {
       RaycastHit hit;
        if (Physics.Raycast(controller.Position, transform.forward, out hit))
        {
            // Place the object 0.05 units above the surface hit
            GameObject placeObject = Instantiate(ObjectToPlace, hit.point + new Vector3(0, 0.05f, 0), Quaternion.identity);
            //GameObject placeObject = Instantiate(ObjectToPlace, hit.point, Quaternion.identity);
        } 
    }
    private void OnDestroy()
    {
        MLInput.Stop();
        //MLInput.OnControllerButtonDown -= OnButtonDown;
        MLInput.OnTriggerDown -= OnTriggerDown;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
