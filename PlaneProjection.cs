using UnityEngine;
using System.Collections;

public class PlaneProjection : MonoBehaviour {
    Vector3 targetPosition;
    GameObject plane;
    public string targetName;
    // Use this for initialization
    void Start () {
        this.gameObject.tag = "TagObject";
    }

    void GetCoordinate() {

         this.targetPosition = GameObject.Find(targetName).transform.position;
         plane = GameObject.Find("DisplayPlane");

    }

    
    // Update is called once per frame
    void Update () {
       
        if (plane!=null)
        { 
            Vector3 cameraPostion = Camera.main.transform.position;
            Vector3 directionToTarget = cameraPostion - this.targetPosition;
            Vector3 PlaneNormal = plane.transform.up;


            float d = Vector3.Dot(plane.transform.position - this.targetPosition, PlaneNormal) / Vector3.Dot(directionToTarget, PlaneNormal);

            this.transform.position = this.targetPosition + directionToTarget * d;// - new Vector3(0,0,2);

          //  Debug.Log("Plane Normal" + PlaneNormal);
          //  Debug.Log("Plane Location" + plane.transform.position);
          //  Debug.Log("Target Location" + this.transform.position);

          //  Debug.Log("------");

        }
        
    }
}
