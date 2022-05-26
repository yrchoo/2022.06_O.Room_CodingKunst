using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using PlayFab;
using PlayFab.ClientModels;

public class SaveCustomizeButton : MonoBehaviour
{
    public ChangeScene CS;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /*public void saveBtn()
    {
        try
        {
            CS.LoadNextScene("MainScene");
        }
        catch (NullReferenceException ex)
        {
            Debug.Log("null");
        }
    }
    public void SetCustomData(int index)
    {
        PlayFabClientAPI.UpdatePlayerStatistics(new UpdatePlayerStatisticsRequest
        {
            Statistics = new List<StatisticUpdate>
            {
                new StatisticUpdate {StatisticName = "customize", Value = index},

            }
        },
        (result) => print("데이터 저장 성공"),
        (error) => print("데이터 저장 실패"));
    }*/

    /*public void LoadNextScene(){
      // 비동기적으로 Scene을 불러오기 위해 Coroutine을 사용한다.
      StartCoroutine(LoadMyAsyncScene());
    }

    IEnumerator LoadMyAsyncScene()
    {    
    // AsyncOperation을 통해 Scene Load 정도를 알 수 있다.
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("ChatScene");

        // Scene을 불러오는 것이 완료되면, AsyncOperation은 isDone 상태가 된다.
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }*/
}
