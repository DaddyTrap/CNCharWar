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
        MusicManager.instance.PlaySE("click");
        PauseImage.gameObject.SetActive(true);
        PausePanel.SetActive(true);
        battleControl.battling = false;
    }

    public void ExitPause()
    {
        MusicManager.instance.PlaySE("click");
        PauseImage.gameObject.SetActive(false);
        PausePanel.SetActive(false);
        battleControl.battling = true;
    }

    public void backToStart()
    {
        MusicManager.instance.PlaySE("click");
        SceneManager.LoadScene("TitleScene");
    }
}
