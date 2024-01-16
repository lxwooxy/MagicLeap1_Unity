using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.XR.MagicLeap;

public class LocationSnippet : MonoBehaviour
{
    public Text textDisplay;

    void Start() 
    {
        // Start Location API
        MLLocation.Start();
    }

    void OnDestroy() 
    {
        // Stop Location API
        MLLocation.Stop(); 
    }

    void Update() 
    {
        GetLocation();
    }
    private void GetLocation() {

        // Request the coarse location
        MLResult result = MLLocation.GetLastCoarseLocation(out MLLocation.Location newData);

        if (result.IsOk) 
        {
            textDisplay.text = String.Format(
                "Latitude:\t<i>{0}</i>\n" +
                "Longitude:\t<i>{1}</i>\n" +
                "Postal Code:\t<i>{2}</i>",
                newData.Latitude.ToString(),
                newData.Longitude.ToString(),
                newData.HasPostalCode ? newData.PostalCode : "(unknown)"
            );
        }
        else 
        {
            textDisplay.text = String.Format("Location not available");
        }
    }
}