using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class CharImgManager {
  public static CharImgManager instance {
    get {
      if (_instance == null)
        _instance = new CharImgManager();
      return _instance;
    }
  }

  static CharImgManager _instance = new CharImgManager();

  public Dictionary<string, Sprite> imgDict;

  public CharImgManager() {
    imgDict = new Dictionary<string, Sprite>();
    var imgs = Resources.LoadAll("单字", typeof(Sprite));
    foreach (var img in imgs) {
      imgDict.Add(img.name, img as Sprite);
    }
  }
}
