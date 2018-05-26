using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlowParts : MonoBehaviour {//需要配合collider（poly）
    public int currentPart;//
    public bool canBlow=false;
	// Use this for initialization
	void Start () {
        currentPart = 0;
	}
	
	// Update is called once per frame
	void Update () {
        if (canBlow == true)
        {
            this.transform.GetChild(currentPart).gameObject.AddComponent<Rigidbody2D>();//给该部件添加RigidBody2D
            this.transform.GetChild(currentPart).gameObject.GetComponent<Animator>().SetBool("isStop", true);//停止该部件的动画
            this.transform.GetChild(currentPart).GetComponent<Rigidbody2D>().velocity = new Vector2(Random.Range(-15, 15), Random.Range(0, 20));//随机发射一个方向
            this.transform.GetChild(currentPart).GetComponent<Rigidbody2D>().angularVelocity = Random.Range(-90, 90);
            this.transform.GetChild(currentPart).GetComponent<Rigidbody2D>().drag = 0.3f;
            canBlow = false;
            currentPart++;
        }
        if (Input.GetButtonDown("Fire1"))//测试用！
        {
            Blow();
        }

	}
    public void Blow()
    {
        if(currentPart<this.transform.childCount)//如果此时可以炸飞一个部件
            canBlow = true;
    }
}
