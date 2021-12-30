using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WebSocketSharp; // websocket library

[System.Serializable]
public class UserInfo
{
    public int userNumber; // ���� ��ȣ

    public void printUserInfo()
    {
        Debug.Log("userNumber: " + userNumber);
    }
}

public class WebSocketConnection : MonoBehaviour
{
    private string host = "127.0.0.1";
    private string port = "8000";
    private WebSocketSharp.WebSocket ws;

    void Start()
    {
        ws = new WebSocketSharp.WebSocket("ws://" + host + ":" + port);
        ws.OnMessage += ws_OnMessage; // ������ �޽����� ������ �� ����� �Լ� ���
        ws.OnOpen += ws_OnOpen; // ������ ����Ǹ� ����� �Լ� ���
        ws.OnClose += ws_OnClose; // ������ ������ �����ϸ� ����� �Լ� ���
        ws.Connect(); // ������ ����        
    }

    // ������ �޽����� ������ �� ����� �Լ�
    void ws_OnMessage(object sender, MessageEventArgs e)
    {
        Debug.Log("Server: " + e.Data);
    }

    // ������ ����Ǹ� ����� �Լ�
    void ws_OnOpen(object sender, System.EventArgs e)
    {
        UserInfo userInfo = new UserInfo();
        userInfo.userNumber = 211;

        string str = JsonUtility.ToJson(userInfo);
        ws.Send(str);
    }

    // ������ ������ �����ϸ� ����� �Լ�
    void ws_OnClose(object sender, CloseEventArgs e)
    {
        Debug.Log("The server has closed the connection.");
    }
}