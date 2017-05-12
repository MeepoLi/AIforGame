using UnityEngine;
using System.Collections;

public class PlotManager : MonoBehaviour {


   public static PlotManager Instance { get; private set; }
    Renderer[] allChildRenderers;
    // Use this for initialization
    void Start () {
        Instance = this;
        allChildRenderers = this.GetComponentsInChildren<Renderer>();
        foreach (Renderer R in allChildRenderers)
            R.enabled = false;
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnClear()
    {
        Debug.Log("Message received");

        allChildRenderers = this.GetComponentsInChildren<Renderer>();
        foreach (Renderer R in allChildRenderers)
            R.enabled = false;

    }
    void OnDraw()
    {
        Debug.Log("Message received");

        allChildRenderers = this.GetComponentsInChildren<Renderer>();
        foreach (Renderer R in allChildRenderers)
            R.enabled = true;

        // SpatialMapping.Instance.DrawVisualMeshes = false;
        // Do a raycast into the world that will only hit the Spatial Mapping mesh.

        var headPosition = Camera.main.transform.position;
        var gazeDirection = Camera.main.transform.forward;

        RaycastHit hitInfo;
        if (Physics.Raycast(headPosition, gazeDirection, out hitInfo))
        {
            // Move this object's parent object to
            // where the raycast hit the Spatial Mapping mesh.
            // this.transform.position = hitInfo.point;
            this.transform.position = hitInfo.point;
            // Rotate this object's parent object to face the user.
            Quaternion toQuat = Camera.main.transform.localRotation;
            toQuat.z = 0;
           // toQuat.x = 0;

            this.transform.rotation = toQuat;

            // this.transform.rotation = toQuat;
        }

    }
}
