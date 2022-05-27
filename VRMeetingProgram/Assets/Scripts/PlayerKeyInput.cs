using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerKeyInput : MonoBehaviour
{
    public InputField ChatInput;

    // Start is called before the first frame update
    void Start()
    {
        ChatInput = GameObject.Find("ChatInput").GetComponent<InputField>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.T)){
                GameObject myPlayer = GameObject.Find("GameManager").GetComponent<GameManager>().myPlayer;
                
                ChatInput.ActivateInputField();
                ChatInput.Select();
                
                myPlayer.GetComponent<Strafer>().enabled = false;
                myPlayer.GetComponent<Croucher>().enabled = false;
                myPlayer.GetComponent<Emoji>().enabled = false;
                myPlayer.GetComponent<DirectAimController>().enabled = false;
                GameObject.Find("PauseRoom").GetComponent<PauseRoom>().cam.GetComponent<CameraMovement>().enabled = false;

                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
        }
    }
}
