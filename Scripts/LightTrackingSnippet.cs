using UnityEngine;
using System;
using UnityEngine.UI;
using UnityEngine.XR.MagicLeap;

public class LightTrackingSnippet : MonoBehaviour 
{
    
    public Text textDisplay;

    void Start() 
    {
        // Start LightTracking
        MLLightingTracking.Start(); 
    }
    
    void OnDestroy() 
    {
        // Stop LightTracking
        MLLightingTracking.Stop();
    }
    
    void Update() 
    {
        
        textDisplay.text = String.Format(
            "Color:\t<i>{0}</i>\n" +
            "Ave Luminence:\t<i>{1}</i>\n",
            MLLightingTracking.GlobalTemperatureColor.ToString(),
            MLLightingTracking.AverageLuminance.ToString()
        );
    }
}