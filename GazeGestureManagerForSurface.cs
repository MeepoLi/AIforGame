using UnityEngine;
using UnityEngine.VR.WSA.Input;
using System.Collections;

public class GazeGestureManagerForSurface : MonoBehaviour
{

    public static GazeGestureManagerForSurface Instance { get; private set; }

    // Represents the hologram that is currently being gazed at.
    public GameObject FocusedObject { get; private set; }

    GestureRecognizer recognizer;
    bool drawing_flag = false;
    // Use this for initialization
    void Start()
    {
        Instance = this;
        // Set up a GestureRecognizer to detect Select gestures.
        Renderer[] allChildRenderers = this.GetComponentsInChildren<Renderer>();
        foreach (Renderer R in allChildRenderers)
            //R.enabled = false;
            R.enabled = true;

        recognizer = new GestureRecognizer();
        recognizer.TappedEvent += (source, tapCount, ray) =>
        {
            // Send an OnSelect message to the focused object and its ancestors.
            if (!drawing_flag)
            {
                allChildRenderers = this.GetComponentsInChildren<Renderer>();
                foreach (Renderer R in allChildRenderers)
                    R.enabled = true;

                // SpatialMapping.Instance.DrawVisualMeshes = false;
                // Do a raycast into the world that will only hit the Spatial Mapping mesh.

                var headPosition = Camera.main.transform.position;
                var gazeDirection = Camera.main.transform.forward;

                RaycastHit hitInfo;
                if (Physics.Raycast(headPosition, gazeDirection, out hitInfo,
                    30.0f, SpatialMapping.PhysicsRaycastMask))
                {
                    // Move this object's parent object to
                    // where the raycast hit the Spatial Mapping mesh.
                    // this.transform.position = hitInfo.point;
                    this.transform.position = hitInfo.point;
                    // Rotate this object's parent object to face the user.
                    Quaternion toQuat = Camera.main.transform.localRotation;
                    toQuat.x = 0;
                    toQuat.z = 0;
                    this.transform.rotation = toQuat; 
                }
                SpatialMapping.Instance.DrawVisualMeshes = false;
            }
            else
            {
                SpatialMapping.Instance.DrawVisualMeshes = true;
                allChildRenderers = this.GetComponentsInChildren<Renderer>();
                foreach (Renderer R in allChildRenderers)
    //                R.enabled = false;
                    R.enabled = true;
            }
            drawing_flag = !drawing_flag;


        };
        recognizer.StartCapturingGestures();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
