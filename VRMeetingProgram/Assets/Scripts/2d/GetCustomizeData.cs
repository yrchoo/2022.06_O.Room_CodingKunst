using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;

public class GetCustomizeData : MonoBehaviour
{
    public ChangeScene CS;

    public int modelData;
    
    public int genderData;

    public int skinData;
    
    public int oldGenderData;
    public int oldModelData;
    public int oldSkinData;


    public GameObject[] character = new GameObject[32];

    public int getCustom;
    public int customize;

    // Start is called before the first frame update
    void Start()
    {
        //getCustom = GetCustomVal();
        //customize = CS.getCustomize();
        //Debug.Log("???"+ customize);
        getCustom = PlayerPrefs.GetInt("userChar");
        
        genderData = getCustom % 16;
        getCustom = getCustom - (16 * genderData);
        modelData = getCustom % 4;
        getCustom = getCustom - (4 * modelData);
        skinData = getCustom;

        // firebase에 저장된 캐릭터 커스터마이징 데이터 값을 읽어서 변수에 저장하도록 함
        oldModelData = modelData;
        oldGenderData = genderData;
        oldSkinData = skinData;
        character[16*genderData + 4*modelData + skinData].SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        // 데이터가 하나라도 변경되면 출력되는 모델을 바꿈
        if(oldModelData != modelData || oldGenderData != genderData || oldSkinData != skinData){
            character[16*oldGenderData+4*oldModelData+oldSkinData].SetActive(false);
            character[16*genderData+4*modelData+skinData].SetActive(true);
            // 데이터 갱신
            oldModelData = modelData;
            oldGenderData = genderData;
            oldSkinData = skinData;

            customize = 16 * genderData + 4 * modelData + skinData;
            
        }
    }

    public void saveBtn()
    {
        SetCustomData(customize);
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
        (result) => print("데이터 저장 성공"),
        (error) => print("데이터 저장 실패"));
    }

    /*public int GetCustomVal()
    {
        int cVal = 0;
        PlayFabClientAPI.GetPlayerStatistics(
            new GetPlayerStatisticsRequest(),
            (result) =>
            {
                foreach (var eachStat in result.Statistics)
                {
                    switch (eachStat.StatisticName)
                    {
                        case "customize": cVal = eachStat.Value;  print("customize"+eachStat.Value); break;
                        case "IDInfo": print("idinfo"+eachStat.Value); break;
                    }
                }
            },
            (error) => { print("값 불러오기 실패"); }
         );

        return cVal;
    }*/

    

}