using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomizeGenderButton : MonoBehaviour
{
    private bool IsClick;
    public int genderN;
    private int genderData;
    public Toggle toggle;

    // Start is called before the first frame update
    void Start()
    {
        IsClick = false;
        genderData = GameObject.Find("MyCharacter").GetComponent<GetCustomizeData>().genderData;
        CheckToggle();
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void CheckToggle(){
        if(genderN == genderData){
            toggle.isOn = true;
        }
    }

    public void ChangeGender(bool clicked){
        IsClick = clicked;
        GameObject.Find("MyCharacter").GetComponent<GetCustomizeData>().genderData = genderN;
        clicked = false;
    }
}
