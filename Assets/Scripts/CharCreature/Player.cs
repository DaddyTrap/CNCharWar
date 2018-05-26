using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
	public CharCreatureInfo basicInfo;
	public CharCreatureInfo deltaInfo;
	public CharCreatureInfo curInfo {
		get { return basicInfo + deltaInfo; }
	}
	public float curHp = 1000;
	public int maxSlotSize = 10;
	public int curSlotSize {
		get {
			return Mathf.CeilToInt(curHp / basicInfo.maxHp * maxSlotSize);
		}
	}

	void Awake() {
		curHp = curInfo.maxHp;
	}

	public void Attack(List<string> characters) {
		var str = CharManager.instance.SearchCharacterByStrings(characters);
	}

	public delegate void OnAttackedHandler(AttackInfo attackInfo);
	public event OnAttackedHandler OnAttacked;

	public void _OnAttacked(AttackInfo attackInfo) {
		this.OnAttacked(attackInfo);
		// TODO: Self attacked handle
	}

	public delegate void OnCurSlotSizeChangedHandler(int size);
	public event OnCurSlotSizeChangedHandler OnCurSlotSizeChanged;
	public delegate void OnDeadHandler();
	public event OnDeadHandler OnDead;
	public void Damage(float damage) {
		int lastCurSlotSize = curSlotSize;
		if (curHp - damage > 0) {
			curHp -= damage;
		} else {
			curHp = 0;
			this.OnDead();
		}
		if (curSlotSize != lastCurSlotSize) {
			this.OnCurSlotSizeChanged(curSlotSize);
		}
	}
}
