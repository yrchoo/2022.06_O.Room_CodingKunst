using UnityEngine.SceneManagement;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using Photon.Pun;
using Photon.Realtime;
//using Hashtable = ExitGames.Client.Photon.Hashtable;
using UnityEngine.UI;

public class PlayFabManager : MonoBehaviourPunCallbacks
{
    //public DonDestroyObject ddo;

    public InputField EInput, PWInput, NInput;

    public InputField EmailInput, PasswordInput, UsernameInput, IdInput, RoleInput, TeamInput;
    public string myID;
    public GameObject AccountPanel;

    public ChangeScene CS;

    void Awake()
    {
        Screen.SetResolution(960, 540, false);
        PhotonNetwork.SendRate = 60;
        PhotonNetwork.SerializationRate = 30;
    }

    public void LoginBtn()
    {
        var request = new LoginWithEmailAddressRequest { Email = EInput.text, Password = PWInput.text };       
        PlayFabClientAPI.LoginWithEmailAddress(request, OnLoginSuccess, OnLoginFailure);
    }

    public void RegisterBtn()
    {
        var request = new RegisterPlayFabUserRequest { Email = EmailInput.text, Password = PasswordInput.text, Username = IdInput.text, DisplayName = IdInput.text };
        PlayFabClientAPI.RegisterPlayFabUser(request, OnRegisterSuccess, OnRegisterFailure);
       
    }

    void OnLoginSuccess(LoginResult result) {
        print("로그인 성공");
        myID = result.PlayFabId;
        CS.SaveStr(myID);
        CS.playfabId = myID;
        //CS.call();
        int customize =1;
        PlayFabClientAPI.GetPlayerStatistics(
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

        CS.SaveInt(customize);

        //PhotonNetwork.ConnectUsingSettings();
        try
        {
           CS.LoadNextScene("MainScene");
          // LoadingSceneController.LoadScene("MainScene");
        }
        catch (NullReferenceException ex)
        {
            Debug.Log(ex+"null");
        }       
    }
    void OnLoginFailure(PlayFabError error) => print("로그인 실패");

    void OnRegisterSuccess(RegisterPlayFabUserResult result)
    {
        print("회원가입 성공");
        SetStat();
        SetData(UsernameInput.text, IdInput.text, RoleInput.text, TeamInput.text);
        //SetCustomData();
        AccountPanel.SetActive(false);
    }
    void OnRegisterFailure(PlayFabError error) => print("회원가입 실패");

    void SetStat()
    {
        var request = new UpdatePlayerStatisticsRequest { 
            Statistics = new List<StatisticUpdate> { 
            new StatisticUpdate { StatisticName = "IDInfo", Value = 0 },
            new StatisticUpdate {StatisticName = "customize", Value = 1},
            } 
        };
        PlayFabClientAPI.UpdatePlayerStatistics(request, (result) => print("값 저장성공"), (error) => print("값 저장실패"));
    }

    /*public void Save(string id)
    {
        PlayerPrefs.SetString("userId", id);
    }*/

    public void SetData(string name, string id, string role, string team)
    {
        var request = new UpdateUserDataRequest() { 
            Data = new Dictionary<string, string>() { 
                { "name", name }, { "id", id }, { "role", role } ,{ "team",team },
            },
            Permission = UserDataPermission.Public

        };
        PlayFabClientAPI.UpdateUserData(request, (result) => print("데이터 저장 성공"), (error) => print("데이터 저장 실패"));
    }

    /*public void SetCustomData()
    {
        PlayFabClientAPI.UpdatePlayerStatistics(new UpdatePlayerStatisticsRequest
        {
            Statistics = new List<StatisticUpdate>
            {
                new StatisticUpdate {StatisticName = "customize", Value = 1},
                
            }
        },
        (result) => print("데이터 저장 성공"), 
        (error) => print("데이터 저장 실패"));
    }
*/


}
