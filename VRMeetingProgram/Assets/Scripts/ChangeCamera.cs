using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeCamera : MonoBehaviour
{
    public GameObject cam;
    public bool longDistance;

    public bool isKeyboardOn;

    private float fDestroyTime = 1f;
    private float fTickTime = 1f;
    // Start is called before the first frame update
    void Start()
    {
        isKeyboardOn = false;
    }

    // Update is called once per frame
    void Update()
    {
         fTickTime += Time.deltaTime;
        if (fTickTime >= fDestroyTime)
        {
            if(!isKeyboardOn){
                if(Input.GetKey(KeyCode.C)){
                    longDistance = !longDistance;
                    if(longDistance){
                        cam.GetComponent<CameraMovement>().maxDistance = 1.5f;
                        cam.GetComponent<CameraMovement>().finalDistance = 1f;
                    }
                    else{
                        cam.GetComponent<CameraMovement>().maxDistance = 0f;
                        cam.GetComponent<CameraMovement>().finalDistance = 0f;
                    }
                    fTickTime = 0f;
                }
            }
        }
    }
}
