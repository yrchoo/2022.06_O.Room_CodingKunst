using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomizeGenderButton : MonoBehaviour
{
    private bool IsClick;
    public int genderN = 0;
    private int genderData;

    // Start is called before the first frame update
    void Start()
    {
        IsClick = false;
        genderData = GameObject.Find("MyCharacter").GetComponent<GetCustomizeData>().genderData;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeGender(bool clicked){
        IsClick = clicked;
        genderData = genderN;
        GameObject.Find("MyCharacter").GetComponent<GetCustomizeData>().genderData = genderData;
        clicked = false;
    }
}
