using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AreaScript : MonoBehaviour
{   
    [Header("Main")]
    public RectTransform ListAreaRect;
    public Text TeamText, NameText, RoleText;
    public string Team, Name, Role;

    [Header("Chat")]
    public RectTransform AreaRect, BoxRect, TextRect;
    public GameObject Tail;
    public Text TimeText, UserText, DateText;
    public Image UserImage;
    public string Time, User;
}
