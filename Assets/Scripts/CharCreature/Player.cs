using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : CharCreature {
    public void Start()
    {
        this.gameObject.GetComponent<Animator>().SetTrigger("attack");
        // this.OnDead += GoDown;
    }
    public override void SetIdleAnim()
    {
        this.gameObject.GetComponent<Animator>().SetBool("isStop", false);
    }
    public override void SetStop()
    {
        this.gameObject.GetComponent<Animator>().SetBool("isStop", true);
    }
    // public void GoDown()
    // {

    //     this.OnDead -= GoDown;//取消监听
    // }

}
