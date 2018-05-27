using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WinOrFail : MonoBehaviour {
    public Camera MainMenuCamera;
    public Image WinImage;
    public GameObject WinPanel;
    public Image LoseImage;
    public GameObject LosePanel;

    public void Start()
    {
        
        WinImage.gameObject.SetActive(false);
        WinPanel.SetActive(false);
        LoseImage.gameObject.SetActive(false);
        LosePanel.gameObject.SetActive(false);
    }

    public void showWinUI()
    {
        MusicManager.instance.PlaySE("click");
        MusicManager.instance.StopBGM();
        MusicManager.instance.PlayBGM("Victory_uibgm");
        WinImage.gameObject.SetActive(true);
        WinPanel.SetActive(true);
        LoseImage.gameObject.SetActive(false);
        LosePanel.gameObject.SetActive(false);
    }

    public void showLoseUI()
    {
        MusicManager.instance.PlaySE("click");
        MusicManager.instance.StopBGM();
        MusicManager.instance.PlayBGM("Defeat_uibgm");
        WinImage.gameObject.SetActive(false);
        WinPanel.SetActive(false);
        LoseImage.gameObject.SetActive(true);
        LosePanel.SetActive(true);
    }

    public void ReStart()
    {
        MusicManager.instance.PlaySE("click");
        SceneManager.LoadScene("TitleScene");
    }

    public void Out()
    {
        MusicManager.instance.PlaySE("click");
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
}
