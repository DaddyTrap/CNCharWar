using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombineEffectBehavior : MonoBehaviour {
	void DestroySelf() {
		Destroy(gameObject);
	}

	public CombineEffect.EffectEndCallBack callback;

	public void EffectFinish() {
		DestroySelf();
		callback();
	}
}
