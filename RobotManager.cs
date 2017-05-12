using UnityEngine;
using System.Collections;

public class RobotManager : MonoBehaviour {

    // Use this for initialization
    int hits = 2;
    private MeshRenderer meshRenderer;
    AudioSource expAudio = null;
    AudioClip expClip = null;
    void Start () { 
        

        expAudio = gameObject.AddComponent<AudioSource>();
        expAudio.playOnAwake = false;
        expAudio.spatialize = true;
        expAudio.spatialBlend = 1.0f;
        expAudio.dopplerLevel = 0.1f;
        expAudio.rolloffMode = AudioRolloffMode.Logarithmic;
        expAudio.maxDistance = 20f;
        expClip = Resources.Load<AudioClip>("expClip");
    }

    // Update is called once per frame
    void Update () {
        meshRenderer = this.gameObject.GetComponentInChildren<MeshRenderer>();
    }

    void OnHit() {
        
        if (hits == 0)
        {
          /* expAudio.clip = expClip;
            expAudio.Play();*/
            this.gameObject.transform.localScale = new Vector3(0, 0, 0);
            this.gameObject.transform.position = new Vector3(-100, -100, -100);
            GazeGestureManager.TargetNum--;


            return;
        }
        else
        {
            hits--;
            for (int i = 0; i < 10; i++)
                this.gameObject.transform.localScale -= new Vector3(0.005f, 0.005f, 0.005f);
        }
    }
}
