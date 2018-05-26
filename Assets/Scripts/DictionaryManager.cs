using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DictionaryManager : MonoBehaviour {
    public Dictionary<string, bool> isFound;
    public Dictionary<string, string> theBook;
    public GameObject template;

    #region Singleton
    private static DictionaryManager instance_;

    public static DictionaryManager instance
    {
        get
        {
            return instance_;
        }
    }

    void Awake()
    {
        if (!instance_)
        {
            instance_ = this;
        }
        else
        {
            this.enabled = false;
        }
        Init();
    }
    #endregion

    #region LoadExplanation


    [System.Serializable]
    public class ExplanationJsonItem
    {
        public string character;
        public string explanation;
    }



    [System.Serializable]
    public class ExplanationJson
    {
        public ExplanationJsonItem[] data;

    }

    void LoadJson()
    {


        var Explains = Resources.Load("Jsons/Explanation") as TextAsset;
        //Debug.Log(Explains.text);
        var ExplainJson = JsonUtility.FromJson(Explains.text, typeof(ExplanationJson)) as ExplanationJson;



        foreach (var i in ExplainJson.data)
        {
            theBook.Add(i.character, i.explanation);//添加入字典
            isFound.Add(i.character, false);
            Debug.Log(i.character);
        }



        Debug.Log("Loading characters finished");
    }
    #endregion

    void Init()
    {
        isFound = new Dictionary<string, bool>();
        theBook = new Dictionary<string, string>();

        LoadJson();

    }






    public void NewFoundCharacter(string character)
    {
        //isFound[character] = true;

            isFound[character] = true;
            Debug.Log(isFound[character]);


    }
    // Use this for initialization
    void Start () {
        /*foreach(var i in isFound)
        {
            NewFoundCharacter(i.Key);
        }*/
        NewFoundCharacter("沯");
        NewFoundCharacter("呇");
        NewFoundCharacter("炎");
        NewFoundCharacter("炑");
        NewFoundCharacter("磊");
        NewFoundCharacter("杏");

        GenerateDictionaryForm();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    void GenerateDictionaryForm()
    {
        foreach(var i in isFound)
        {
            
           // Debug.Log("1");
            if (i.Value == false) continue;//不执行显示
            Debug.Log("2");
            GameObject temp=Instantiate(template);
            temp.transform.SetParent(template.transform.parent);
            temp.name = i.Key;
            temp.SetActive(true);
            temp.transform.GetChild(0).transform.GetChild(0).GetComponent<Image>().sprite = 
                CharImgManager.instance.imgDict[i.Key];//图片赋值
            temp.transform.GetChild(1).GetComponent<Text>().text = theBook[i.Key];

        }
    }
    public void BackUp()
    {
        this.gameObject.SetActive(false);
    }
    public void ShowUp()
    {
        this.gameObject.SetActive(true);
    }
}
