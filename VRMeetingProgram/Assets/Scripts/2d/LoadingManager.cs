using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingManager : MonoBehaviour
{

    //public GameObject LoadingPanel;

    [SerializeField]
    Image progressBar;
    
    public float timer;


    // Start is called before the first frame update
    void Start()
    {
        timer = 0f;
    }

    // Update is called once per frame
    void Update()
    {
            timer += Time.unscaledDeltaTime;
            progressBar.fillAmount = Mathf.Lerp(0.1f, 1.0f, timer / 1.75f);
            if (progressBar.fillAmount >= 1.0f)
            {
                gameObject.SetActive(false);
            }
    }
}
