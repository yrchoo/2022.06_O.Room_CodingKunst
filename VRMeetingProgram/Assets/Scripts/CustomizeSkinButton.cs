using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomizeSkinButton : MonoBehaviour
{
    private bool IsClick;
    public int skinN = 0;
    private int skinData;

    // Start is called before the first frame update
    void Start()
    {
        IsClick = false;
        skinData = GameObject.Find("MyCharacter").GetComponent<GetCustomizeData>().skinData;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeSkin(bool clicked){
        IsClick = clicked;
        skinData = skinN;
        GameObject.Find("MyCharacter").GetComponent<GetCustomizeData>().skinData = skinData;
        clicked = false;
    }
}
