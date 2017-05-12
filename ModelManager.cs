using UnityEngine;
using System.Collections;
using UnityEngine.VR.WSA.Input;


public class ModelManager : MonoBehaviour {
    public static ModelManager Instance { get; private set; }

    // Represents the hologram that is currently being gazed at.
    public GameObject Model1;
    public GameObject Model2;

    GestureRecognizer recognizer;
    int drawing_flag;
    // Use this for initialization

    ModelManager()
    {
        drawing_flag = 0;
    }
    void Start () {
        Instance = this;
        // Set up a GestureRecognizer to detect Select gestures.
        Renderer[] allChildRenderers = this.GetComponentsInChildren<Renderer>();
        foreach (Renderer R in allChildRenderers)
            R.enabled = false;


        recognizer = new GestureRecognizer();
        recognizer.TappedEvent += (source, tapCount, ray) =>
        {
            switch (drawing_flag)
            {
                case 0:
                    Model1.SendMessageUpwards("OnDraw");
                    Debug.Log("Message Sent1");
                    drawing_flag = 1;
                    break;
                case 1:
                    Model2.SendMessageUpwards("OnDraw");
                    Debug.Log("Message Sent2");
                    SpatialMapping.Instance.DrawVisualMeshes = false;
                    allChildRenderers = this.GetComponentsInChildren<Renderer>();
                    drawing_flag = 2;
                    
                    break;
                case 2:
                    Model1.SendMessageUpwards("OnClear");
                    Model2.SendMessageUpwards("OnClear");
                    SpatialMapping.Instance.DrawVisualMeshes = true;
                    drawing_flag = 0;
                    break;
            }
        };
        recognizer.StartCapturingGestures();
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
