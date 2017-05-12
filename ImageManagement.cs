using UnityEngine;
using System.Collections;

public class ImageManagement : MonoBehaviour
{
    string url1 = "http://www.vsteel.vn/content/hinh/tin/2015829111951677.jpg";
    //"http://172.22.72.33:5000/get_image?type=1";

    string url2 = "https://geo3.ggpht.com/cbk?panoid=SJGKkSrz8Esm0oD6oT5lXQ&output=thumbnail&cb_client=search.TACTILE.gps&thumb=2&w=408&h=200&yaw=270.1157&pitch=0&thumbfov=100";

    string url3 = "https://lh4.googleusercontent.com/proxy/R5VP9fO235qYV6HNrV2m7HQ00GYUjirm1pCIM7cq8ZQBMOpvBzP1KsBusbZghaKT1tKrOhPqVWPw5Qky8rbDhmsHzCVcx3pROoYq00pljTZtoiVWKX2eOCG3Yj0uGnpZBJ9P_8oD7kK6Gp_QhXKFl_E1d9ag3Xo=w408-h308-k-no";
    //"http://172.22.72.33:5000/get_image?type=2";

    // Use this for initialization

    IEnumerator onHit(string gameObjectName)
    {
        WWW www;
        Texture2D tex;
        switch (gameObjectName) {
            case "Bridge":
                www = new WWW(url1);
                Debug.Log("downloading Bridge...");
                yield return www;
                //yield return new WaitForSeconds(2);
                tex = new Texture2D(800, 600);
                www.LoadImageIntoTexture(tex);
                GetComponent<Renderer>().material.mainTexture = tex;
                break;
                
            case "NYP":
                www = new WWW(url2);
                Debug.Log("downloading NYP...");
                yield return www;
                //yield return new WaitForSeconds(2);
                tex = new Texture2D(800, 600);
                www.LoadImageIntoTexture(tex);
                GetComponent<Renderer>().material.mainTexture = tex;
                break;
                
            case "Urban":
                www = new WWW(url3);
                Debug.Log("downloading Urban...");
                yield return www;
                //yield return new WaitForSeconds(1);
                tex = new Texture2D(800, 600);
                www.LoadImageIntoTexture(tex);
                GetComponent<Renderer>().material.mainTexture = tex;
                break;
            
        }

    }
    void Start()
    {
     
    }

}
