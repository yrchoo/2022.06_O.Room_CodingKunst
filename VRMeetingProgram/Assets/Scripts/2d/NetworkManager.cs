using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
public class NetworkManager : MonoBehaviourPunCallbacks
{
    private readonly string gameVersion = "v1.0";

    public GameObject Conference;
    public GameObject School;
    public GameObject Office;

    public ChatManager CM;
    public ChangeScene CS;

    static public string myID;
    public string UserName;
    public string ID;

    [Header("SideBar")]
    public GameObject SideBar;
    public InputField NickNameInput;

    [Header("Lobby")]
    public GameObject LobbyPanel;     
    public Dropdown RoomSortDropdown;
    public Button PreviousBtn;
    public Button NextBtn;
    List<RoomInfo> myList = new List<RoomInfo>();
    List<RoomInfo> CurRoomList = new List<RoomInfo>();
    public Button[] CellBtn;
    int currentPage = 1, maxPage, multiple;

    [Header("SecretPanel")]
    public GameObject SecretPanel;
    public InputField PWInput;
    public Text CurRoomNameText;

    [Header("CreatePanel")]
    public InputField RoomInput;
    public InputField RoomNum;
    public Toggle SecretToggle;
    public InputField SecretInput;
    public Dropdown RoomKindDropdown;

    [Header("Chat")]
    public GameObject Chat;
    public Text ListText;
    public Text RoomInfoText;
    public Text CountInfoText;
    public Text MaxInfoText;
    public InputField ChatInput;

    [Header("ETC")]
    public PhotonView PV;
    public GameObject cam;

    [Header("Disconnect")]
    public PlayerLeaderboardEntry MyPlayFabInfo; 
    public List<PlayerLeaderboardEntry> PlayFabUserList = new List<PlayerLeaderboardEntry>();

    [Header("LoadingPanel")]
    public GameObject LoadingPanel;


    #region 서버연결
    void Awake()
    {
        LoadingPanel.SetActive(true);
        //myID = CS.Load("userId");
        myID = PlayerPrefs.GetString("userId");
        GetMyName(myID);
        PhotonNetwork.GameVersion = gameVersion;

        PhotonNetwork.ConnectUsingSettings();       
    }   

    public override void OnConnectedToMaster() => PhotonNetwork.JoinLobby(); //connect�� �ݹ�

    public override void OnJoinedLobby()
    {       
        Debug.Log("OnJoinedLobby" + UserName);
        
        PhotonNetwork.LocalPlayer.NickName = UserName;
        //WelcomeText.text = PhotonNetwork.LocalPlayer.NickName + "님";

        myList.Clear();
        ShowPanel(LobbyPanel);
           
    }   

    public override void OnDisconnected(DisconnectCause cause) //disconnect�ݹ�
    {        
        //CS.MemberClick();
    }
    #endregion

    //content추가
    //public void List(string room, string num)
    //{
    //    Transform CurListArea = Instantiate(RoomArea).transform;
    //    CurListArea.SetParent(ContentRect.transform, false);
    //    CurListArea.SetSiblingIndex(CurListArea.GetSiblingIndex());
    //    CurListArea.GetComponent<AreaScript>().TeamText.text = team;
    //    CurListArea.GetComponent<AreaScript>().RoleText.text = role;
    //    CurListArea.GetComponent<AreaScript>().NameText.text = name;
    //}

