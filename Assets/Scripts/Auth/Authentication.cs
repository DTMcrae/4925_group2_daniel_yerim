using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

using TMPro;

public class Authentication : MonoBehaviour
{
    [Header("Login")]
    [SerializeField] TMP_Text textDisplayLogin;

    [SerializeField] TMP_InputField usernameFieldLogin;
    [SerializeField] TMP_InputField passwordFieldLogin;

    [Header("Sign Up")]
    [SerializeField] TMP_Text textDisplaySignup;

    [SerializeField] TMP_InputField usernameFieldSignup;
    [SerializeField] TMP_InputField passwordFieldSignup;

    [System.Serializable]
    public class ErrorResponse
    {
        public string message;
    }

    public void LoginFunction()
    {
        //textDisplayLogin.text = "Button Pressed";

        StartCoroutine(PostLogin());
    }

    public void SignupFunction()
    {
        //textDisplaySignup.text = "Button Pressed";

        StartCoroutine(PostSignup());
    }

    private string userId;
    private string username;

    public IEnumerator PostLogin()
    {
        string url = API.Base + "auth/login";
        WWWForm formData = new WWWForm();
        Debug.Log("Url: (" + url + ")");
        formData.AddField("username", usernameFieldLogin.text);
        formData.AddField("password", passwordFieldLogin.text);

        UnityWebRequest request = UnityWebRequest.Post(url, formData);
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            try
            {
                var response = JsonUtility.FromJson<AuthResponse>(request.downloadHandler.text);

                userId = response.userId;
                username = response.username;

                textDisplayLogin.text = $"Welcome, {username} (ID: {userId})";

                GameManager.Instance.SetUserDetails(username, int.Parse(userId));
                GameManager.Instance.LoadScene(3);
            }
            catch
            {
                textDisplayLogin.text = "Error parsing response.";
            }
        }
        else
        {
            textDisplayLogin.text = $"Login failed: {request.error}";
        }
    }

    public IEnumerator PostSignup()
    {
        string url = API.Base + "auth/signup";
        WWWForm formData = new WWWForm();
        formData.AddField("username", usernameFieldSignup.text);
        formData.AddField("password", passwordFieldSignup.text);

        UnityWebRequest request = UnityWebRequest.Post(url, formData);
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            try
            {
                // Parse the JSON response
                var response = JsonUtility.FromJson<AuthResponse>(request.downloadHandler.text);

                // Store the user's ID and username
                userId = response.userId;
                username = response.username;

                textDisplaySignup.text = $"Account created for {username} (ID: {userId})";
                GameManager.Instance.SetUserDetails(username, int.Parse(userId));
                GameManager.Instance.LoadScene(3);
            }
            catch
            {
                textDisplaySignup.text = "Error parsing signup response.";
            }
        }
        else
        {
            //textDisplaySignup.text = $"Signup failed: {request.error}";

            // Parse error response from the backend
            try
            {
                var errorResponse = JsonUtility.FromJson<ErrorResponse>(request.downloadHandler.text);
                textDisplaySignup.text = $"Signup failed: {errorResponse.message}";
            }
            catch
            {
                textDisplaySignup.text = $"Signup failed: {request.error}";
            }
        }
    }


    [System.Serializable]
    private class AuthResponse
    {
        public string userId;
        public string username;
    }
}
