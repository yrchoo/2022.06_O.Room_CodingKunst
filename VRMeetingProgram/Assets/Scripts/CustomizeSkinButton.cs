using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomizeSkinButton : MonoBehaviour
{
    private bool IsClick;
    public int skinN;
    private int skinData;
    public Toggle toggle;

    public float waitTime = 1f;
    public float ttime = 0f;

    // Start is called before the first frame update
    void Start()
    {
        // while(true){
        //     ttime += Time.deltaTime;
        //     if (ttime>waitTime) break;
        // }
        IsClick = false;
        skinData = GameObject.Find("MyCharacter").GetComponent<GetCustomizeData>().oldSkinData;
        CheckToggle();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void CheckToggle(){
        if(skinN == skinData){
            toggle.isOn = true;
        }
    }

    public void ChangeSkin(bool clicked){
        IsClick = clicked;
        skinData = skinN;
        GameObject.Find("MyCharacter").GetComponent<GetCustomizeData>().skinData = skinData;
        clicked = false;
    }
}
