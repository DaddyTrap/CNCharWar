using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FadeInOut : MonoBehaviour {

    public static FadeInOut instance;

    private float fadeSpeed = 7f;
    private bool sceneStarting = true;
    private bool first = true;
    private bool second = false;
    public Image tex;
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    private void FadeToClear()
    {
        tex.color = Color.Lerp(tex.color, Color.clear, fadeSpeed * Time.deltaTime);
    }

    private void FadeToBlack()
    {
        tex.color = Color.Lerp(tex.color, Color.black, fadeSpeed * Time.deltaTime);
    }

    public void StartScene()
    {
        FadeToClear();
        if (tex.color.a <= 0.05f)
        {
            tex.color = Color.clear;
            tex.enabled = false;
            sceneStarting = false;
            first = true;
            second = false;
        }
    }
    public void transition()
    {
        Debug.Log("yes");
        first = false;
    }

    public void EndScen()
    {
        tex.enabled = true;
        FadeToBlack();
        if(tex.color.a >= 0.95)
        {
            first = true;
            second = true;
        }
        /*
        if(tex.color.a >= 0.95f)
        {
            SceneManager.LoadScene("Scenes/DebugScene");
        }*/
    }

    public void Update()
    {
        if (sceneStarting)
        {
            StartScene();
        }
        else if (first == false)
        {
            Debug.Log("ok");
            EndScen();
        }
        else if (second == true)
        {
            StartScene();
        }
    }
}
