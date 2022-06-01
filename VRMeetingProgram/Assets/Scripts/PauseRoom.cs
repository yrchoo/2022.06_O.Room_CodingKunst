using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseRoom : MonoBehaviour
{

    public GameObject cam;

    public GameObject NetworkManager;

    public GameObject pausePanel;

    public GameObject LobbyPanel;

    public GameObject Chat;

    public GameObject bg;

    GameObject myPlayer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {

            myPlayer = GameObject.Find("GameManager").GetComponent<GameManager>().myPlayer;
            //cam.SetActive(false);
            cam.GetComponent<CameraMovement>().enabled = false;
            myPlayer.GetComponent<Strafer>().enabled = false;
            myPlayer.GetComponent<Croucher>().enabled = false;
            myPlayer.GetComponent<Emoji>().enabled = false;
            myPlayer.GetComponent<DirectAimController>().enabled = false;

            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            pausePanel.SetActive(true);
        }
    }

    public void LeaveRoom(){
        Debug.Log("LeaveRoomBtn Pressed");
        pausePanel.SetActive(false);
        Destroy(myPlayer);
        cam.SetActive(false);
        Chat.SetActive(false);

        NetworkManager.GetComponent<NetworkManager>().XBtn();

        LobbyPanel.SetActive(true);
        bg.SetActive(true);
        //UnityEngine.SceneManagement.SceneManager.LoadScene (gameObject.scene.name);
    }

    public void BackToRoom(){
        Debug.Log("BackToRoomBtn Pressed");

        pausePanel.SetActive(false);
        //cam.SetActive(true);
        cam.GetComponent<CameraMovement>().enabled = true;
        myPlayer.GetComponent<Strafer>().enabled = true;
        myPlayer.GetComponent<Croucher>().enabled = true;
        myPlayer.GetComponent<Emoji>().enabled = true;
        myPlayer.GetComponent<DirectAimController>().enabled = true;
        Cursor.lockState = CursorLockMode.Locked; // 마우스
        Cursor.visible = false;
        
        
        
    }
}
