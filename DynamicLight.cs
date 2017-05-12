using HoloToolkit;
using UnityEngine;
using System.Collections;

public class DynamicLight : Singleton<DynamicLight>
{
    public GameObject lightSource;
    public bool movingFlag;
    // Use this for initialization
    Vector3 lightPos;
    //bool direction;
    bool initialStatus;
    float LightSensitivity = 0.7f;
    void Start () {
       
        /*lightPos.x = -0.8f;
        lightPos.y = 0.7f;
        lightPos.z = 1.33f;
        */
        lightSource.transform.position = new Vector3(-0.8f, 0.7f, 4.0f);
        initialStatus = false;
        //  direction = true;
        movingFlag = false;

    }
	
	// Update is called once per frame
	void Update () {
        if (lightSource != null) {


            if (GestureManager.Instance.IsNavigating && GestureManager.Instance.ActiveFunction == 2)
            {
                if (!initialStatus)
                {
                    lightPos = lightSource.transform.position;
                    initialStatus = true;
                }
                else
                {
                    float newPosx = lightPos.x + GestureManager.Instance.NavigationPosition.x * LightSensitivity; ;
                    float newPosy = lightPos.y;
                    float newPosz = lightPos.z;
                    lightSource.transform.position = new Vector3(newPosx, newPosy, newPosz);
                }
            }
            else {

                initialStatus = false;
            }
            /*  if (lightPos.x > 2.0f)
                  direction = false;
              else 
              if (lightPos.x < -2.0f) 
                  direction = true;

              if (direction)
              {
                  lightPos.x += 0.008f;
              }
              else
              {
                  lightPos.x -= 0.008f;
              }
            

            lightSource.transform.position = new Vector3(lightPos.x, lightPos.y, lightPos.z);
              */

        }
    }
}