    #region 방리스트 갱신
    public void MyListClick(int num)
    {
        string roomNum = myList[num].Name.Substring(myList[num].Name.IndexOf("@R@") + 3,1);
        if (roomNum == "0")
        {
            ChangeRoom(Conference);
        }
        else if(roomNum == "1")
        {
            ChangeRoom(School);
        }
        else if (roomNum == "2")
        {
            ChangeRoom(Office);
        }

        if (myList[num].Name.Contains("@PW@"))
        {
            SecretPanel.SetActive(true);
            PWInput.text = "";
            CurRoomNameText.text = myList[num].Name;           
        }
        else PhotonNetwork.JoinRoom(myList[num].Name); //onjoinedroom
        MyListRenewal();
    }
    public void SecretConfirmBtn()
    {
        SecretPanel.SetActive(false);
        string roomName = CurRoomNameText.text;        
        string pw = roomName.Substring(CurRoomNameText.text.IndexOf("@PW@") + 4);      
        
        if(PWInput.text == pw) PhotonNetwork.JoinRoom(CurRoomNameText.text);
    }
    //original
    /*void MyListRenewal()
    {
        //original coding
        
        *//*for (int i = 0; i < CellBtn.Length; i++)
        {
            CellBtn[i].interactable = (i < myList.Count);
            if (i >= myList.Count) { CellBtn[i].gameObject.SetActive(false); }
            else { CellBtn[i].gameObject.SetActive(true); }

            Debug.Log("myList[i].Name" + myList[i].Name+ myList[i].PlayerCount+ myList[i].MaxPlayers);
            CellBtn[i].transform.GetChild(0).GetComponent<Text>().text = (i < myList.Count) ? myList[i].Name : "";
            CellBtn[i].transform.GetChild(1).GetComponent<Text>().text = (i < myList.Count) ? myList[i].PlayerCount + "/" + myList[i].MaxPlayers : "";

            //CellBtn[i].transform.GetChild(2).gameObject.SetActive(i< myList.Count && myList[i].Name.Contains("@PW@")
            *//*if (myList[i].Name.Contains("@PW@"))
            {
                CellBtn[i].transform.GetChild(0).GetComponent<Text>().text = myList[i].Name.Substring(0, myList[i].Name.IndexOf("@PW@"));
            }
            else { CellBtn[i].transform.GetChild(0).GetComponent<Text>().text = (i < myList.Count) ? myList[i].Name : ""; }
            CellBtn[i].transform.GetChild(1).GetComponent<Text>().text = (i < CurRoomList.Count) ? CurRoomList[i].PlayerCount + "/" + CurRoomList[i].MaxPlayers : "";*//*
        }*//*
        /////////////////////////   
        ///
        for (int i = 0; i < CellBtn.Length; i++)
        {
            CellBtn[i].interactable = (i < myList.Count);
            
            if (i >= myList.Count) { CellBtn[i].gameObject.SetActive(false); }
            else { CellBtn[i].gameObject.SetActive(true); }

            //string roomName = myList[i].Name;
            if (i < myList.Count && myList[i].Name.Contains("@PW@"))
            {
                CellBtn[i].transform.GetChild(0).GetComponent<Text>().text = (i < myList.Count) ? myList[i].Name.Substring(0, myList[i].Name.IndexOf("@PW@")) : "";
                CellBtn[i].transform.GetChild(2).gameObject.SetActive(true);
            }
            else { 
                CellBtn[i].transform.GetChild(0).GetComponent<Text>().text = (i < myList.Count) ? myList[i].Name : "";
                CellBtn[i].transform.GetChild(2).gameObject.SetActive(false);
            }
                               
            CellBtn[i].transform.GetChild(1).GetComponent<Text>().text = (i < myList.Count) ? myList[i].PlayerCount + "/" + myList[i].MaxPlayers : "";
            *//*CellBtn[i].transform.GetChild(2).gameObject.SetActive(i < myList.Count && myList[i].Name.Contains("@PW@"));*//*
        }

    }*/

