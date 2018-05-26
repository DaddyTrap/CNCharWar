using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : CharCreature {

    public void Start()
    {
        //SetIdleAnim();
        SetStop();
        ShowSkill();
        SetIdleAnim();
    }
    public override void SetIdleAnim()
    {
        this.gameObject.GetComponent<Animator>().SetBool("isStop",false);
        this.gameObject.GetComponent<Animator>().SetBool("isIdle", true);
    }


    public override void SetStop()
    {
        this.gameObject.GetComponent<Animator>().SetBool("isStop", true);
        this.gameObject.GetComponent<Animator>().SetBool("isIdle", false);
    }
    /*
    public override void ShowSkill()
    {
        this.gameObject.GetComponent<Animator>().SetTrigger("showSkill");
    }*/
}
