using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.MagicLeap;
using UnityEngine.UI;

public class PlanesSnippet : MonoBehaviour 
{
    public int maxPlaneResults = 50;
    public Text TextDisplay;
    public Transform cameraTransform;
    public Vector3 boundsExtents;
    public GameObject planePrefab;

    public MLPlanes.QueryFlags QueryFlags;
    private MLPlanes.QueryParams _queryParams;

    private List<GameObject> _planeCache = new List<GameObject>();

    void Start () 
    {
        // Start Planes.
        MLPlanes.Start();

        // The max number of planes returned by the query.
        //_queryParams.MaxResults = 50; // play with this number  
        _queryParams.MaxResults = (uint)maxPlaneResults; 

        // Set the Planes Request to repeat every 5 secs.
        InvokeRepeating("RequestPlanes", 1f, 5f);
    }

    private void OnDestroy() 
    {
        //Stop Planes.
        MLPlanes.Stop();
    }

    void RequestPlanes() 
    {
        
        _queryParams.MaxResults = (uint)maxPlaneResults;
        // Update the query parameters.
        _queryParams.Flags = QueryFlags;
        _queryParams.BoundsCenter = cameraTransform.position;
        _queryParams.BoundsRotation = cameraTransform.rotation;
        _queryParams.BoundsExtents = boundsExtents;

        // Make planes request with parameter list.
        MLPlanes.GetPlanes(_queryParams, HandleOnReceivedPlanes);
    }

    private void HandleOnReceivedPlanes(MLResult result, MLPlanes.Plane[] planes, 
                                        MLPlanes.Boundaries[] boundaries) 
    {

        // Empty the planes cache.
        for (int i = _planeCache.Count - 1; i >= 0; --i) 
        {
            Destroy(_planeCache[i]);
            _planeCache.Remove(_planeCache[i]);
        }

        // For each plane, instantiate a quad and set the quad dimensions
        // to match the plane.
        for (int i = 0; i < planes.Length; ++i) 
        {
            GameObject newPlane = Instantiate(planePrefab);
            newPlane.transform.position = planes[i].Center;
            newPlane.transform.rotation = planes[i].Rotation;
            newPlane.transform.localScale = new Vector3(planes[i].Width, 
                                                        planes[i].Height, 1f);
            _planeCache.Add(newPlane);
        }
    }
}