using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

[System.Serializable]
public class CheckState : UnityEvent<GameStates>
{

}

public struct ClipPoints
{
    public Vector3 upperLeft, upperRight, downLeft, downRight;

    public override string ToString()
    {
        string _returnStr = string.Concat("------Plane Points------" + System.Environment.NewLine +
            "Upper Right: " + upperRight + System.Environment.NewLine +
            "Upper Left: " + upperLeft + System.Environment.NewLine +
            "Down Left: " + downLeft + System.Environment.NewLine +
            "Down Right: " + downRight);

        return _returnStr;
    }
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

    #region Static Methods
    public static ClipPoints CalculatePlayGround(float distance, Camera cam)
    {
        ClipPoints _points = new ClipPoints();

        Transform camTrans = cam.transform;
        Vector3 camPos = camTrans.position;
        Vector3 camProj = camPos + camTrans.forward * distance;
        float halfFOV_Rad = cam.fieldOfView * 0.5f * Mathf.Deg2Rad;
        float aspect = cam.aspect;

        float height = Mathf.Tan(halfFOV_Rad) * distance;
        float width = height * aspect;

        _points.upperLeft = camProj;
        _points.upperLeft -= camTrans.right * width;
        _points.upperLeft += camTrans.up * height;

        _points.upperRight = camProj;
        _points.upperRight += camTrans.right * width;
        _points.upperRight += camTrans.up * height;

        _points.downLeft = camProj;
        _points.downLeft -= camTrans.right * width;
        _points.downLeft -= camTrans.up * height;

        _points.downRight = camProj;
        _points.downRight += camTrans.right * width;
        _points.downRight -= camTrans.up * height;

        return _points;
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
