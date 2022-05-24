using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class followPlayer : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform player;

    private PhotonView pv;
    private Transform emoji;
    void Start()
    {
        pv = GetComponent<PhotonView>();
        emoji = GetComponent<Transform>();
        player = transform.parent.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if(pv.IsMine){
            emoji.position = new Vector3(player.position.x, player.position.y + 1.4f, player.position.z);
        }
        
    }
}
