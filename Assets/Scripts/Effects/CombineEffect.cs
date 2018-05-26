using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CombineEffect : MonoBehaviour {
	public GameObject effectPrefab;

	public delegate void EffectEndCallBack();
	public void MakeEffect(List<string> strs, string res, EffectEndCallBack callback) {
		if (strs.Count < 2 || strs.Count > 3) {
			Debug.LogError("Do not support effects not between 2 and 3 (inclusive)");
			return;
		}
		var effect = Instantiate(effectPrefab, transform) as GameObject;
		var animator = effect.GetComponent<Animator>();
		for (int i = 0; i < strs.Count; ++i) {
			var childImg = effect.transform.GetChild(i).GetComponent<Image>();
			childImg.sprite = CharImgManager.instance.imgDict[strs[i]];
		}
		var resImg = effect.transform.GetChild(3).GetComponent<Image>();
		resImg.sprite = CharImgManager.instance.imgDict[res];
		var combBehav = effect.GetComponent<CombineEffectBehavior>();
		combBehav.callback = callback;
		switch (strs.Count) {
			case 2:
				animator.Play("CombineTwo");
				break;
			case 3:
				animator.Play("CombineThree");
				break;
		}
	}
}
