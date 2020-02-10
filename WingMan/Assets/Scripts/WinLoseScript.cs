using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WinLoseScript : MonoBehaviour
{
    public Button vibrationButton;
    public Button soundButton;
    int isVibrate;
    int isSound;
    public Text currentLevel;
    public Text NextLevel;
    public Image vibrationOff;
    public Image soundOff;
    public void Start()
    {
        currentLevel.text = (GameManager.shownLevel + 1).ToString();
        NextLevel.text =  (GameManager.shownLevel + 2).ToString();
        isVibrate = PlayerPrefs.GetInt("isvibration", 0);
        isSound = PlayerPrefs.GetInt("isSound", 0);
        VibrationSetData();
        SoundSetData();
     //   SoundManager.Instance.Stop();
    }
   
    public void ReplayButton()
    {
        switch (GameManager.Instance.CurrentState)
        {
          
            case GameStates.Win:
                GameManager.Instance.WinGame();
                break;
            case GameStates.MainMenu:

                GameManager.Instance.StartGame();
                
                break;
            case GameStates.Lose:
                GameManager.Instance.RestartGame();
                break;
        }
    }
   

    void OnEnable()
    {
        if (SoundManager.Instance != null)
        {
            isVibrate = PlayerPrefs.GetInt("isvibration", 0);
            isSound = PlayerPrefs.GetInt("isSound", 0);
            VibrationSetData();
            SoundSetData();
        }
    }
  
    public void VibrationButtton()
    {
        if (PlayerPrefs.GetInt("isvibration") == 0)
        {
            isVibrate = 1;
            PlayerPrefs.SetInt("isvibration", 1);
            vibrationOff.gameObject.SetActive(true);

        }
        else
        {
            isVibrate = 0;
            PlayerPrefs.SetInt("isvibration", 0);
            vibrationOff.gameObject.SetActive(false);

        }
    }
    public void VibrationSetData()
    {
        if (PlayerPrefs.GetInt("isvibration") == 0)
        {
            vibrationOff.gameObject.SetActive(false);

        }
        else
        {
            vibrationOff.gameObject.SetActive(true);
        }
    }
    public void SoundSetData()
    {
        if (PlayerPrefs.GetInt("isSound") == 0)
        {
            soundOff.gameObject.SetActive(false);

        //    SoundManager.Instance.notMute();
        }
        else
        {
            soundOff.gameObject.SetActive(true);
          //  SoundManager.Instance.Mute();

        }
    }
    public void SoundButton()
    {
        if (PlayerPrefs.GetInt("isSound") == 0)
        {
         //   isSound = 1;
          //   SoundManager.Instance.Mute();
            PlayerPrefs.SetInt("isSound", 1);
        soundOff.gameObject.SetActive(true);


        }
        else
        {
       //     isSound = 0;
       //    SoundManager.Instance.notMute();
            PlayerPrefs.SetInt("isSound", 0);
            soundOff.gameObject.SetActive(false);
        }
    }
}
