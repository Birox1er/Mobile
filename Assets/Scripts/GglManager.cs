using System;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using System.Threading.Tasks;
using UnityEngine;

public class GglManager : MonoBehaviour
{
    public string Token;
    public string Error;
    static public bool successn;

    void Awake()
    {
        PlayGamesPlatform.Activate();
    }

    async void Start()
    {
        await LoginGooglePlayGames();
    }
    //Fetch the Token / Auth code
    public Task LoginGooglePlayGames()
    {
        var tcs = new TaskCompletionSource<object>();
        PlayGamesPlatform.Instance.Authenticate((success) =>
        {
            successn = (success==SignInStatus.Success);
            if (success == SignInStatus.Success)
            {
               
                Debug.Log("Login with Google Play games successful.");
                PlayGamesPlatform.Instance.RequestServerSideAccess(true, code =>
                {
                    Debug.Log("Authorization code: " + code);
                    Token = code;
                    // This token serves as an example to be used for SignInWithGooglePlayGames
                    tcs.SetResult(null);
                });
            }
            else
            {
                Error = "Failed to retrieve Google play games authorization code";
                Debug.Log("Login Unsuccessful");
                tcs.SetException(new Exception("Failed"));
            }
        });
        return tcs.Task;
    }
}


