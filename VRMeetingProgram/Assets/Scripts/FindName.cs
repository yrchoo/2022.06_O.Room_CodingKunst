using System;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine.UI;

public class FindName : MonoBehaviour
{
    public ChangeScene CS;
    static public string myID;
    // Start is called before the first frame update
    void Start()
    {
        //GameObject.Find("PlayerName").GetComponent<TextMesh>().text;
        
        findUserName(myID);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void findUserName(string myID)
    {
        /*AreaScript Area = Instantiate(ListArea).GetComponent<AreaScript>();
        Area.transform.SetParent(MyRect.transform, false);
        Area.ListBoxRect.sizeDelta = new Vector2(600, Area.ListBoxRect.sizeDelta.y);  */


        var request = new GetUserDataRequest() { PlayFabId = myID };
        PlayFabClientAPI.GetUserData(request, (result) => {

            GameObject.Find("PlayerName").GetComponent<TextMesh>().text = result.Data["name"].Value;
            
        },
            (error) => print("데이터 불러오기 실패")
            );
    }
    //Fit(Area.ListBoxRect);
    //Fit(MyRect);
}

