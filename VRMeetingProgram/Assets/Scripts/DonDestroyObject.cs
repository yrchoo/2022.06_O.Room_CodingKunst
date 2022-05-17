using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DonDestroyObject : MonoBehaviour
{
    
    public string PFuserID;
    public GameObject ddObject;

    public void awake()
    {
        DontDestroyOnLoad(ddObject);
    }
}
