using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlowParts : MonoBehaviour {//需要配合collider（poly）
    public int currentPart;//
    public bool canBlow=false;
    public bool goShake = false;
    public bool goExplode = false;
    public float waitSeconds=3f;
    public GameObject Explosion;//爆炸的粒子效果
    public Sprite Remaining;//遗留下来的墨水
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
            GameObject stand =Instantiate( this.transform.GetChild(currentPart).gameObject);//创建一个替身
            stand.transform.position = this.transform.GetChild(currentPart).transform.position;//替身与本体坐标一致
            this.transform.GetChild(currentPart).gameObject.SetActive(false);//让本体隐藏
            stand.AddComponent<Rigidbody2D>();//给替身添加刚体部件
            stand.GetComponent<Rigidbody2D>().velocity = new Vector2(Random.Range(-15, 15), Random.Range(0, 20));//给刚体替身一个速度
            stand.GetComponent<Rigidbody2D>().angularVelocity = Random.Range(-90, 90);//给刚体替身一个角速度
            stand.GetComponent<Rigidbody2D>().drag = 0.3f;
            StartCoroutine(FadeOutInDelay(stand));

           /* this.transform.GetChild(currentPart).gameObject.AddComponent<Rigidbody2D>();//给该部件添加RigidBody2D
            this.transform.GetChild(currentPart).gameObject.GetComponent<Animator>().SetBool("isStop", true);//停止该部件的动画
            this.transform.GetChild(currentPart).GetComponent<Rigidbody2D>().velocity = new Vector2(Random.Range(-15, 15), Random.Range(0, 20));//随机发射一个方向
            this.transform.GetChild(currentPart).GetComponent<Rigidbody2D>().angularVelocity = Random.Range(-90, 90);
            this.transform.GetChild(currentPart).GetComponent<Rigidbody2D>().drag = 0.3f;
            StartCoroutine(FadeOutInDelay(this.transform.GetChild(currentPart).gameObject));*/
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
            
            this.transform.position += new Vector3(Random.Range(-0.02f, 0.02f), Random.Range(-0.02f, 0.02f), 0);
        }
        /*if (Input.GetButtonDown("Fire1"))//测试用！
        {
            Blow();
        }*/

	}
    IEnumerator ExplodeInDelay()
    {
        //this.transform.GetChild(currentPart).gameObject.GetComponent<Animator>().SetBool("isStop", true);


        yield return new WaitForSeconds(1f);//抖完
        var tempPC = Instantiate(Explosion);
        tempPC.transform.position = this.transform.position;//移动到该位置
        //this.gameObject.SetActive(false);
        tempPC.GetComponent<ParticleSystem>().Play();//播放爆炸动画
        Debug.Log("Exploded");

        this.GetComponent<SpriteRenderer>().sprite = Remaining;
        this.transform.GetChild(currentPart).gameObject.SetActive(false);//所有子物体都应该消失
        goShake = false;//停止抖动
        
    }

    IEnumerator FadeOutInDelay(GameObject toFade)
    {
        yield return new WaitForSeconds(waitSeconds);
        //toFade.SetActive(false);
        /* toFade.AddComponent<Animator>();
         toFade.GetComponent<Animator>().c
         toFade.GetComponent<Animator>().SetBool("canFade", true);*/
        toFade.AddComponent<Fade>();//一秒后自动消失
        StartCoroutine(DisableInDelay(toFade));
    }
    IEnumerator DisableInDelay(GameObject toDisable)
    {
        yield return new WaitForSeconds(1f);
        toDisable.SetActive(false);
    }
    public void ThinkBlow(int size)//size 是剩下的血量//!!!未测试
    {
        Debug.Log("thinkblow" + size);
        int countRemain=0;
        for(int i = 0; i < this.transform.childCount; ++i)
        {
            if (this.transform.GetChild(i).gameObject.activeInHierarchy == true)
            {
                countRemain++;
            }
        }
        Debug.Log("countRemain" + countRemain);
        for (int i = size-1; i < countRemain; ++i)
        {
            Blow();
        }
    }
    public void Blow()
    {
        if (currentPart < this.transform.childCount - 1) {//如果此时可以炸飞一个部件!!最后一个部件要用Explode来处理
            canBlow = true;
            Debug.Log("真的要blow了");
        }
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
