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
        showWinUI();
    }

    public void showWinUI()
    {
        WinImage.gameObject.SetActive(true);
        WinPanel.SetActive(true);
        LoseImage.gameObject.SetActive(false);
        LosePanel.gameObject.SetActive(false);
    }

    public void showLoseUI()
    {
        WinImage.gameObject.SetActive(false);
        WinPanel.SetActive(false);
        LoseImage.gameObject.SetActive(true);
        LosePanel.SetActive(true);
    }

    public void ReStart()
    {
        SceneManager.LoadScene("TitleScene");
    }

    public void Out()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
}
