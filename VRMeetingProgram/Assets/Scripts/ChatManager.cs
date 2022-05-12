using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class ChatManager : MonoBehaviour
{
    public GameObject YellowArea, WhiteArea, DateArea;
    public RectTransform ContentRect;
    public Scrollbar scrollBar;
    public Toggle MineToggle;
    AreaScript LastArea;

    //public InputField ChatInput;
    
    public void Send(String msg)
    {

        //Chat(true, ChatInput.text, "��", null);
        //ChatInput.text = "";
        Debug.Log(msg);
        Chat(true, msg, "��", null);
    }

    public void ChatTo(String msg)
    {
        string[] msgSp = msg.Split(':');
        Chat(false, msgSp[1], msgSp[0], null);
    }

    public void Inform(String msg)
    {
        Debug.Log(msg);

        Transform CurDateArea = Instantiate(DateArea).transform;
        CurDateArea.SetParent(ContentRect.transform, false);
        CurDateArea.SetSiblingIndex(CurDateArea.GetSiblingIndex());       
        CurDateArea.GetComponent<AreaScript>().DateText.text = msg;
    }

    public void ReceiveMessage(string text)
    {
        if (MineToggle.isOn) Chat(true, text, "��", null);
        else Chat(false, text, "Ÿ��", null);
    }


    /*public void LayoutVisible(bool b)
    {
        AndroidJavaClass kotlin = new AndroidJavaClass("com.unity3d.player.SubActivity");
        kotlin.CallStatic("LayoutVisible", b);
    }*/

    

    public void Chat(bool isSend, string text, string user, Texture2D picture)
    {
        if (text.Trim() == "") return;

        bool isBottom = scrollBar.value <= 0.00001f;

        //print(text);
        
        //������ ����� ���, �޴� ����� ��������� ũ�� ����� �ؽ�Ʈ ����
        AreaScript Area = Instantiate(isSend ? YellowArea : WhiteArea).GetComponent<AreaScript>();
        Area.transform.SetParent(ContentRect.transform, false);
        Area.BoxRect.sizeDelta = new Vector2(600, Area.BoxRect.sizeDelta.y);
        Area.TextRect.GetComponent<Text>().text = text;
        Fit(Area.BoxRect);

        
        // �� �� �̻��̸� ũ�⸦ �ٿ����鼭, �� ���� �Ʒ��� �������� �ٷ� �� ũ�⸦ ���� 
        float X = Area.TextRect.sizeDelta.x + 42;
        float Y = Area.TextRect.sizeDelta.y;
        if (Y > 49)
        {
            for (int i = 0; i < 200; i++)
            {
                Area.BoxRect.sizeDelta = new Vector2(X - i * 2, Area.BoxRect.sizeDelta.y);
                Fit(Area.BoxRect);

                if (Y != Area.TextRect.sizeDelta.y) { Area.BoxRect.sizeDelta = new Vector2(X - (i * 2) + 2, Y); break; }
            }
        }
        else Area.BoxRect.sizeDelta = new Vector2(X, Y);

        
        // ���� �Ϳ� �б��� ������ ��¥�� �����̸� ����
        DateTime t = DateTime.Now;
        Area.Time = t.ToString("yyyy-MM-dd-HH-mm");
        Area.User = user;


        // ���� ���� �׻� ���ο� �ð� ����
        int hour = t.Hour;
        if (t.Hour == 0) hour = 12;
        else if (t.Hour > 12) hour -= 12;
        Area.TimeText.text = (t.Hour > 12 ? "���� " : "���� ") + hour + ":" + t.Minute.ToString("D2");

        
        // ���� �Ͱ� ������ ���� �ð�, ���� ���ֱ�
        bool isSame = LastArea != null && LastArea.Time == Area.Time && LastArea.User == Area.User;
        if (isSame) LastArea.TimeText.text = "";
        Area.Tail.SetActive(!isSame);


        // Ÿ���� ���� �Ͱ� ������ �̹���, �̸� ���ֱ�
        if (!isSend)
        {
            Area.UserImage.gameObject.SetActive(!isSame);
            Area.UserText.gameObject.SetActive(!isSame);
            Area.UserText.text = Area.User;
            if (picture != null) Area.UserImage.sprite = Sprite.Create(picture, new Rect(0, 0, picture.width, picture.height), new Vector2(0.5f, 0.5f));
        }

        
        // ���� �Ͱ� ��¥�� �ٸ��� ��¥���� ���̱�
        if (LastArea != null && LastArea.Time.Substring(0, 10) != Area.Time.Substring(0, 10))
        {
            Transform CurDateArea = Instantiate(DateArea).transform;
            CurDateArea.SetParent(ContentRect.transform, false);
            CurDateArea.SetSiblingIndex(CurDateArea.GetSiblingIndex() - 1);

            string week = "";
            switch (t.DayOfWeek)
            {
                case DayOfWeek.Sunday: week = "��"; break;
                case DayOfWeek.Monday: week = "��"; break;
                case DayOfWeek.Tuesday: week = "ȭ"; break;
                case DayOfWeek.Wednesday: week = "��"; break;
                case DayOfWeek.Thursday: week = "��"; break;
                case DayOfWeek.Friday: week = "��"; break;
                case DayOfWeek.Saturday: week = "��"; break;
            }
            CurDateArea.GetComponent<AreaScript>().DateText.text = t.Year + "�� " + t.Month + "�� " + t.Day + "�� " + week + "����";
        }

        Fit(Area.BoxRect);
        Fit(Area.AreaRect);
        Fit(ContentRect);
        LastArea = Area;

        
        // ��ũ�ѹٰ� ���� �ö� ���¿��� �޽����� ������ �� �Ʒ��� ������ ����
        if (!isSend && !isBottom) return;
        Invoke("ScrollDelay", 0.03f);
    
    }

    void Fit(RectTransform Rect) => LayoutRebuilder.ForceRebuildLayoutImmediate(Rect);


    void ScrollDelay() => scrollBar.value = 0;

    
    /*
    public void OnSendButton(InputField SendInput)
    {
#if (UNITY_EDITOR || UNITY_STANDALONE)
		if (!Input.GetButtonDown("Submit")) return;
		SendInput.ActivateInputField();
#endif
        if (SendInput.text.Trim() == "") return;

        string message = SendInput.text;
        SendInput.text = "";
        Send(message);
    }
    */
}
