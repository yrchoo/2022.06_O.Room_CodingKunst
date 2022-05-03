using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine.UI;

public class PlayFabManager : MonoBehaviour
{
    public InputField EmailInput, PasswordInput, UsernameInput;

    public InputField EInput, PWInput, NInput, IdInput, RoleInput;
    public string myID;
    public GameObject AccountPanel;
    public void LoginBtn()
    {
        var request = new LoginWithEmailAddressRequest { Email = EmailInput.text, Password = PasswordInput.text };       
        PlayFabClientAPI.LoginWithEmailAddress(request, OnLoginSuccess, OnLoginFailure);     
    }


    public void RegisterBtn()
    {
        var request = new RegisterPlayFabUserRequest { Email = EInput.text, Password = PWInput.text, Username = IdInput.text };
        PlayFabClientAPI.RegisterPlayFabUser(request, OnRegisterSuccess, OnRegisterFailure);
       
    }

    void OnLoginSuccess(LoginResult result) {
        print("로그인 성공");
        myID = result.PlayFabId;
    }

    void OnLoginFailure(PlayFabError error) => print("占싸깍옙占쏙옙 占쏙옙占쏙옙");


    void OnRegisterSuccess(RegisterPlayFabUserResult result)
    {
        print("회원가입 성공");
        SetData(NInput.text, IdInput.text, RoleInput.text);
        AccountPanel.SetActive(false);
    }

    void OnRegisterFailure(PlayFabError error) => print("회占쏙옙占쏙옙占쏙옙 占쏙옙占쏙옙");

    public void SetData(string name, string id, string role)
    {
        var request = new UpdateUserDataRequest() { Data = new Dictionary<string, string>() { { "name", name }, { "id", id }, { "role", role } } };
        PlayFabClientAPI.UpdateUserData(request, (result) => print("데이터 저장 성공"), (error) => print("데이터 저장 실패"));
    }

    public void GetData()
    {
        var request = new GetUserDataRequest() { PlayFabId = myID };
        PlayFabClientAPI.GetUserData(request, (result) => {
            foreach (var eachData in result.Data) //LogText.text += eachData.Key + " : " + eachData.Value.Value + "\n"; 
                print(eachData.Key + ":" + eachData.Value.Value);
        }, 
            (error) => print("데이터 불러오기 실패")
            );
    }

}
