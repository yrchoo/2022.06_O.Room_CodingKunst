using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class followPlayer : MonoBehaviour, IPunObservable
{
    // Start is called before the first frame update
    public Transform player;

    private PhotonView pv;
    private Vector3 currentPos;
    private Quaternion currentRot;
    //private bool  

    void Start()
    {
        pv = GetComponent<PhotonView>();
        //emoji = GetComponent<Transform>();
        if (pv.IsMine)
        {
            player = transform.parent.transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (pv.IsMine)
        {
            gameObject.transform.position = new Vector3(player.position.x, player.position.y + 1.4f, player.position.z);
            //gameObject.transform.rotation = Quaternion.Euler(player.rotation.x, player.rotation.y, player.rotation.z);
            //realCamera.localPosition = Vector3.Lerp(realCamera.localPosition, dirNormalized * finalDistance, Time.deltaTime * smoothness);
            //emoji.position = Vector3.Lerp(emoji.localPosition, currentPos, Time.deltaTime * 10.0f);
        }
        else
        {
            gameObject.transform.position = Vector3.Lerp(gameObject.transform.localPosition, currentPos, Time.deltaTime * 10.0f);
            gameObject.transform.rotation = Quaternion.Lerp(gameObject.transform.localRotation, currentRot, Time.deltaTime * 10.0f);
        }
        
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            //sending Datas...
            stream.SendNext(gameObject.transform.position);
            stream.SendNext(gameObject.transform.rotation);
            //currGrounded = true;
            
        }
        else
        {
            currentPos = (Vector3)stream.ReceiveNext();
            currentRot = (Quaternion)stream.ReceiveNext();
            //currGrounded = (bool)stream.ReceiveNext();
        }
    }
}
