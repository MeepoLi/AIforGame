﻿using UnityEngine;
using System.Collections;

public class TapToPlace : MonoBehaviour {
    bool placing = false;
    // Use this for initialization
    void OnSelect(bool place) {

        placing = place;
        // If the user is in placing mode, display the spatial mapping mesh. 
        Renderer[] allChildRenderers = this.GetComponentsInChildren<Renderer>(); 
        if (placing)
        {
            SpatialMapping.Instance.DrawVisualMeshes = true;
            MeshRenderer render = gameObject.GetComponentInChildren<MeshRenderer>();
            render.enabled = true;

            foreach (Renderer R in allChildRenderers)
                R.enabled = true;
        }
        // If the user is not in placing mode, hide the spatial mapping mesh.
        else
        {
            SpatialMapping.Instance.DrawVisualMeshes = false;
            MeshRenderer render = gameObject.GetComponentInChildren<MeshRenderer>();
            render.enabled = false; 
            foreach (Renderer R in allChildRenderers)
                R.enabled = false;
        } 
    }
	
	// Update is called once per frame
	void Update () {
        // If the user is in placing mode,
        // update the placement to match the user's gaze.

        if (placing)
        {
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
                this.transform.parent.position = hitInfo.point; 
                // Rotate this object's parent object to face the user.
                Quaternion toQuat = Camera.main.transform.localRotation;
                toQuat.x = 0;
                toQuat.z = 0;
                this.transform.parent.rotation = toQuat;

               // this.transform.rotation = toQuat;
            }
        }
    }
}
