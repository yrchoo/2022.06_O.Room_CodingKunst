using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookCam : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Cam;
    //public Transform Cam;

    Vector3 startScale;
    public float distance = 5;

    private void Start()
    {
        Cam = GameObject.Find("Main Camera");
        startScale = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        float dist = Vector3.Distance(Cam.transform.position, transform.position);
        Vector3 newScale = startScale * dist / distance;
        transform.localScale = newScale;

        transform.rotation = Cam.transform.rotation;
    }
}
