using UnityEngine;
using UnityEngine.XR.MagicLeap;

public class MeshingSnippet : MonoBehaviour 
{

    public MLSpatialMapper mapper;
    public Material matPointCloud;
    public Material matWireframe;

    void Start() 
    {
        // Start Magic Leap input
        MLInput.Start();

        // Add the Control button callback
        //MLInput.OnControllerButtonDown += _buttonDownCallback;
    }

    void OnDestroy() 
    {
        // Remove the Control button callback
        //MLInput.OnControllerButtonDown -= _buttonDownCallback;

        // Stop Magic Leap input
        MLInput.Stop();
    }

    private void toggleMeshing() 
    {
        // Toggle meshing on/off
        mapper.enabled = mapper.enabled ? false : true;

        // Loop over the meshes and swap the material assignment
        for (int i = 0; i < transform.childCount; i++) 
        {
            GameObject gObject = transform.GetChild(i).gameObject;
            MeshRenderer meshRenderer = 
                        gObject.GetComponentInChildren<MeshRenderer>();
            if (mapper.enabled) 
            {
                meshRenderer.material = matWireframe;
            }
            else 
            {
                meshRenderer.material = matPointCloud;
            }
        }
    }

    // Callback - Bumper controls toggling meshing on/off
    private void _buttonDownCallback(byte controller_id, 
                                     MLInput.Controller.Button button) 
    {
        if (button == MLInput.Controller.Button.Bumper) 
        {
            toggleMeshing();
        }       
    }
}