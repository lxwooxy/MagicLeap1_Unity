using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.MagicLeap;

public class DestroyObject : MonoBehaviour
{
    private MLInput.Controller controller;

    // variable to store the name of the last destroyed object
    private string lastDestroyedObjectName;
    // Start is called before the first frame update
    void Start()
    {
        MLInput.Start();
        MLInput.OnControllerButtonDown += OnButtonDown;
        controller = MLInput.GetController(MLInput.Hand.Left);
    }
    private void OnButtonDown(byte controllerId, MLInput.Controller.Button button)
    {
        if(button == MLInput.Controller.Button.HomeTap)
        {
            RaycastHit hit;
            if (Physics.Raycast(controller.Position, transform.forward, out hit))
            {
                // if the object is of tag "placedobject"
                if (hit.collider.gameObject.tag == "placedobject")
                {
                    // set the last destroyed object name
                    lastDestroyedObjectName = hit.collider.gameObject.tag;
                    Debug.Log("last destroyed object name: " + lastDestroyedObjectName);
                    Destroy(hit.collider.gameObject);
                }
                else
                {
                    // Destroy all game objects in the scene with the same name as the last destroyed object
                    GameObject[] objects = GameObject.FindGameObjectsWithTag(lastDestroyedObjectName);
                    Debug.Log("objects length: " + objects.Length);
                    foreach (GameObject obj in objects)
                    {
                        Destroy(obj);
                    }
                }
                
            } 
        }
    }
    private void OnDestroy()
    {
        MLInput.Stop();
        MLInput.OnControllerButtonDown -= OnButtonDown;
        //MLInput.OnTriggerDown -= OnTriggerDown;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}

