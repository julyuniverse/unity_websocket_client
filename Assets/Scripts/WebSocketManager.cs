using UnityEngine;
using WebSocketSharp; // websocket library

namespace WebSocketManager
{
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

    public class WebSocketConnection
    {
        private static string host = "127.0.0.1";
        private static string port = "8000";
        private static WebSocket ws;
        private static UserData userData = new UserData();

        public static void connection(int type, int userNumber)
        {
            userData.type = type;
            userData.userInfo.userNumber = userNumber;

            ws = new WebSocket("ws://" + host + ":" + port);
            ws.OnOpen += ws_OnOpen; // ������ ����Ǹ� ����� �Լ� ���
            ws.OnMessage += ws_OnMessage; // ������ �޽����� ������ �� ����� �Լ� ���        
            ws.OnClose += ws_OnClose; // ������ ������ �����ϸ� ����� �Լ� ���
            ws.Connect(); // ������ ����
        }

        // ������ ����Ǹ� ����� �Լ�
        public static void ws_OnOpen(object sender, System.EventArgs e)
        {
            string data = JsonUtility.ToJson(userData);
            ws.Send(data);
        }

        // ������ �޽����� ������ �� ����� �Լ�
        public static void ws_OnMessage(object sender, MessageEventArgs e)
        {
            Debug.Log("Server: " + e.Data);
        }

        // ������ ������ �����ϸ� ����� �Լ�
        public static void ws_OnClose(object sender, CloseEventArgs e)
        {
            Debug.Log("The server has closed the connection.");
        }
    }
}
