using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fade : MonoBehaviour {
    public float alphaDelta=1f;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        var color = GetComponent<SpriteRenderer>().color;
        color.a -= alphaDelta*Time.deltaTime;
        if (color.a <= 0) return;//如果小于零则不继续执行
        GetComponent<SpriteRenderer>().color = color;
    }
}
