using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class followPlayer : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform player;

    private Transform emoji;
    void Start()
    {
        emoji = GetComponent<Transform>();
        player = transform.parent.transform;
    }

    // Update is called once per frame
    void Update()
    {
        emoji.position = new Vector3(player.position.x, player.position.y + 1.4f, player.position.z);
    }
}
