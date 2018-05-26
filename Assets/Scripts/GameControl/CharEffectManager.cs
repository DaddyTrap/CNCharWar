using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class CharEffectManager {
  public static CharEffectManager instance {
    get {
      if (_instance == null)
        _instance = new CharEffectManager();
      return _instance;
    }
  }

  static CharEffectManager _instance = new CharEffectManager();

  public Dictionary<string, GameObject> effectDict;

  CharEffectManager() {
    effectDict = new Dictionary<string, GameObject>();
    var gobjs = Resources.LoadAll("Particles", typeof(GameObject));
    foreach (var gobj in gobjs) {
      effectDict.Add(gobj.name, gobj as GameObject);
    }
  }
}
