using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BottomBanner : MonoBehaviour {//职责：隔一段时间来一个字；把选好的字体传给；拖拽完成后删除
    public GameObject template;//字的模板
    public List<GameObject> BottomString;
    public int maximum;//列表中最多可以有多少项
    public float deltaTime;
    public List<GameObject> selectedString;
    public List<string> executeWord;
    public Player player;
    public Image Cover;//覆盖在BottomBanner上的东西（Stretch）

    public delegate void ConfirmSelectedCharacterHandler(List<string> characters);
    public event ConfirmSelectedCharacterHandler ConfirmSelectedCharacter;

    // Use this for initialization
    void Start () {
        player.OnHpChanged +=SyncSlot;//监听玩家的槽的个数变化
        /*AddWord("火");
        AddWord("火");
        AddWord("火");
        AddWord("火");*/
    }
	// Update is called once per frame
	void Update () {

    }
    public void SyncSlot(float HPPercentage)
    {
        //Cover.rectTransform.rect.xMin = HPPercentage * 1400;
        Cover.rectTransform.offsetMin=new Vector2(HPPercentage * 1400+258, -500);//同步Hp条长度

    }

    public void AddWord(string word)//输入一个字，增加这个字体的
    {

        if (BottomString.Count >= maximum)//此时不添加
        {
            return;
        }else
        {
            GameObject newWord =Instantiate(template,template.transform.parent);
            newWord.name = word;
            newWord.SetActive(true);
            newWord.transform.GetChild(0).GetComponent<Image>().sprite = CharImgManager.instance.imgDict[word];

            BottomString.Add(newWord);//列表里添加该元素
        }
    }

    public void SelectCharacter(GameObject word)
    {
        selectedString.Add(word);
    }
    public void CancelCharacter(GameObject word)
    {
        selectedString.Remove(word);
    }
    public void ExecuteWord()//开始执行技能；
    {//先删除节点，再执行技能

        for(int i = 0; i < selectedString.Count; ++i)
        {
            executeWord.Add(selectedString[i].name);
            BottomString.Remove(selectedString[i].gameObject);
            Destroy(selectedString[i].gameObject);

        }
        selectedString.Clear();
        //Player
        if (this.ConfirmSelectedCharacter != null)
        {
            this.ConfirmSelectedCharacter(executeWord);
        }
        executeWord.Clear();
    }
}
