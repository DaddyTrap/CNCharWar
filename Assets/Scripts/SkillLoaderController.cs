using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillLoaderController : MonoBehaviour {
    public string character;
    public GameObject SkillLoader;
    public GameObject Enemy;
    public GameObject Dai;
    public float MoveSpeed;         //推荐值4000
    public bool canMove = false;
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
        if (canMove == true) {
            this.transform.position = /*this.transform.position +*/ Vector3.MoveTowards(this.transform.position, Enemy.transform.position, MoveSpeed*Time.deltaTime);
            if (this.transform.position.Equals(Enemy.transform.position))
            {
                canMove = false;
            }
        }
        
	}
    public void MoveToEnemyOrDai()
    {
        canMove = true;
    }


}
