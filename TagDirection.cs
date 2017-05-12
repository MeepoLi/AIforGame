using UnityEngine;
using System.Collections;


 
public class TagDirection : MonoBehaviour {

    // Use this for initialization
    public Quaternion DefaultRotation { get; private set; }
    void Start () {
        DefaultRotation = this.transform.rotation;
    }
	
	// Update is called once per frame
	void Update () {
        Vector3 directionToTarget = Camera.main.transform.position - this.transform.position;

        // Adjust for the pivot axis.
       
        directionToTarget.x = gameObject.transform.position.x; 
       // directionToTarget.y = gameObject.transform.position.y;
        // Calculate and apply the rotation required to reorient the object and apply the default rotation to the result.
        this.transform.rotation = Quaternion.LookRotation(-directionToTarget) * DefaultRotation;
    }
}
