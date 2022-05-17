using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine.UI;

public class ChangeScene : MonoBehaviour
{
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

    #region 저장
    public void Save(string id)
    {
        PlayerPrefs.SetString("userId", id);
    }
    public string Load(string key)
    {
        string myID = PlayerPrefs.GetString(key);
        return myID;
    }
    #endregion
}
