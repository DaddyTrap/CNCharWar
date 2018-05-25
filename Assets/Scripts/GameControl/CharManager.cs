using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharManager : MonoBehaviour {

	/* Singleton Begin */
	private static CharManager instance_;

	public static CharManager instance {
		get {
			return instance_;
		}
	}

	void Awake() {
		if (!instance_) {
			instance_ = this;
		} else {
			this.enabled = false;
		}
	}
	/* Singleton End */

	void LoadJson() {

	}
}
