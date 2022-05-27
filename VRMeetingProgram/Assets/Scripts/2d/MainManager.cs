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

    public Text Team, Name, Role;
    public Image Image;

    //bool isLoaded;
    
    [Header("Disconnect")]
    public PlayerLeaderboardEntry MyPlayFabInfo; //내 정보 다 들어감
    public List<PlayerLeaderboardEntry> PlayFabUserList = new List<PlayerLeaderboardEntry>();

    public int customize = 0;
    public string userName = "";
    public string userRole = "";
    public string userTeam = "";

    void Awake()
    {
        /*SceneManager = GameObject.Find("PFuserID");
        Debug.Log(SceneManager.GetComponent<DonDestroyObject>().PFuserID);*/

        //myID = CS.Load("userId");
        //CS.GetUserData(myID);

        myID = PlayerPrefs.GetString("userId");
        GetLeaderboard(myID);
        ShowMyProfile(myID);
        
        //GetStat();
        //Debug.Log(id);
    }

    // Update is called once per frame
    void Update()
    {
        //Load();
    }

    
    #region 버튼 클릭
    /*
    public void MemberClick()
    {
        MemberPanel.SetActive(true);
    }
   
    public void ChatClick()
    {
        //MemberPanel.SetActive(false);
        try
        {
            CS.LoadNextScene("ChatScene");
        }
        catch (NullReferenceException ex)
        {
            Debug.Log(ex+"null");
        }
    }
   
    public void SettingClick()
    {
        //MemberPanel.SetActive(false);
        try
        {
            CS.LoadNextScene("CustomizeScene");
        }
        catch (NullReferenceException ex)
        {
            Debug.Log(ex+"null");
        }
    }
    */
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
            Team.text = result.Data["team"].Value+"팀";

        },
            (error) => print("데이터 불러오기 실패")
            );

        //Fit(Area.ListBoxRect);
        //Fit(MyRect);
    }

    /*public void List(Texture2D picture)
    {
        AreaScript Area = Instantiate(ListArea).GetComponent<AreaScript>();
        Area.transform.SetParent(ContentRect.transform, false);
        Area.ListBoxRect.sizeDelta = new Vector2(330, Area.ListBoxRect.sizeDelta.y);
        Area.ListTextRect.GetComponent<Text>().text = "??";
        Fit(Area.ListBoxRect);

    } */

    

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
                //////////////
                ShowOtherUserNickName();
            },
            (error) => { });
        }
    }

    void ShowOtherUserNickName()
    {
        //Debug.Log("count"+PlayFabUserList.Count);
        for (int i = 0; i < PlayFabUserList.Count; i++)
        {
            //Debug.Log(PlayFabUserList[i].DisplayName);
            //UserNickNameText.text += PlayFabUserList[i].DisplayName + "\n";
            string otherId = PlayFabUserList[i].PlayFabId;
            
            //Debug.Log("here??"+otherId);
            SetData(otherId);

        }
    }

    public void SetData(string myID)
    {
        userName = "";
        userRole = "";
        userTeam = "";
        var request = new GetUserDataRequest() { PlayFabId = myID };

        PlayFabClientAPI.GetUserData(request,
            (result) => {
                userName = result.Data["name"].Value;
                userRole = result.Data["role"].Value;
                userTeam = result.Data["team"].Value;
                List(userTeam, userRole, userName);
            },
            (error) => print("데이터 불러오기 실패")
        );
        
    }

    public void List(string team, string role, string name)
    {
        Transform CurListArea = Instantiate(ListArea).transform;
        CurListArea.SetParent(ContentRect.transform, false);
        CurListArea.SetSiblingIndex(CurListArea.GetSiblingIndex());
        CurListArea.GetComponent<AreaScript>().TeamText.text = team;
        CurListArea.GetComponent<AreaScript>().RoleText.text = role;
        CurListArea.GetComponent<AreaScript>().NameText.text = name;
    }

   
    public void clean()
    {
        
        for (int i = 0; i < ContentRect.childCount; i++)
        {
            Destroy(ContentRect.GetChild(i).gameObject);
        }
    }

    void Fit(RectTransform Rect) => LayoutRebuilder.ForceRebuildLayoutImmediate(Rect);
}
