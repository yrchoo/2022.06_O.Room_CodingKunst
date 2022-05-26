using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine.UI;

public class ChangeScene : MonoBehaviour
{
    public string playfabId;
    public GameObject userObject;

    /*    public void call()
        {
            DontDestroyOnLoad(userObject);
        }
    */

    public int customize =0;
    public string userName ="";
    public string userRole="";
    public string userTeam="";

    #region 저장
    public void SaveStr(string val)
    {
        PlayerPrefs.SetString("userId", val);
        
        //PlayerPrefs.SetString
        SetData(val);       
    }
    public void SaveInt(int val)
    {
        PlayerPrefs.SetInt("userChar", val);
    }
    
    public string Load(string key)
    {
        string myID = PlayerPrefs.GetString(key);
        return myID;
    }
    #endregion

    public int SetData(string myID)
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

        return 0;
       
    }
    
    public int getCustomize()
    {
        return customize;
    }
    public string getName()
    {
        return userName;
    }
    public string getRole()
    {
        return userRole;
    }
    public string getTeam()
    {
        return userTeam;
    }

    #region 씬 전환
    public void LoadNextScene(string scene)
    {

        // 비동기적으로 Scene을 불러오기 위해 Coroutine을 사용한다.
        StartCoroutine(LoadMyAsyncScene(scene));
    }

    IEnumerator LoadMyAsyncScene(string scene)
    {
        // AsyncOperation을 통해 Scene Load 정도를 알 수 있다.
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(scene);

        // Scene을 불러오는 것이 완료되면, AsyncOperation은 isDone 상태가 된다.
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
    #endregion

   
}
