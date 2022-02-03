using UnityEngine;
using WebSocketSharp; // websocket library

// 사용자 데이터
class UserData
{
    public int type; // 타입 (1: 최초 접속)
    public UserInfo userInfo = new UserInfo();
}

[System.Serializable]
class UserInfo
{
    public int userNumber; // 사용자 고유 번호
}

public class WebSocketConnection : MonoBehaviour
{
    private string host = "127.0.0.1";
    private string port = "8000";
    private WebSocket ws;

    void Start()
    {
        ws = new WebSocket("ws://" + host + ":" + port);
        ws.OnOpen += ws_OnOpen; // 서버가 연결되면 실행될 함수 등록
        ws.OnMessage += ws_OnMessage; // 서버가 메시지를 보냈을 때 실행될 함수 등록        
        ws.OnClose += ws_OnClose; // 서버가 연결을 종료하면 실행될 함수 등록
        ws.Connect(); // 서버에 연결
    }

    // 서버가 연결되면 실행될 함수
    void ws_OnOpen(object sender, System.EventArgs e)
    {
        UserData userData = new UserData();
        userData.type = 1;
        userData.userInfo.userNumber = 211;

        string data = JsonUtility.ToJson(userData);
        ws.Send(data);
    }

    // 서버가 메시지를 보냈을 때 실행될 함수
    void ws_OnMessage(object sender, MessageEventArgs e)
    {
        Debug.Log("Server: " + e.Data);
    }

    // 서버가 연결을 종료하면 실행될 함수
    void ws_OnClose(object sender, CloseEventArgs e)
    {
        Debug.Log("The server has closed the connection.");
    }
}