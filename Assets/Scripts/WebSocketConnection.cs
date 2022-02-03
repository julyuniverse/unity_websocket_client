using UnityEngine;
using WebSocketSharp; // websocket library

// ����� ������
class UserData
{
    public int type; // Ÿ�� (1: ���� ����)
    public UserInfo userInfo = new UserInfo();
}

[System.Serializable]
class UserInfo
{
    public int userNumber; // ����� ���� ��ȣ
}

public class WebSocketConnection : MonoBehaviour
{
    private string host = "127.0.0.1";
    private string port = "8000";
    private WebSocket ws;

    void Start()
    {
        ws = new WebSocket("ws://" + host + ":" + port);
        ws.OnOpen += ws_OnOpen; // ������ ����Ǹ� ����� �Լ� ���
        ws.OnMessage += ws_OnMessage; // ������ �޽����� ������ �� ����� �Լ� ���        
        ws.OnClose += ws_OnClose; // ������ ������ �����ϸ� ����� �Լ� ���
        ws.Connect(); // ������ ����
    }

    // ������ ����Ǹ� ����� �Լ�
    void ws_OnOpen(object sender, System.EventArgs e)
    {
        UserData userData = new UserData();
        userData.type = 1;
        userData.userInfo.userNumber = 211;

        string data = JsonUtility.ToJson(userData);
        ws.Send(data);
    }

    // ������ �޽����� ������ �� ����� �Լ�
    void ws_OnMessage(object sender, MessageEventArgs e)
    {
        Debug.Log("Server: " + e.Data);
    }

    // ������ ������ �����ϸ� ����� �Լ�
    void ws_OnClose(object sender, CloseEventArgs e)
    {
        Debug.Log("The server has closed the connection.");
    }
}