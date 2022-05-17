using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AreaScript : MonoBehaviour
{
    

    [Header("Main")]
    public RectTransform ListAreaRect, ListBoxRect, ListTextRect;
    public Text State, Name, Role;
    public Image Image;

    [Header("Chat")]
    public RectTransform AreaRect, BoxRect, TextRect;
    public GameObject Tail;
    public Text TimeText, UserText, DateText;
    public Image UserImage;
    public string Time, User;

}

