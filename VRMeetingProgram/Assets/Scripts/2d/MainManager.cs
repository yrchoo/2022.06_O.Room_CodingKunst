using System;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine.UI;

public class MainManager : MonoBehaviour
{
    
    public ChangeScene CS;
    static public string myID;

    [Header("MemberPanel")]
    public GameObject MemberPanel;
    public GameObject ListArea;
    public RectTransform ContentRect, MyRect;
    public Scrollbar scrollBar;
    public Toggle MineToggle;

    public Text State, Name, Role;
    public Image Image;

    //bool isLoaded;
    

    [Header("Disconnect")]
    public PlayerLeaderboardEntry MyPlayFabInfo; //내 정보 다 들어감
    public List<PlayerLeaderboardEntry> PlayFabUserList = new List<PlayerLeaderboardEntry>();

    void Awake()
    {
        /*SceneManager = GameObject.Find("PFuserID");
        Debug.Log(SceneManager.GetComponent<DonDestroyObject>().PFuserID);*/
        
        myID = CS.Load("userId");       
        GetLeaderboard(myID);
        ShowMyProfile(myID);
    }

    // Update is called once per frame
    void Update()
    {
        //Load();
    }

    #region 사원버튼 클릭
    public void MemberClick()
    {
        MemberPanel.SetActive(true);
    }
    #endregion

    #region 채팅버튼 클릭
    public void ChatClick()
    {
        //MemberPanel.SetActive(false);
        try
        {
            CS.LoadNextScene("ChatScene");
        }
        catch (NullReferenceException ex)
        {
            Debug.Log("null");
        }
    }
    #endregion


    public void ShowMyProfile(string myID)
    {
        /*AreaScript Area = Instantiate(ListArea).GetComponent<AreaScript>();
        Area.transform.SetParent(MyRect.transform, false);
        Area.ListBoxRect.sizeDelta = new Vector2(600, Area.ListBoxRect.sizeDelta.y);  */


        var request = new GetUserDataRequest() { PlayFabId = myID };
        PlayFabClientAPI.GetUserData(request, (result) => {

            Name.text = result.Data["name"].Value;
            Role.text = result.Data["role"].Value;

        },
            (error) => print("데이터 불러오기 실패")
            );

        //Fit(Area.ListBoxRect);
        //Fit(MyRect);
    }

    public void List(Texture2D picture)
    {
        AreaScript Area = Instantiate(ListArea).GetComponent<AreaScript>();
        Area.transform.SetParent(ContentRect.transform, false);
        Area.ListBoxRect.sizeDelta = new Vector2(330, Area.ListBoxRect.sizeDelta.y);
        Area.ListTextRect.GetComponent<Text>().text = "??";
        Fit(Area.ListBoxRect);

        //Area.State = 


    }

   

    void GetLeaderboard(string myID)
    {
        PlayFabUserList.Clear();

        for (int i = 0; i < 10; i++)
        {
            var request = new GetLeaderboardRequest
            {
                StartPosition = i * 100,
                StatisticName = "IDInfo",
                MaxResultsCount = 100,
                ProfileConstraints = new PlayerProfileViewConstraints() { ShowDisplayName = true }
            };
            PlayFabClientAPI.GetLeaderboard(request, (result) =>
            {
                if (result.Leaderboard.Count == 0) return;
                for (int j = 0; j < result.Leaderboard.Count; j++)
                {
                    PlayFabUserList.Add(result.Leaderboard[j]);
                    if (result.Leaderboard[j].PlayFabId == myID) MyPlayFabInfo = result.Leaderboard[j];
                }
            },
            (error) => { });
        }
    }

    public void GetData(string myID)
    {
        var request = new GetUserDataRequest() { PlayFabId = myID };
        PlayFabClientAPI.GetUserData(request, (result) => {
            foreach (var eachData in result.Data) //LogText.text += eachData.Key + " : " + eachData.Value.Value + "\n"; 
                print(eachData.Key + ":" + eachData.Value.Value);
        },
            (error) => print("데이터 불러오기 실패")
            );
    }






    void ShowUserNickName()
    {
        //UserNickNameText.text = "";
        //for (int i = 0; i < PlayFabUserList.Count; i++) UserNickNameText.text += PlayFabUserList[i].DisplayName + "\n";
    }

    public void clean()
    {
        
        for (int i = 0; i < ContentRect.childCount; i++)
        {
            Destroy(ContentRect.GetChild(i).gameObject);
        }
    }

    void Fit(RectTransform Rect) => LayoutRebuilder.ForceRebuildLayoutImmediate(Rect);


/*    public void Load()
    {
        myID = PlayerPrefs.GetString("userId");

    }*/
}
