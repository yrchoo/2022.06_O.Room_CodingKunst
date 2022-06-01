using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviourPunCallbacks
{
    public static GameManager instance = null;
    public bool isConnect = false;
    public Transform[] spawnPoints;

    public GameObject myPlayer;

    public int index; // 1~32


    public void Awake()
    {
        index = PlayerPrefs.GetInt("userCustom");
        
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else if (instance != this)
        {
            Destroy(this.gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(CreatePlayer());
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator CreatePlayer()
    {

        yield return new WaitUntil(() => isConnect);

        spawnPoints = GameObject.Find("SpawnPointGroup").GetComponentsInChildren<Transform>();

        Vector3 pos = spawnPoints[PhotonNetwork.CurrentRoom.PlayerCount % 4].position;
        Quaternion rot = spawnPoints[PhotonNetwork.CurrentRoom.PlayerCount % 4].rotation;

        myPlayer = PhotonNetwork.Instantiate($"Player{index}", pos, rot);

    }

    public override void OnLeftRoom()
    {
        instance.isConnect = false;
        StartCoroutine(CreatePlayer());
    }
}