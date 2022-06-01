using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

using TMPro;

public class NickNameDisplay : MonoBehaviour
{

    [SerializeField] PhotonView playerPv;
    [SerializeField] TMP_Text text;


    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(playerPv.Owner.NickName);
        text.text = playerPv.Owner.NickName;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
