using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

[System.Serializable]
public class CheckState : UnityEvent<GameStates>
{

}

public class GameManager : MonoBehaviour
{
    #region Public Variables
    public CheckState CheckMyStates;
    #endregion
    GameStates currentState;
    public static int actualLevel;
    public static int shownLevel;
    public PlayerStats playerStats;
    public int coinCount;
    private static GameManager instance;
    static bool firstTime;
    public GameStates CurrentState
    {
        get => currentState;
        set
        {
            currentState = value;
            CheckMyStates.Invoke(currentState);
        }
    }

    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<GameManager>();
            }
            return instance;
        }

    }
    private void OnDestroy()
    {
        CheckMyStates.RemoveAllListeners();
    }
    #region Public Methods
    public void StartGame()
    {
        CurrentState = GameStates.Playing;
    }
    public void RestartGame()
    {
        SceneManager.LoadScene(actualLevel);
    }
    public void WinGame()
    {
        // CurrentState = GameStates.Playing;

        if (actualLevel < SceneManager.sceneCountInBuildSettings - 1)
        {

            actualLevel++;
        }
        else
        {
            actualLevel = 0;
        }
        shownLevel++;
        PlayerPrefs.SetInt("ShownLevel", shownLevel);
        PlayerPrefs.SetInt("CurrentLevel", actualLevel);
       // LogAchieveLevelEvent(shownLevel.ToString());
        SceneManager.LoadScene(actualLevel);

    }
    public void GameOver()
    {
        CurrentState = GameStates.Lose;
    }

    #endregion


    #region Unity Callbacks

    private void Start()
    {
        if (!PlayerPrefs.HasKey("CurrentLevel"))
        {
            PlayerPrefs.SetInt("CurrentLevel", 0);
            actualLevel = PlayerPrefs.GetInt("CurrentLevel");

            CurrentState = GameStates.MainMenu;

        }
        else if (actualLevel != PlayerPrefs.GetInt("CurrentLevel"))
        {
            actualLevel = PlayerPrefs.GetInt("CurrentLevel");
            firstTime = true;
            SceneManager.LoadScene(actualLevel);

        }
        else if (firstTime)
        {
            CurrentState = GameStates.MainMenu;
            firstTime = false;

        }
        else
        {
            CurrentState = GameStates.Playing;

        }
        shownLevel = PlayerPrefs.GetInt("ShownLevel");
    }
    #endregion
//    void Awake()
//    {
//        if (FB.IsInitialized)
//        {
//            FB.ActivateApp();
//        }
//        else
//        {
//            //Handle FB.Init
//            FB.Init(() =>
//            {
//                FB.ActivateApp();
//            });
//        }
//        //        Debug.Log("here");
//        /* Mandatory - set your AppsFlyer’s Developer key. */
//        AppsFlyer.setAppsFlyerKey("Lion Dev");
//        /* For detailed logging */
//        /* AppsFlyer.setIsDebug (true); */
//#if UNITY_IOS
//        /* Mandatory - set your apple app ID
//        NOTE: You should enter the number only and not the "ID" prefix */
//        AppsFlyer.setAppID("zcKrZYJWnrWWctCxcLNnyT");
//        AppsFlyer.getConversionData();
//        AppsFlyer.trackAppLaunch();
//#elif UNITY_ANDROID
//        /* For getting the conversion data in Android, you need to add the "AppsFlyerTrackerCallbacks" listener.*/
//        AppsFlyer.init("Lion Dev", "AppsFlyerTrackerCallbacks");
//#endif
//    }
    //public void LogAchieveLevelEvent(string level)
    //{
    //    var parameters = new Dictionary<string, object>();
    //    parameters[AppEventParameterName.Level] = level;
    //    FB.LogAppEvent(
    //        AppEventName.AchievedLevel, null,
    //        parameters
    //    );
    //}
}
