using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class PhotonManager : MonoBehaviourPunCallbacks
{

    private readonly string gameVersion = "v1.0";
    private string userId = "khl3846";

    private void Awake()
    {
        PhotonNetwork.GameVersion = gameVersion;

        PhotonNetwork.ConnectUsingSettings();
    }
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("00. 포톤 매니저 시작");
        PhotonNetwork.NickName = userId;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public override void OnConnectedToMaster()
    {
        Debug.Log("01. 포톤 서버에 접속");
        PhotonNetwork.JoinRandomRoom();
    }
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("02. 랜덤 룸 접속 실패");
    
        RoomOptions ro = new RoomOptions();
        ro.IsOpen = true;
        ro.IsVisible = true;
        ro.MaxPlayers = 30;

        PhotonNetwork.CreateRoom("room_1", ro);
    }
    public override void OnCreatedRoom()
    {
        Debug.Log("03. 방 생성 완료");
    }
    public override void OnJoinedRoom()
    {
        Debug.Log("04. 방 입장 완료");
        GameManager.instance.isConnect = true;
    }
}
