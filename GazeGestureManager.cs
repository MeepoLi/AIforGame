using UnityEngine;
using UnityEngine.VR.WSA.Input;
using System.Collections;

public class GazeGestureManager : MonoBehaviour {

    public static GazeGestureManager Instance { get; private set; }
    public GameObject FocusedObject { get; private set; }

    // Represents the hologram that is currently being gazed at.
    AudioSource audioSource = null;
    AudioClip hitClip = null;
    AudioClip mcClip = null;
    public static int TargetNum = 5;
     


    GameObject InfoPlane;

    GestureRecognizer recognizer;
    bool place = false;
    string FocusedObjectName = "";
    // Use this for initialization
    void Start () {

        Instance = this; 
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
        audioSource.spatialize = true;
        audioSource.spatialBlend = 1.0f;
        audioSource.dopplerLevel = 0.1f;
        audioSource.rolloffMode = AudioRolloffMode.Logarithmic;
        audioSource.maxDistance = 20f;

        hitClip = Resources.Load<AudioClip>("shotClip");
        mcClip = Resources.Load<AudioClip>("missionComplete");
        // Set up a GestureRecognizer to detect Select gestures.
        recognizer = new GestureRecognizer();
        recognizer.TappedEvent += (source, tapCount, ray) =>
        {
            // Send an OnSelect message to the focused object and its ancestors.
            if (!place)
            {
                GetComponent<TargetGenerator>().onGenerate();
                place = true;
                SpatialMapping.Instance.DrawVisualMeshes = false;
            }
            else
            {
                if (TargetNum <= 0)
                {
                    Debug.Log("MUSIC!");
                    audioSource.Stop();
                    audioSource.clip = mcClip;
                    audioSource.Play();

                }
                else
                {
                    audioSource.clip = hitClip;
                    audioSource.Stop();
                    audioSource.Play();
                }
                if (FocusedObject != null)
                {
                    Debug.Log("HitObj is " + FocusedObject.name);
                    FocusedObject.SendMessageUpwards("OnHit");
                }

                Debug.Log("num left = " + TargetNum);
            }
        };
        recognizer.StartCapturingGestures();
    }
	
	// Update is called once per frame
	void Update () {

        GameObject oldFocusObject = FocusedObject;

        // Do a raycast into the world based on the user's
        // head position and orientation.
         
        var headPosition = Camera.main.transform.position;
        var gazeDirection = Camera.main.transform.forward;

        RaycastHit[] hits;
        hits = Physics.RaycastAll(headPosition, gazeDirection, 300.0f);
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
                    FocusedObject = hit.collider.gameObject;
                    ifHit = true;
                    break;
                }

                
            }
        }
         
        if (!ifHit)
        {
            // If the raycast did not hit a hologram, clear the focused object.
            
            FocusedObject = null;
        }
       
        // If the focused object changed this frame,
        // start detecting fresh gestures again.
            if (FocusedObject != oldFocusObject)
        {
            recognizer.CancelGestures();
            recognizer.StartCapturingGestures();
        }
    }
}
