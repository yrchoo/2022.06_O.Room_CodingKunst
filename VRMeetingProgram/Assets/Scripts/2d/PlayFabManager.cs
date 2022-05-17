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
        CS.Save(myID);

        //PhotonNetwork.ConnectUsingSettings();
        try
        {
            CS.LoadNextScene("MainScene");
        }
        catch (NullReferenceException ex)
        {
            Debug.Log("null");
        }
        

    }
    void OnLoginFailure(PlayFabError error) => print("로그인 실패");

    void OnRegisterSuccess(RegisterPlayFabUserResult result)
    {
        print("회원가입 성공");
        SetStat();
        SetData(UsernameInput.text, IdInput.text, RoleInput.text, TeamInput.text);
        AccountPanel.SetActive(false);
    }
    void OnRegisterFailure(PlayFabError error) => print("회원가입 실패");

    void SetStat()
    {
        var request = new UpdatePlayerStatisticsRequest { Statistics = new List<StatisticUpdate> { new StatisticUpdate { StatisticName = "IDInfo", Value = 0 } } };
        PlayFabClientAPI.UpdatePlayerStatistics(request, (result) => { }, (error) => print("값 저장실패"));
    }

    /*public void Save(string id)
    {
        PlayerPrefs.SetString("userId", id);
    }*/

    public void SetData(string name, string id, string role, string team)
    {
        var request = new UpdateUserDataRequest() { 
            Data = new Dictionary<string, string>() { 
                { "name", name }, { "id", id }, { "role", role } ,{"team",team}
            },
            Permission = UserDataPermission.Public

        };
        PlayFabClientAPI.UpdateUserData(request, (result) => print("데이터 저장 성공"), (error) => print("데이터 저장 실패"));
    }

    //public override void OnConnectedToMaster() => PhotonNetwork.JoinLobby();

    /*public void LoadNextScene()
    {
        
        // 비동기적으로 Scene을 불러오기 위해 Coroutine을 사용한다.
        StartCoroutine(LoadMyAsyncScene());
    }*/

    /*IEnumerator LoadMyAsyncScene()
    {
        // AsyncOperation을 통해 Scene Load 정도를 알 수 있다.
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("MainScene");

        // Scene을 불러오는 것이 완료되면, AsyncOperation은 isDone 상태가 된다.
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }*/


}
