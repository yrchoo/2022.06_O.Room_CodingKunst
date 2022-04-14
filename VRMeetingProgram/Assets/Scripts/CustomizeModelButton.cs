using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomizeModelButton : MonoBehaviour
{
    private bool IsClick;
    public int modelN = 0;
    private int modelData;
    public Toggle toggle;

    // Start is called before the first frame update
    void Start()
    {
        IsClick = false;
        modelData = GameObject.Find("MyCharacter").GetComponent<GetCustomizeData>().oldModelData;
        CheckToggle();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CheckToggle(){
        if(modelN == modelData){
            toggle.isOn = true;
        }
    }

    public void ChangeModel(bool clicked){
        IsClick = clicked;
        modelData = modelN;
        GameObject.Find("MyCharacter").GetComponent<GetCustomizeData>().modelData = modelData;
        clicked = false;
    }
}
