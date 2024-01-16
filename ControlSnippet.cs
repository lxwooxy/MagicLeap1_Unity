using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.XR.MagicLeap;

public class ControlSnippet : MonoBehaviour 
{

    public Text textDisplay;
    private MLInput.Controller _control;

    void Start () 
    {
        // Start receiving input.
        MLInput.Start();

        // Choose the left hand for the controller.
        _control = MLInput.GetController(MLInput.Hand.Left);
    }
    void OnDestroy () 
    {
        // Stop receiving input.
        MLInput.Stop();
    }
    void Update () 
    {
        // Display the position and orientation in a text field.
        // textDisplay.text = String.Format(
        //     "Position:\t<i>{0}</i>\n" +
        //     "Rotation:\t<i>{1}</i>\n",
        //     _control.Position.ToString(),
        //     _control.Orientation.eulerAngles.ToString()
        // );
    }
}