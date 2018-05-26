using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : CharCreature {
    public void Start()
    {
        this.gameObject.GetComponent<Animator>().SetTrigger("attack");
    }
    public override void SetIdleAnim()
    {
        this.gameObject.GetComponent<Animator>().SetBool("isStop", false);
    }




    public override void SetStop()
    {

        this.gameObject.GetComponent<Animator>().SetBool("isStop", true);
    }

}
