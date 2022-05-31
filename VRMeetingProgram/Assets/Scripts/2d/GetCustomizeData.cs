using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine.UI;

public class GetCustomizeData : MonoBehaviour
{
    public ChangeScene CS;

    public int modelData;
    
    public int genderData;

    public int skinData;
    
    public int oldGenderData;
    public int oldModelData;
    public int oldSkinData;

    public InputField nameInput, teamInput, roleInput;

    public GameObject[] character = new GameObject[32];

    public int getCustom;
    public int customize;

    void Awake(){
        getCustom = PlayerPrefs.GetInt("userCustom");

        Debug.Log("customize" + getCustom);
        customize = getCustom;
        getCustom = getCustom - 1;

        genderData = getCustom / 16;
        getCustom = getCustom % 16;

        modelData = getCustom / 4;
        getCustom = getCustom % 4;

        skinData = getCustom;

        // firebase에 저장된 캐릭터 커스터마이징 데이터 값을 읽어서 변수에 저장하도록 함
        oldModelData = modelData;
        oldGenderData = genderData;
        oldSkinData = skinData;

        nameInput.text = PlayerPrefs.GetString("userName");
        teamInput.text = PlayerPrefs.GetString("userTeam");
        roleInput.text = PlayerPrefs.GetString("userRole");
        
    }

    // Start is called before the first frame update
    void Start()
    {
        //getCustom = GetCustomVal();
        //customize = CS.getCustomize();
        //Debug.Log("???"+ customize);
        //Debug.Log("oldGenderData : "+ oldGenderData + "  oldModelData : " + oldModelData + "  oldSkinData : " + oldSkinData);
        //Debug.Log("GenderData : "+ genderData + "  ModelData : " + modelData + "  SkinData : " + skinData);
        character[customize - 1].SetActive(true);
        
    }

    // Update is called once per frame
    void Update()
    {
        // 데이터가 하나라도 변경되면 출력되는 모델을 바꿈
        if(oldModelData != modelData || oldGenderData != genderData || oldSkinData != skinData){
            int deactiveNum = 16*oldGenderData+4*oldModelData+oldSkinData;
            //Debug.Log("Deactive"+deactiveNum);
            //Debug.Log("oldGenderData : "+ oldGenderData + "  oldModelData : " + oldModelData + "  oldSkinData : " + oldSkinData);
            int activeNum = (16*genderData)+(4*modelData)+skinData;
            //Debug.Log("Active"+activeNum);
            Debug.Log("GenderData : "+ genderData + "  ModelData : " + modelData + "  SkinData : " + skinData);
            
            

            character[deactiveNum].SetActive(false);
            character[activeNum].SetActive(true);

            // 데이터 갱신
            oldModelData = modelData;
            oldGenderData = genderData;
            oldSkinData = skinData;

            customize = activeNum + 1;
            
        }
    }

    public void saveBtn()
    {
        SetCustomData(customize);
        SetChangedData(nameInput.text, roleInput.text, teamInput.text);
        /*PlayerPrefs.SetString("userName", nameInput.text);    
        PlayerPrefs.SetString("userRole", roleInput.text);        
        PlayerPrefs.SetString("userTeam", teamInput.text);  */
        
        //CS.LoadNextScene("ChatScene");
        CS.LoadNextScene("MainScene");
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
        (result) => { print("데이터 저장 성공" + index); PlayerPrefs.SetInt("userCustom", index); },
        (error) => print("데이터 저장 실패"));
    }

    public void SetChangedData(string name, string role, string team){

        var request = new UpdateUserDataRequest()
        {
            Data = new Dictionary<string, string>() {
                { "name", name }, { "role", role } ,{ "team",team },
            },
            Permission = UserDataPermission.Public

        };
        PlayFabClientAPI.UpdateUserData(request, (result) => print("데이터 저장 성공"), (error) => print("데이터 저장 실패"));
    }
}