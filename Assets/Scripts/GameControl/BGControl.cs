using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGControl : MonoBehaviour {
	public GameObject[] bgs;

	int time = -1;

	private float _distance;
	public float distance {
		get { return _distance; }
	}
	void Start() {
		_distance = Mathf.Abs(bgs[1].transform.position.x - bgs[0].transform.position.x);
	}

	public void Next() {
		time++;
		if (time != 0) {
			var pos = bgs[time % 2].transform.position;
			pos.x += 2 * distance;
			bgs[time % 2].transform.position = pos;
		}
	}
}
