using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : CharCreature {
    public float startTime = 0f;
    public float attackInterval = 5f;
    public AttackInfo[] attackSequence;

    public override void SetIdleAnim()
    {
        //this.gameObject.GetComponent<Animator>().SetBool("isStop",false);
        //this.gameObject.GetComponent<Animator>().SetBool("isIdle", true);
    }


    public override void SetStop()
    {
        //this.gameObject.GetComponent<Animator>().SetBool("isStop", true);
        //this.gameObject.GetComponent<Animator>().SetBool("isIdle", false);
    }

    void Start() {
        StartAttack();
    }

    int seqIndex = 0;
    IEnumerator StartAttack() {
        yield return new WaitForSeconds(startTime);
        StartCoroutine(Attacking());
    }

    IEnumerator Attacking() {
        this.Attack(attackSequence[seqIndex++]);
        yield return new WaitForSeconds(attackInterval);
        StartCoroutine(Attacking());
    }
}
