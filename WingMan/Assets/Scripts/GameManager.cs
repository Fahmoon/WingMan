using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class CheckGameState : UnityEvent<GameStates>
{

}
[System.Serializable]
public class CheckPlayerState : UnityEvent<PlayerStates>
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
    public CheckGameState CheckMyGameStates;
    public CheckPlayerState CheckMyPlayerStates;
    #endregion
    GameStates currentGameState;
    public static int currentLevel;
    public PlayerStats playerStats;
    public int coinCount;
    private static GameManager instance;
    public GameStates CurrentGameState
    {
        get => currentGameState;
        set
        {
            currentGameState = value;
            CheckMyGameStates.Invoke(currentGameState);
        }
    }
    public PlayerStates CurrentPlayerState
    {
        get => playerStats.currentPlayerState;
        set
        {
            playerStats.currentPlayerState = value;
            CheckMyPlayerStates.Invoke(value);
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
        CheckMyGameStates.RemoveAllListeners();
    }
    #region Public Methods
    public void StartGame()
    {
        CurrentGameState = GameStates.Playing;
    }
    public void RestartGame()
    {

    }
    public void WinGame()
    {
        // CurrentState = GameStates.Playing;

        currentLevel++;
//        ObstacleGenerator.Instance.GenerateNewLevel();
        PlayerPrefs.SetInt("CurrentLevel", currentLevel);
        // LogAchieveLevelEvent(shownLevel.ToString());

    }
    public void GameOver()
    {
        CurrentGameState = GameStates.Lose;
    }

    #endregion


    #region Unity Callbacks

    private void Start()
    {
        if (!PlayerPrefs.HasKey("CurrentLevel"))
        {
            PlayerPrefs.SetInt("CurrentLevel", 0);
            currentLevel = PlayerPrefs.GetInt("CurrentLevel");
            CurrentGameState = GameStates.MainMenu;
            CurrentPlayerState = PlayerStates.Idle;
            //ObstacleGenerator.Instance.GenerateNewLevel(_clipPoints);
        }
        else if (currentLevel != PlayerPrefs.GetInt("CurrentLevel"))
        {
            currentLevel = PlayerPrefs.GetInt("CurrentLevel");
            //ObstacleGenerator.Instance.GenerateNewLevel(_clipPoints);
            CurrentPlayerState = PlayerStates.Idle;
            CurrentGameState = GameStates.MainMenu;
            //TODO
            //Can Load a saved level here instead of generating a new one if we don't want the player to see a new level each time he enters the game
        }
        else
        {
            CurrentGameState = GameStates.Playing;

        }
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

        //_points.upperLeft= cam.ViewportToWorldPoint(new Vector3(0, 1, distance)); 
        //_points.upperRight = cam.ViewportToWorldPoint(new Vector3(1, 1, distance));
        //_points.downLeft = cam.ViewportToWorldPoint(new Vector3(1,0,distance));
        //_points.downRight = cam.ViewportToWorldPoint(new Vector3(0,0,distance));

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
