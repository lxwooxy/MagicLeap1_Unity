using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.MagicLeap;

public class GestureScript : MonoBehaviour {

    private bool FistHandPose = false;
    private float speed = 30.0f;  // Speed of our cube
    private float distance = 2.0f; // Distance between Main Camera and Cube
    private GameObject cube; // Reference to our Cube
    private MLHandTracking.HandKeyPose[] gestures; // Holds the different gestures we will look for

    void Start () {
        MLHandTracking.Start();

        gestures = new MLHandTracking.HandKeyPose[4];
        gestures[0] = MLHandTracking.HandKeyPose.Ok;
        gestures[1] = MLHandTracking.HandKeyPose.Fist;
        gestures[2] = MLHandTracking.HandKeyPose.OpenHand;
        gestures[3] = MLHandTracking.HandKeyPose.Finger;
        MLHandTracking.KeyPoseManager.EnableKeyPoses(gestures, true, false);

        cube = GameObject.Find("Cube");
        cube.SetActive(false);
    }

    void OnDestroy() {
        MLHandTracking.Stop();
    }

    void Update() {
        // Print the name of the discovered gesture and how confident the gesture is
        if (GetGesture(MLHandTracking.Left, MLHandTracking.HandKeyPose.Ok)
        || GetGesture(MLHandTracking.Right, MLHandTracking.HandKeyPose.Ok)) {
            Debug.Log("OK");
        }
        if (GetGesture(MLHandTracking.Left, MLHandTracking.HandKeyPose.Fist)
        || GetGesture(MLHandTracking.Right, MLHandTracking.HandKeyPose.Fist)) {
            Debug.Log("Fist");
        }
        if (GetGesture(MLHandTracking.Left, MLHandTracking.HandKeyPose.OpenHand)
        || GetGesture(MLHandTracking.Right, MLHandTracking.HandKeyPose.OpenHand)) {
            Debug.Log("Open Hand");
        }
        if (GetGesture(MLHandTracking.Left, MLHandTracking.HandKeyPose.Finger)
        || GetGesture(MLHandTracking.Right, MLHandTracking.HandKeyPose.Finger)) {
            Debug.Log("Finger");
        }
        else {
            Debug.Log("No gesture detected");
        }
        if (FistHandPose) {
            if (GetGesture(MLHandTracking.Left, MLHandTracking.HandKeyPose.OpenHand)
            || GetGesture(MLHandTracking.Right, MLHandTracking.HandKeyPose.OpenHand))
                cube.transform.Rotate(Vector3.up, + speed * Time.deltaTime);

            if (GetGesture(MLHandTracking.Left, MLHandTracking.HandKeyPose.Fist)
            || GetGesture(MLHandTracking.Right, MLHandTracking.HandKeyPose.Fist))
                cube.transform.Rotate(Vector3.up, -speed * Time.deltaTime);

            if (GetGesture(MLHandTracking.Left, MLHandTracking.HandKeyPose.Finger))
                cube.transform.Rotate(Vector3.right, +speed * Time.deltaTime);

            if (GetGesture(MLHandTracking.Right, MLHandTracking.HandKeyPose.Finger))
                cube.transform.Rotate(Vector3.right, -speed * Time.deltaTime);

            // if both hands are ok, set the cube to inactive
            if (GetGesture(MLHandTracking.Left, MLHandTracking.HandKeyPose.Ok)
            && GetGesture(MLHandTracking.Right, MLHandTracking.HandKeyPose.Ok)) {
                FistHandPose = false;
                cube.SetActive(false);
            }
        }
        else {
            // If both hands are fists, set the cube to active
            if (GetGesture(MLHandTracking.Left, MLHandTracking.HandKeyPose.Fist)
            && GetGesture(MLHandTracking.Right, MLHandTracking.HandKeyPose.Fist)) {
                FistHandPose = true;
                cube.SetActive(true);
                cube.transform.position = transform.position + transform.forward * distance;
                cube.transform.rotation = transform.rotation;
            }
        }
    }

    bool GetGesture(MLHandTracking.Hand hand, MLHandTracking.HandKeyPose type)  {
        if (hand != null) {
            if (hand.KeyPose == type) {
                if (hand.HandKeyPoseConfidence > 0.9f) {
                    return true;
                }
            }
        }
        return false;
    }
}