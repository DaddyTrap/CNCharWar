using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottomBanner : MonoBehaviour {
    public GameObject template;//字的模板


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void AddWord(string word)
    {
        GameObject newWord = Instantiate(template);
        newWord.name = word;

    }
}
