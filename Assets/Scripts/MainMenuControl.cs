using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuControl : MonoBehaviour
{

    public Camera MainMenuCamera;
    public GameObject MainPanel;
    public GameObject MenuPanel;
    public GameObject CornerPanel;
    // public string firstScene = "scene_3-1";

    //GameManager manager;

    public void ChangeBGMVolume(float a)
    {
        //GameManager.instance.musicManager.ChangeBGMVolume(a);
    }

    public void ChangeSEVolume(float a)
    {
        //GameManager.instance.musicManager.ChangeSEVolume(a);
    }

    // Use this for initialization
    void Start()
    {
        //manager = GameManager.instance;
        //manager.musicManager.PlayBGM("title");

        // Add items to inventory
        StartCoroutine(AddItems());
    }

    IEnumerator AddItems()
    {
        yield return new WaitForSeconds(1f);
        //manager.inventoryManager.AddItem(manager.jsonManager.itemDict["SimpleSword"]);
        //manager.inventoryManager.AddItem(manager.jsonManager.itemDict["Excalibur"]);
    }

    void Update()
    {

    }

    public void StartGame()
    {
        //manager.lastVDoorName = manager.saveDataManager.saveData.lastVDoorName;
        //Debug.Log(manager.lastVDoorName);
        //manager.SwitchScene(GameManager.instance.saveDataManager.saveData.lastSceneName);
    }

    public void start()
    {
        MainPanel.SetActive(false);
        MenuPanel.SetActive(true);
        CornerPanel.SetActive(true);
        FadeInOut.instance.transition();
    }

    public void corner()
    {
        MainPanel.SetActive(false);
        CornerPanel.SetActive(true);
        MenuPanel.SetActive(false);
    }

    public void ExitCorner()
    {
        MainPanel.SetActive(true);
        CornerPanel.SetActive(false);
        MenuPanel.SetActive(false);
    }

    public void Menu()
    {
        MainPanel.SetActive(false);
        MenuPanel.SetActive(true);
        CornerPanel.SetActive(false);
    }

    public void ExitMenu()
    {
        MainPanel.SetActive(true);
        CornerPanel.SetActive(false);
        MenuPanel.SetActive(false);
    }

    public void Exit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
    Application.Quit();
#endif
    }
}