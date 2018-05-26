using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterClicked : MonoBehaviour {//绑在点击的MASK上，这一部分可以处理各种颜色的更改
    public GameObject BottomBanner;
    public bool selected=false;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void TellBottomBanner()
    {
        if (selected == false)
        {
            BottomBanner.GetComponent<BottomBanner>().SelectCharacter(this.transform.parent.gameObject);//传入父亲的名字
            GetComponent<UnityEngine.UI.Image>().color = new Color32(156, 156, 156, 100);
            //Debug.Log("Y");
            selected = true;
        }else
        {
            BottomBanner.GetComponent<BottomBanner>().CancelCharacter(this.transform.parent.gameObject);
            selected = false;
            GetComponent<UnityEngine.UI.Image>().color = new Color32(156, 156, 156, 0);
        }
        
    }

}
