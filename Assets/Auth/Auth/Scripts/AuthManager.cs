using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoginReturnValues
{
    public int success;
    public int error_code;
    public int userNumber;
}

public class AuthManager : MonoBehaviour
{
    [Header("LoginPanel")]
    public InputField IDInputField; // ID
    public InputField PWInputField; // PW

    [Header("SignUpPanel")]
    public InputField SignUpIDInputField; // 회원가입 ID
    public InputField SignUpPWInputField; // 회원가입 PW

    public GameObject SignUpPanelObj; // 회원가입 패널

    public string LoginUrl;
    public string SignUpUrl;
    public LoginReturnValues loginReturnValues;

    // Start is called before the first frame update
    void Start()
    {
        LoginUrl = "localhost:3000/api/auth/login";
        SignUpUrl = "loclahost:3000/api/auth/signup";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // 로그인 버튼
    public void LoginBtn()
    {
        StartCoroutine(LoginCo());
    }

    IEnumerator LoginCo()
    {
        WWWForm form = new WWWForm();
        form.AddField("id", IDInputField.text);
        form.AddField("pw", PWInputField.text);

        UnityWebRequest www = UnityWebRequest.Post(LoginUrl, form);
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(www.error);
        }
        else
        {
            // Show results as text
            Debug.Log(www.downloadHandler.text);

            loginReturnValues = JsonUtility.FromJson<LoginReturnValues>(www.downloadHandler.text);

            // 로그인 성공 시
            if (loginReturnValues.success == 1)
            {
                SceneManager.LoadScene("ProfileScene");
            }
        }
    }

    // 회원가입 버튼
    public void ToSignUpBtn()
    {
        SignUpPanelObj.SetActive(true); // 회원가입 패널 활성화
    }

    public void SignUpBtn()
    {
        StartCoroutine(SignUpCo());
    }

    IEnumerator SignUpCo()
    {
        WWWForm form = new WWWForm();
        form.AddField("id", SignUpIDInputField.text);
        form.AddField("pw", SignUpPWInputField.text);

        UnityWebRequest www = UnityWebRequest.Post(SignUpUrl, form);
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(www.error);
        }
        else
        {
            // Show results as text
            Debug.Log(www.downloadHandler.text);

            // Or retrieve results as binary data
            byte[] results = www.downloadHandler.data;
        }
    }

    // 회원가입 취소 버튼
    public void SignUpCancelBtn()
    {
        SignUpPanelObj.SetActive(false); // 회원가입 패널 활성화
    }

}
