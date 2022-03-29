using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomizeModelButton : MonoBehaviour
{
    private bool IsClick;
    public int modelN = 0;
    private int modelData;

    // Start is called before the first frame update
    void Start()
    {
        IsClick = false;
        modelData = GameObject.Find("MyCharacter").GetComponent<GetCustomizeData>().modelData;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeModel(bool clicked){
        IsClick = clicked;
        modelData = modelN;
        GameObject.Find("MyCharacter").GetComponent<GetCustomizeData>().modelData = modelData;
        clicked = false;
    }
}
