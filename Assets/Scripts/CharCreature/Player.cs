using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : CharCreature {
	public int maxSlotSize = 10;
	public int curSlotSize {
		get {
			return Mathf.CeilToInt(curHp / basicInfo.maxHp * maxSlotSize);
		}
	}

	public delegate void OnCurSlotSizeChangedHandler(int size);
	public event OnCurSlotSizeChangedHandler OnCurSlotSizeChanged;
	public override void Damage(float damage) {
		int lastCurSlotSize = curSlotSize;
		base.Damage(damage);
		if (curSlotSize != lastCurSlotSize) {
			if (this.OnCurSlotSizeChanged != null)
				this.OnCurSlotSizeChanged(curSlotSize);
		}
	}
}
