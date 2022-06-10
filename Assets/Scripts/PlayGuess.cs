using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine.SceneManagement;

public class PlayGuess : MonoBehaviour
{
   
    public static string _playerId; 
    public void PlayGuest()
    {
        PlayFabClientAPI.LoginWithAndroidDeviceID(new LoginWithAndroidDeviceIDRequest
        {
           AndroidDeviceId =SystemInfo.deviceUniqueIdentifier
        },
            Succes =>
            {
                Debug.Log("Misafir Girişi Başarılı");
                _playerId = Succes.PlayFabId;
                GuessDisplayName();
            },
            Error => 
            {
                Debug.Log("Misafir Girişi Başarısız");
            });

    }
    public void GuessDisplayName()
    {
        PlayFabClientAPI.UpdateUserTitleDisplayName(new UpdateUserTitleDisplayNameRequest() 
        {
        DisplayName ="Guest"+Random.Range(1,100).ToString()
        }, Result => 
        {
            SceneManager.LoadScene(1);
        }, Error => 
        {
            Debug.Log("Başarısız Giriş");
        });


    }




}
