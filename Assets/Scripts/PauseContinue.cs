using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseContinue : MonoBehaviour {

    public Camera MainMenuCamera;
    public Image PauseImage;
    public GameObject PausePanel;

    public void pause()
    {
        PauseImage.gameObject.SetActive(true);
        PausePanel.SetActive(true);
    }

}
