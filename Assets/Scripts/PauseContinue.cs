using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseContinue : MonoBehaviour {

    public Camera MainMenuCamera;
    public Image PauseImage;
    public GameObject PausePanel;
    public BattleControl battleControl;

    public void Start()
    {
        ExitPause();
    }

    public void pause()
    {
        PauseImage.gameObject.SetActive(true);
        PausePanel.SetActive(true);
        battleControl.battling = false;
    }

    public void ExitPause()
    {
        PauseImage.gameObject.SetActive(false);
        PausePanel.SetActive(false);
        battleControl.battling = true;
    }

    public void backToStart()
    {
        SceneManager.LoadScene("TitleScene");
    }
}
