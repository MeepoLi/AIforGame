using UnityEngine;
using System.Collections;

public class WorldCursor : MonoBehaviour {
    private MeshRenderer meshRenderer;
    [Tooltip("Maximum gaze distance for calculating a hit.")]
    public float MaxGazeDistance = 1.5f;
    public bool Hit { get; private set; }
    public LayerMask RaycastLayerMask ;
    public Vector3 Position { get; private set; }
    public Vector3 Normal { get; private set; }

    // Use this for initialization
    void Start () {
         


        meshRenderer = this.gameObject.GetComponentInChildren<MeshRenderer>();
        meshRenderer.enabled = true;
        //SpatialMapping.Instance.DrawVisualMeshes = true;
    }
	
	// Update is called once per frame
	void Update () {
        var gazeOrigin = Camera.main.transform.position;
        var gazeDirection = Camera.main.transform.forward;

        RaycastHit[] hits;
        hits = Physics.RaycastAll(gazeOrigin, gazeDirection, 300.0f);
        bool ifHit = false;
        if (hits.Length > 0)
        {
            // If the raycast hit a hologram, use that as the focused object. 

            for (int i = 0; i < hits.Length; i++)
            {
                RaycastHit hit = hits[i];
                // Debug.Log("HitObj  = " + hit.transform.gameObject.name);

                if (hit.transform.gameObject.tag == "target")
                {
                    Position = hit.point;
                    Normal = hit.normal;
                    ifHit = true;
                    break;
                  
                }


            }
        }

        
        if (!ifHit) 
        {

            Position = gazeOrigin + (gazeDirection * MaxGazeDistance);
            Normal = gazeDirection;
        }
        
        this.transform.position = Position;

        this.transform.rotation = Quaternion.FromToRotation(Vector3.up, Normal);

        /*Debug.Log("cameraLoc = " + gazeOrigin);

        Debug.Log("Position = " + this.transform.position);
        Debug.Log("Rotation = " + this.transform.rotation);*/


        // If the raycast did not hit a hologram, hide the cursor mesh.
        //meshRenderer.enabled = true;

    }
}
