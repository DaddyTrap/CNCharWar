using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioIntoBattle : MonoBehaviour {

	// Use this for initialization
	void Start () {
        MusicManager.instance.StopBGM();
        MusicManager.instance.PlayBGM("ling_battlebgm");	
    }
}
