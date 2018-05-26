using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackGroundController : MonoBehaviour {
    public Sprite BackGround;
    public bool isMoving=true;//标志是否正在滚动
    public float scrollSpeed;//滚动速度
    public Image currentBackGround;
    public GameObject Canvas;

    public List<float> stopTime;//场景停下的时间
    public int stopTimeID;
	void Start () {
        
        currentBackGround.transform.position.Set(0, 0, 0);
        currentBackGround.sprite = BackGround;
        stopTimeID = 0;
	}
	
	// Update is called once per frame
	void Update () {//
        if (isMoving)
        {
            currentBackGround.transform.position = currentBackGround.transform.position + new Vector3(-scrollSpeed * Time.deltaTime, 0, 0);//从左边滚动到右边
                                                                                                                                           //nextBackGround.transform.position = currentBackGround.transform.position + new Vector3(-scrollSpeed * Time.deltaTime, 0, 0);


            if (stopTime[stopTimeID] - RunningTime(-currentBackGround.transform.position.x + Canvas.transform.position.x) < 0.5f)//表明是需要暂停的情况
            {
                isMoving = false;
                Debug.Log("stop!!");
                stopTimeID++;//进入下一个停止时间
                return;
            }

        }
    }
    float RunningTime(float pos_x)
    {
        //Debug.Log(pos_x);
        return pos_x / scrollSpeed;//返回基于位置计算的时间
    }
}
