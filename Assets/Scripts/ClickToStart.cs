using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ClickToStart : MonoBehaviour
{
    public GameObject start;
    public GameObject inner;
    public GameObject jie;
    public GameObject zi;
    public void ClickStart()
    {
        Animation[] Animations1 = start.GetComponentsInChildren<Animation>();
        for(int i = 0; i < Animations1.Length; i++)
        {
            Animations1[i].Play();
        }
        Animation[] Animations2 = inner.GetComponentsInChildren<Animation>();
        for(int i = 0;i< Animations2.Length; i++)
        {
            Animations2[i].Play();
        }
        Animation A1 = jie.GetComponent<Animation>();
        A1.Play();
        Animation A2 = zi.GetComponent<Animation>();
        A2.Play();

        StartCoroutine(GotoScene());
    }

    IEnumerator GotoScene()
    {
        yield return new WaitForSeconds(1.1f);
        SceneManager.LoadScene("MenuScene");
    }
}