    public void MyListRenewal()
    {
        CurRoomList = new List<RoomInfo>();

        //Debug.Log("dropdown" + RoomSortDropdown.value);
        //전체
        if (RoomSortDropdown.value == 0) CurRoomList = myList;
        //공개
        else if (RoomSortDropdown.value == 1)
        {
            for (int i = 0; i < myList.Count; i++)
            {
                if (!myList[i].Name.Contains("@PW@")) CurRoomList.Add(myList[i]);
            }
        }
        //비밀
        else if (RoomSortDropdown.value == 2)
        {
            for (int i = 0; i < myList.Count; i++)
            {
                if (myList[i].Name.Contains("@PW@")) CurRoomList.Add(myList[i]);
            }
        }

        // 최대페이지
        maxPage = (CurRoomList.Count % CellBtn.Length == 0) ? CurRoomList.Count / CellBtn.Length : CurRoomList.Count / CellBtn.Length + 1;

        // 이전, 다음버튼
        PreviousBtn.interactable = (currentPage <= 1) ? false : true;
        NextBtn.interactable = (currentPage >= maxPage) ? false : true;

        // 페이지에 맞는 리스트 대입
        multiple = (currentPage - 1) * CellBtn.Length;
        for (int i = 0; i < CellBtn.Length; i++)
        {           
            CellBtn[i].interactable = (multiple + i < CurRoomList.Count) ? true : false;

            if (i >= CurRoomList.Count) { CellBtn[i].gameObject.SetActive(false); }
            else { CellBtn[i].gameObject.SetActive(true); }

            /*if (i < CurRoomList.Count && CurRoomList[i].Name.Contains("@PW@"))
            {
                CellBtn[i].transform.GetChild(0).GetComponent<Text>().text = (multiple+i < CurRoomList.Count) ? CurRoomList[multiple+i].Name.Substring(0, CurRoomList[multiple+i].Name.IndexOf("@R@")) : "";
                CellBtn[i].transform.GetChild(2).gameObject.SetActive(true);
            }
            else
            {
                CellBtn[i].transform.GetChild(0).GetComponent<Text>().text = (multiple + i < CurRoomList.Count) ? CurRoomList[multiple + i].Name.Substring(0, CurRoomList[multiple + i].Name.IndexOf("@R@")) : "";
                CellBtn[i].transform.GetChild(2).gameObject.SetActive(false);
            }*/

            CellBtn[i].transform.GetChild(0).GetComponent<Text>().text = (multiple + i < CurRoomList.Count) ? CurRoomList[multiple + i].Name.Substring(0, CurRoomList[multiple + i].Name.IndexOf("@R@")) : "";           
            CellBtn[i].transform.GetChild(1).GetComponent<Text>().text = (multiple + i < CurRoomList.Count) ? CurRoomList[multiple + i].PlayerCount + "/" + CurRoomList[multiple + i].MaxPlayers : "";
            CellBtn[i].transform.GetChild(2).gameObject.SetActive(i < CurRoomList.Count && CurRoomList[i].Name.Contains("@PW@"));
        }


/*
        for (int i = 0; i < CellBtn.Length; i++)
        {
            CellBtn[i].interactable = (i < CurRoomList.Count);

            if (i >= CurRoomList.Count) { CellBtn[i].gameObject.SetActive(false); }
            else { CellBtn[i].gameObject.SetActive(true); }

            if (i < CurRoomList.Count && CurRoomList[i].Name.Contains("@PW@"))
            {
                CellBtn[i].transform.GetChild(0).GetComponent<Text>().text = (i < CurRoomList.Count) ? CurRoomList[i].Name.Substring(0, CurRoomList[i].Name.IndexOf("@PW@")) : "";
                CellBtn[i].transform.GetChild(2).gameObject.SetActive(true);
            }
            else
            {
                CellBtn[i].transform.GetChild(0).GetComponent<Text>().text = (i < CurRoomList.Count) ? CurRoomList[i].Name : "";
                CellBtn[i].transform.GetChild(2).gameObject.SetActive(false);
            }

            CellBtn[i].transform.GetChild(1).GetComponent<Text>().text = (i < CurRoomList.Count) ? CurRoomList[i].PlayerCount + "/" + CurRoomList[i].MaxPlayers : "";
            *//*CellBtn[i].transform.GetChild(2).gameObject.SetActive(i < myList.Count && myList[i].Name.Contains("@PW@"));*//*
        }*/


    }

