using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    public bool isConnect = false;
    public Transform[] spawnPoints;
    public GameObject[] PrefabsToInstantiate;

    public int index;

    public void Awake()
    {
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
        yield return new WaitUntil(()=> isConnect);

        spawnPoints = GameObject.Find("SpawnPointGroup").GetComponentsInChildren<Transform>();

        Vector3 pos = spawnPoints[PhotonNetwork.CurrentRoom.PlayerCount].position;
        Quaternion rot = spawnPoints[PhotonNetwork.CurrentRoom.PlayerCount].rotation;

        GameObject playerTemp = PhotonNetwork.Instantiate("Player", pos, rot, 0);

        GameObject child = this.PrefabsToInstantiate[index];

        Instantiate(child, new Vector3(0, 0, 0), Quaternion.identity).transform.parent = parent.transform;
    }
}
