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


    public int customize = 0;
    public string userName = "";
    public string userRole = "";
    public string userTeam = "";


    void Awake()
    {
        /*SceneManager = GameObject.Find("PFuserID");
        Debug.Log(SceneManager.GetComponent<DonDestroyObject>().PFuserID);*/
        
        myID = CS.Load("userId");
        //CS.GetUserData(myID);

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
            Debug.Log("null");
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
    public void SetData(string myID)
    {
        var request = new GetUserDataRequest() { PlayFabId = myID };

        PlayFabClientAPI.GetUserData(request,
            (result) => {
                userName = result.Data["name"].Value;
                userRole = result.Data["role"].Value;
                userTeam = result.Data["team"].Value;
            },
            (error) => print("데이터 불러오기 실패")
        );


        /*PlayFabClientAPI.GetPlayerStatistics(
            new GetPlayerStatisticsRequest(),
            (result) =>
            {
                foreach (var eachStat in result.Statistics)
                {
                    switch (eachStat.StatisticName)
                    {
                        case "customize": customize = eachStat.Value; print(customize); break;

                    }
                }
            },
            (error) => { print("값 불러오기 실패"); }
         );

        CS.SaveInt(customize);*/

    }

    /*public void GetData(string myID)
    {
        var request = new GetUserDataRequest() { PlayFabId = myID };
        PlayFabClientAPI.GetUserData(request, (result) => {
            foreach (var eachData in result.Data) //LogText.text += eachData.Key + " : " + eachData.Value.Value + "\n"; 
                print(eachData.Key + ":" + eachData.Value.Value);
        },
            (error) => print("데이터 불러오기 실패")
            );
    }*/

    /*public void GetStat()
    {
        PlayFabClientAPI.GetPlayerStatistics(
            new GetPlayerStatisticsRequest(),
            (result) =>
            {
                foreach (var eachStat in result.Statistics)
                {
                    switch (eachStat.StatisticName)
                    {
                        case "customize": print(eachStat.Value); break;
                    }
                }
            },
            (error) => { print("값 불러오기 실패"); });  
    }*/


    /*public void SetCustomData()
    {
        PlayFabClientAPI.UpdatePlayerStatistics(new UpdatePlayerStatisticsRequest
        {
            Statistics = new List<StatisticUpdate>
            {
                new StatisticUpdate {StatisticName = "customize", Value = 2},

            }
        },
        (result) => print("데이터 저장 성공"),
        (error) => print("데이터 저장 실패"));
    }*/
    /*public void GetStat()
    {
        int id = 0;
        string customize = "";
        PlayFabClientAPI.GetPlayerStatistics(
            new GetPlayerStatisticsRequest(),
            (result) =>
            {
                //var eachStat = result.Statistics[1];
                //id = eachStat.Value;
                foreach (var eachStat in result.Statistics)
                {
                    customize = eachStat.StatisticName;
                    id = eachStat.Value;
                }
                Debug.Log(customize);
                Debug.Log(id);
            },
            (error) => { print("값 불러오기 실패"); });
        //return id;
    }*/

    /*public void GetStat()
    {
        PlayFabClientAPI.GetPlayerStatistics(
            new GetPlayerStatisticsRequest(),
            (result) =>
            {
                StatText.text = "";
                foreach (var eachStat in result.Statistics)
                    StatText.text += eachStat.StatisticName + " : " + eachStat.Value + "\n";
            },
            (error) => { StatText.text = "값 불러오기 실패"; });
    }
*/

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
