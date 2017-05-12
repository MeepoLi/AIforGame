using UnityEngine;
using System.Collections;

public class SphereProjection : MonoBehaviour {
    GameObject targetObject;
    GameObject plane;
    public string targetName;
    // Use this for initialization
    void Start () { 
    }

    void GetCoordinate() {

         targetObject = GameObject.Find(targetName);
         plane = GameObject.Find("SpherePlane");

    }
    // Update is called once per frame
    void Update () {
       
        if (plane!=null && targetObject!=null)
        {
            RaycastHit[] hits;
           // RaycastHit[] hitstest;
            Vector3 cameraPostion = Camera.main.transform.position;
            Vector3 directionToTarget = (this.targetObject.transform.position - cameraPostion);

            float minDis = 2 * Mathf.Sqrt(2);
            float maxDis = 0;
            //Debug.Log("Cast Ray...");
            hits = Physics.RaycastAll(cameraPostion, directionToTarget, 300.0f);
            
            Vector3 newPosition = new Vector3(0, 0, 0);
            bool ifhit = false;
            for (int i = 0; i < hits.Length; i++)
            {
                RaycastHit hit = hits[i];
               // Debug.Log("HitObj  = " + hit.transform.gameObject.name);
                if (hit.transform.gameObject.name != "SpherePlane") continue;

               // Debug.Log("Hit " + i + " = " + hit.point);
                if (hit.distance > maxDis)
                {
                    maxDis = hit.distance;
                    ifhit = true;
                  //  Debug.Log(targetName + ", hit position = " + this.transform.position);
                    //Debug.Log("Direction = " + directionToTarget); 
                    newPosition = new Vector3(hit.point.x, hit.point.y, hit.point.z); 
                }
            }

            if (ifhit)
            {
                this.transform.position = newPosition;
               // Debug.Log("position = " + newPosition); 
                
            }
            else
                this.transform.position = new Vector3(0, 0, 0);


        }
        
    }
}
