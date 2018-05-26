using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlowParts : MonoBehaviour {//需要配合collider（poly）
    public int currentPart;//
    public bool canBlow=false;
    public bool goShake = false;
    public bool goExplode = false;
    public float waitSeconds=3f;
    public ParticleSystem Explosion;//爆炸的粒子效果
	// Use this for initialization
	void Start () {
        this.GetComponent<Enemy>().OnCurSlotSizeChanged += ThinkBlow;//订阅
        this.GetComponent<Enemy>().OnDead += Explode;//订阅死亡事件
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
            StartCoroutine(FadeOutInDelay(this.transform.GetChild(currentPart).gameObject));
            canBlow = false;
            currentPart++;
        }
        if (goExplode == true)
        {
            goExplode = false;
            StartCoroutine(ExplodeInDelay());
        }
        if (goShake == true)
        {
            //StartCoroutine();
            this.transform.GetChild(currentPart).gameObject.GetComponent<Animator>().SetBool("isStop", true);
            this.transform.position += new Vector3(Random.Range(-0.02f, 0.02f), Random.Range(-0.02f, 0.02f), 0);
        }
        if (Input.GetButtonDown("Fire1"))//测试用！
        {
            Blow();
        }

	}
    IEnumerator ExplodeInDelay()
    {
        yield return new WaitForSeconds(1f);//抖完
        var tempPC = Instantiate(Explosion);
        tempPC.transform.position = this.transform.position;//移动到该位置
        this.gameObject.SetActive(false);
        tempPC.Play();//播放爆炸动画
        Debug.Log("Exploded");
    }

    IEnumerator FadeOutInDelay(GameObject toFade)
    {
        yield return new WaitForSeconds(waitSeconds);
        //toFade.SetActive(false);
        toFade.GetComponent<Animator>().SetBool("canFade", true);
        StartCoroutine(DisableInDelay(toFade));
    }
    IEnumerator DisableInDelay(GameObject toDisable)
    {
        yield return new WaitForSeconds(1f);
        toDisable.SetActive(false);
    }
    public void ThinkBlow(int size)//size 是剩下的血量//!!!未测试
    {
        int countRemain=0;
        for(int i = 0; i < this.transform.childCount; ++i)
        {
            if (this.transform.GetChild(i).gameObject.activeInHierarchy == false)
            {
                countRemain++;
            }
        }
        for(int i = size; i < countRemain; ++i)
        {
            Blow();
        }
    }
    public void Blow()
    {
        if(currentPart<this.transform.childCount-1)//如果此时可以炸飞一个部件!!最后一个部件要用Explode来处理
            canBlow = true;
        else
        {
            this.GetComponent<Enemy>().OnCurSlotSizeChanged -= ThinkBlow;
        }
    }

    public void Explode()
    {
        goShake = true;
        goExplode = true;
        this.GetComponent<Enemy>().OnDead -= Explode;
    }
}
