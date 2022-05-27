using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AreaScript : MonoBehaviour
{   
    [Header("Main")]
    public RectTransform ListAreaRect;
    public Text TeamText, NameText, RoleText, StateText;
    public string Team, Name, Role, State;

    [Header("Chat")]
    public RectTransform AreaRect, BoxRect, TextRect;
    public GameObject Tail;
    public Text TimeText, UserText, DateText;
    public Image UserImage;
    public string Time, User;

    [Header("Lobby")]
    public RectTransform RoomAreaRect;
    public Text RoomText, NumText;
    public string room, num;
}
