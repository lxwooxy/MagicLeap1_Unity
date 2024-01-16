using System.Collections;
using UnityEngine;
using UnityEngine.XR.MagicLeap;

public class RaycastSnippet : MonoBehaviour 
{

    public Transform camTransform;
    public GameObject prefab;

    private MLRaycast.QueryParams _raycastParams = new MLRaycast.QueryParams();

    void Start() 
    {
        // Start raycasting.
        MLRaycast.Start();
    }

    private void OnDestroy() 
    {
        // Stop raycasting.
        MLRaycast.Stop();
    }

    void Update()
    {
        // Update the orientation data in the raycast parameters.
        _raycastParams.Position = camTransform.position;
        _raycastParams.Direction = camTransform.forward;
        _raycastParams.UpVector = camTransform.up;

        // Make a raycast request using the raycast parameters 
        MLRaycast.Raycast(_raycastParams, HandleOnReceiveRaycast);
    }

    private IEnumerator NormalMarker(Vector3 point, Vector3 normal) 
    {
        // Rotate the prefab to match given normal.
        Quaternion rotation = Quaternion.FromToRotation(Vector3.up, normal);
        // Instantiate the prefab at the given point.
        GameObject go = Instantiate(prefab, point, rotation);
        // Wait 2 seconds then destroy the prefab.
        yield return new WaitForSeconds(1);
        Destroy(go);
    }

    void HandleOnReceiveRaycast( MLRaycast.ResultState state, 
                                UnityEngine.Vector3 point, Vector3 normal, 
                                float confidence) 
    {
        if (state ==  MLRaycast.ResultState.HitObserved) 
        {
            StartCoroutine(NormalMarker(point, normal));
        }
    }
}