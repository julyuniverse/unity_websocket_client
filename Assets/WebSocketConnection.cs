using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WebSocketSharp; // websocket library

[System.Serializable]
public class UserInfo
{
    public int userNumber; // 유저 번호

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
        ws.OnMessage += ws_OnMessage; // 서버가 메시지를 보냈을 때 실행될 함수 등록
        ws.OnOpen += ws_OnOpen; // 서버가 연결되면 실행될 함수 등록
        ws.OnClose += ws_OnClose; // 서버가 연결을 종료하면 실행될 함수 등록
        ws.Connect(); // 서버에 연결        
    }

    // 서버가 메시지를 보냈을 때 실행될 함수
    void ws_OnMessage(object sender, MessageEventArgs e)
    {
        Debug.Log("Server: " + e.Data);
    }

    // 서버가 연결되면 실행될 함수
    void ws_OnOpen(object sender, System.EventArgs e)
    {
        UserInfo userInfo = new UserInfo();
        userInfo.userNumber = 211;

        string str = JsonUtility.ToJson(userInfo);
        ws.Send(str);
    }

    // 서버가 연결을 종료하면 실행될 함수
    void ws_OnClose(object sender, CloseEventArgs e)
    {
        Debug.Log("The server has closed the connection.");
    }
}