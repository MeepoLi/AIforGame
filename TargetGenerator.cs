using UnityEngine;
using System.Collections;

public class TargetGenerator : MonoBehaviour {

    [Tooltip("Drag the Target asset you want to display.")]
    public GameObject TargetObject;


    [Tooltip("Input the Target Num.")]
    public int num;


    public float maxX = 2.0f;
    public float maxY = 1.0f;
    public float maxZ = 3.0f;


    [Tooltip("Yes 1/ No 0")]
    public int attachToSurface = 1;

    [Tooltip("Yes 1/ No 0")]
    public int inFront = 0;

    [Tooltip("Yes 1/ No 0")]
    public int normaldistribution = 0;
     
    // Use this for initialization
    public void onGenerate() {
        Debug.Log("Generating Content...");
        if (TargetObject == null)
        {
            return;
        }
        if (num > 0)
        {
            for (int i = 0; i < num; i++)
            {

                float x, y, z;
                if (inFront == 1)
                {
                    x = Random.Range(0, maxX);
                    y = Random.Range(0, maxY);
                    z = Random.Range(0, maxZ);
                }
                else
                {
                    x = Random.Range(-maxX, maxX);
                    y = Random.Range(-maxY, maxY);
                    z = Random.Range(-maxZ, maxZ);
                }
                GameObject newTarget = GameObject.Instantiate(TargetObject);

                while (attachToSurface == 1)
                {

                    Vector3 direction = Camera.main.transform.position - new Vector3(x, y, z);
                    RaycastHit hitInfo;
                    if (Physics.Raycast(Camera.main.transform.position, direction, out hitInfo,
                        30.0f, SpatialMapping.PhysicsRaycastMask))
                    {
                        x = hitInfo.point.x;
                        y = hitInfo.point.y;
                        z = hitInfo.point.z;
                        break;
                    }
                    if (inFront == 1)
                    {
                        x = Random.Range(0, maxX);
                        y = Random.Range(0, maxY);
                        z = Random.Range(0, maxZ);
                    }
                    else
                    {
                        x = Random.Range(-maxX, maxX);
                        y = Random.Range(-maxY, maxY);
                        z = Random.Range(-maxZ, maxZ);
                    }
                }

                Debug.Log("Loc = (" + x + "," + y + "," + z + ")");
                newTarget.transform.position = new Vector3(x, y, z);
                newTarget.tag = "target";
                //newTarget.layer = RaycastLayerMask;
                Debug.Log(newTarget.layer);
                newTarget.SetActive(true);
                newTarget.AddComponent<RobotManager>();
            }
          

        }

    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
