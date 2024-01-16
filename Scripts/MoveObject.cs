using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.MagicLeap;

public class MoveObject : MonoBehaviour
{
     private MLInput.Controller controller;

    // Start is called before the first frame update
    void Start()
    {
        MLInput.Start();
        controller = MLInput.GetController(MLInput.Hand.Left);
        MLInput.OnControllerButtonDown += OnButtonDown;
        // get the home button

    }
    private void OnButtonDown(byte controllerId, MLInput.Controller.Button button)
    {
        if(button == MLInput.Controller.Button.Bumper)
        {
            // apply a force to the object at the end of the raycast, if it is a placed object
            RaycastHit hit;
            if (Physics.Raycast(controller.Position, transform.forward, out hit))
            {
                // if the object is of tag "placedobject"
                if (hit.collider.gameObject.tag == "placedobject")
                {
                    // apply a force to the object at the end of the raycast
                    hit.collider.gameObject.GetComponent<Rigidbody>().AddForce(transform.forward * 600);
                }
                
            } 
            
        }
    }
  
    // Update is called once per frame
    void Update()
    {
       
    }
}
