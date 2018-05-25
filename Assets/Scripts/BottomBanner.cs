using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BottomBanner : MonoBehaviour {
    public GameObject template;//字的模板
    public List<GameObject> BottomString;
    public int maximum;//列表中最多可以有多少项

    public Sprite 火;
    public Sprite 水;
    public Sprite 木;
    public Sprite 石;
    public Sprite 口;
    public float deltaTime;
    public List<string> selectedString;
    // Use this for initialization
    void Start () {
        test();
        //StartCoroutine(GenerateWord());
	}
	IEnumerator GenerateWord()//每隔一段时间新增一次字
    {
        AddWord("木");

        yield return new WaitForSeconds(deltaTime);
        StartCoroutine(GenerateWord());
    }
	// Update is called once per frame
	void Update () {
		
	}
    void test()
    {
       // AddWord("火");
        AddWord("木");
        AddWord("水");
        AddWord("石");
        Debug.Log("yes");
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
            switch (word)
            {
                case "火":
                    newWord.transform.GetChild(0).GetComponent<Image>().sprite = 火;
                    break;
                case "水":
                    newWord.transform.GetChild(0).GetComponent<Image>().sprite = 水;
                    break;
                case "木":
                    newWord.transform.GetChild(0).GetComponent<Image>().sprite = 木;
                    break;
                case "口":
                    newWord.transform.GetChild(0).GetComponent<Image>().sprite = 口;
                    break;
                case "石":
                    newWord.transform.GetChild(0).GetComponent<Image>().sprite = 石;
                    break;
            }

            BottomString.Add(newWord);//列表里添加该元素
        }
    }

    public void SelectCharacter(string word)
    {
        selectedString.Add(word);
    }
    public void CancelCharacter(string word)
    {
        selectedString.Remove(word);
    }
}