    /*
     -로비에 접속 시
    -새로운 룸이 만들어질 경우
    -룸이 삭제되는 경우
    -룸의 IsOpen 값이 변화할 경우(아예 RoomInfo 내 데이터가 바뀌는 경우 전체일 수도 있습니다)
     */
    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        int roomCount = roomList.Count;
        //Debug.Log("roomCount"+roomCount);
        for (int i = 0; i < roomCount; i++)
        {
            if (!roomList[i].RemovedFromList)
            {
                if (!myList.Contains(roomList[i])) myList.Add(roomList[i]);
                else myList[myList.IndexOf(roomList[i])] = roomList[i];
            }
            else if (myList.IndexOf(roomList[i]) != -1) myList.RemoveAt(myList.IndexOf(roomList[i]));
        }
        MyListRenewal();
    }
    #endregion


    #region 방
    public void CreateConfirmBtn()
    {
        //Debug.Log("??" + RoomKindDropdown.value);
        //1
        int kind = 0;
        kind = RoomKindDropdown.value;
        if (kind == 0)
        {
            ChangeRoom(Conference);           
        }
        //2
        else if (kind == 1)
        {
            ChangeRoom(School);
        }
        //3
        else if (kind == 2)
        {
            ChangeRoom(Office);
        }

        if (RoomInput.text != "" && RoomNum.text != "")
        {           
            byte numByte = byte.Parse(RoomNum.text);      
            if(!SecretToggle.isOn) //일반방
            {
                //PhotonNetwork.CreateRoom(RoomInput.text == "" ? "Room" + Random.Range(0, 100) : RoomInput.text, new RoomOptions { MaxPlayers = numByte });
                Debug.Log("일반방");
                PhotonNetwork.CreateRoom(RoomInput.text+"@R@"+ kind, new RoomOptions { MaxPlayers = numByte });
            }
            else if(SecretToggle.isOn && SecretInput.text != "") //비밀방
            {
                Debug.Log("비밀방");
                PhotonNetwork.CreateRoom(RoomInput.text+ "@R@" + kind +"@PW@" + SecretInput.text, new RoomOptions { MaxPlayers = numByte });
            }  
            
        }        
        
        CS.UpdateState(RoomInput.text);
        RoomInput.text = "";
        RoomNum.text = "";
        SecretToggle.isOn = false;
    }

    public override void OnJoinedRoom()
    {
        LoadingPanel.GetComponent<LoadingManager>().timer = 0f;
        LoadingPanel.GetComponent<LoadingManager>().progressBar.fillAmount = 0;
        LoadingPanel.SetActive(true);

        GameObject.Find("PauseRoom").GetComponent<PauseRoom>().cam.GetComponent<CameraMovement>().enabled = true;

        //Chat.SetActive(true);
        //LobbyPanel.SetActive(false);
        ShowPanel(Chat);

        GameObject.Find("Background2").SetActive(false);
        cam.SetActive(true);
        
        GameManager.instance.isConnect = true;

        Cursor.lockState = CursorLockMode.Locked; // 마우스
        Cursor.visible = false;
       
        
        RoomRenewal();
        CM.clean();
        ChatInput.text = "";        
        
    }

    public override void OnCreateRoomFailed(short returnCode, string message) { RoomInput.text = ""; CreateConfirmBtn(); }

    //public override void OnJoinRandomFailed(short returnCode, string message) { RoomInput.text = ""; CreateRoom(); }

    //player�� �濡 ���� �� ȣ��
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        RoomRenewal(); //����� ���Դ� ������ �� �� �� ����
        InformRPC(newPlayer.NickName + "님이 입장하였습니다");
        //CS.UpdateState("접속중");
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        RoomRenewal();
        InformRPC(otherPlayer.NickName + "님이 퇴장하였습니다.");
        //CS.UpdateState("온라인");
    }

    void RoomRenewal()
    {
        ListText.text = "";
        for (int i = 0; i < PhotonNetwork.PlayerList.Length; i++)
            ListText.text += PhotonNetwork.PlayerList[i].NickName + ((i + 1 == PhotonNetwork.PlayerList.Length) ? "" : ", ");

        string roomName = PhotonNetwork.CurrentRoom.Name;
        RoomInfoText.text = roomName.Substring(0, roomName.IndexOf("@R@"));
        /*if (roomName.Contains("@PW@"))
        {
            RoomInfoText.text = roomName.Substring(0, roomName.IndexOf("@PW@"));

        }
        else { RoomInfoText.text = roomName.Substring(0, roomName.IndexOf("@R@")); }*/

        CS.UpdateState(RoomInfoText.text);
        CountInfoText.text = "현재 : " + PhotonNetwork.CurrentRoom.PlayerCount + "명";
        MaxInfoText.text = "최대 "+PhotonNetwork.CurrentRoom.MaxPlayers + "명";
    }
    #endregion


