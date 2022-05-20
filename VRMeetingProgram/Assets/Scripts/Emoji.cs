using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using System.Reflection;

public class Emoji : MonoBehaviour
{
    public Transform[] spawnPoints;

    public GameObject[] Emojis;

    public GameObject[] PrefabsToInstantiate;

    public int index;

    // Start is called before the first frame update
    void Start()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Instantiate(Emojis[0], Emojis[0].transform.position, Emojis[0].transform.rotation);
            Destroy(Emojis[0], 2.0f);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Instantiate(Emojis[1], Emojis[1].transform.position, Emojis[1].transform.rotation);
            Destroy(Emojis[1], 2.0f);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            Instantiate(Emojis[2], Emojis[2].transform.position, Emojis[2].transform.rotation);
            Destroy(Emojis[2], 2.0f);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            Instantiate(Emojis[3], Emojis[3].transform.position, Emojis[3].transform.rotation);
            Destroy(Emojis[3], 2.0f);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            Instantiate(Emojis[4], Emojis[4].transform.position, Emojis[4].transform.rotation);
            Destroy(Emojis[4], 2.0f);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            Instantiate(Emojis[5], Emojis[5].transform.position, Emojis[5].transform.rotation);
            Destroy(Emojis[5], 2.0f);
        }
    }

    // Update is called once per frame
    void Update()
    {
    }

    void CreateEmoji()
    {
        spawnPoints = GameObject.Find("EmojiSpawnPoint").GetComponentsInChildren<Transform>();
        //Transform[] spawnPoints = GameObject.Find("SpqwnPointGroup").GetComponentsInChildren<Transform>();
        Vector3 pos = spawnPoints[PhotonNetwork.CurrentRoom.PlayerCount].position;
        Quaternion rot = spawnPoints[PhotonNetwork.CurrentRoom.PlayerCount].rotation;

        var parent = PhotonNetwork.Instantiate("Emoji", pos, rot);

        GameObject child = this.PrefabsToInstantiate[index];

        Instantiate(child, new Vector3(0, 0, 0), Quaternion.identity).transform.parent = parent.transform;

    }

    

    

    
}


