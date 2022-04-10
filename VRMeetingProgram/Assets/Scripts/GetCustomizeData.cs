using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetCustomizeData : MonoBehaviour
{
    public int skinData = 0;
    public int genderData = 0;
    public int modelData = 0;
    public int oldSkinData = 0;
    public int oldGenderData = 0;
    public int oldModelData = 0;
    public GameObject[] character = new GameObject[32];

    struct characterData{
        int gender;
        int model;        
        int skin;
    };


    // Start is called before the first frame update
    void Start()
    {
        // firebase에 저장된 캐릭터 커스터마이징 데이터 값을 읽어서 변수에 저장하도록 함
        character[16*genderData + 4*modelData + skinData].SetActive(true);
        oldModelData = modelData;
        oldGenderData = genderData;
        oldSkinData = skinData;
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
        }
    }
}