#region send
    
    public void BtnSend()
    {
        for (int i = 0; i < PhotonNetwork.PlayerList.Length; i++)
        {
            if ((PhotonNetwork.PlayerList[i].NickName).Equals(PhotonNetwork.LocalPlayer.NickName))
            {                
                CM.Send(ChatInput.text);
            }
        }
        PV.RPC("ChatRPC", RpcTarget.Others, PhotonNetwork.NickName + " : " + ChatInput.text);
        ChatInput.text = "";
        ChatInput.DeactivateInputField();
    }

    public void TFSend()
    {
             
        if(Input.GetKeyDown(KeyCode.Return) && ChatInput.text != "")
        {            
            for (int i = 0; i < PhotonNetwork.PlayerList.Length; i++)
            {
                if ((PhotonNetwork.PlayerList[i].NickName).Equals(PhotonNetwork.LocalPlayer.NickName))
                {                   
                    CM.Send(ChatInput.text);
                    
                }
            }
            PV.RPC("ChatRPC", RpcTarget.Others, PhotonNetwork.NickName + " : " + ChatInput.text);
            ChatInput.text = "";
        }
            
        ChatInput.DeactivateInputField();

        GameObject myPlayer = GameObject.Find("GameManager").GetComponent<GameManager>().myPlayer;
        myPlayer.GetComponent<cshMute>().enabled = true;
        myPlayer.GetComponent<Strafer>().enabled = true;
        myPlayer.GetComponent<Croucher>().enabled = true;
        myPlayer.GetComponent<Emoji>().enabled = true;
        myPlayer.GetComponent<DirectAimController>().enabled = true;
        GameObject.Find("PauseRoom").GetComponent<PauseRoom>().cam.GetComponent<CameraMovement>().enabled = true;
        GameObject.Find("PauseRoom").GetComponent<PauseRoom>().cam.GetComponent<ChangeCamera>().isKeyboardOn = false;
        Cursor.lockState = CursorLockMode.Locked; // 마우스
        Cursor.visible = false;
    }

    void InformRPC(string msg)
    {
        CM.Inform(msg);
    }

    [PunRPC]
    void ChatRPC(string msg)
    {
        CM.ChatTo(msg);
    }
    #endregion

    #region 기타
    public void GetMyName(string myID)
    {
        var request = new GetUserDataRequest() { PlayFabId = myID };
        PlayFabClientAPI.GetUserData(request, (result) => {

            ID = result.Data["id"].Value;
            UserName = result.Data["name"].Value;

        },
            (error) => print("������ �ҷ����� ����")
            );
    }

    public void XBtn()
    {
        if (PhotonNetwork.InLobby)
        {
            PhotonNetwork.Disconnect(); //ondisconnected
            CS.UpdateState("온라인");
        }
        else if (PhotonNetwork.InRoom)
        {
            PhotonNetwork.LeaveRoom();
            CS.UpdateState("온라인");
        }
    }

    void ShowPanel(GameObject CurPanel)
    {
        SideBar.SetActive(false);
        LobbyPanel.SetActive(false);
        Chat.SetActive(false);

        CurPanel.SetActive(true);
    }
    void ChangeRoom(GameObject CurRoom)
    {
        Conference.SetActive(false);
        School.SetActive(false);
        Office.SetActive(false);

        CurRoom.SetActive(true);
    }

    public void ToggleChange()
    {
        SecretInput.interactable = SecretToggle.isOn;

        if (!SecretToggle.isOn)
        {
            SecretInput.text = "";
        }
    }
    #endregion


}