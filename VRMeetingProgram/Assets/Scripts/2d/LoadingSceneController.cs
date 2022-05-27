using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingSceneController : MonoBehaviour
{
    static string nextScene;
    public GameObject LoadingPanel;

    [SerializeField]
    Image progressBar;

    public float timer = 0f;

    void Awake()
    {
        LoadingPanel.SetActive(true);
    }
    void Start()
    {
        
        Debug.Log("LoadingScene");
        while (true)
        {
            
            timer += Time.unscaledDeltaTime;
            progressBar.fillAmount = Mathf.Lerp(0.9f, 1f, timer);
            if (progressBar.fillAmount >= 3f)
            {
                LoadingPanel.SetActive(false);
                break;

            }
        }
        
    }

    void Update()
    {
       

    }


}