using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharCreature : MonoBehaviour {
	public CharCreatureInfo basicInfo;
	public CharCreatureInfo deltaInfo;
	public CharCreatureInfo curInfo {
		get { return basicInfo + deltaInfo; }
	}
	public float curHp = 1000;

	public class Buff {
		public BuffInfo buff;
		public float pastTime;
		public Buff(BuffInfo buff) {
			this.buff = buff;
			pastTime = 0f;
		}
	}
	public List<Buff> buffs;

	protected void Awake() {
		curHp = curInfo.maxHp;
		buffs = new List<Buff>();
	}

	public void Attack(List<string> characters) {
		// TODO: 具体攻击逻辑
	}

	public delegate void OnAttackedHandler(AttackInfo attackInfo);
	public event OnAttackedHandler OnAttacked;

	protected void AddBuff(BuffInfo info) {
		bool found = false;
		foreach (var i in buffs) {
			// 检查是否是相同的 buff
			if (i.buff.charCreatureInfo == info.charCreatureInfo) {
				// 如果是相同 buff ，则重置 buff 时间
				i.pastTime = 0f;
				found = true;
				break;
			}
		}
		if (!found) {
			buffs.Add(new Buff(info));
		}
	}

	// attackInfo 里面有 damage，而参数中的 damage 是 attackInfo.damage 的计算值
	// FIXME: 所以这是冗余的，但暂时没有想到好的设计
	public void _OnAttacked(AttackInfo attackInfo, float calDamage) {
		var finalDamage = calDamage - curInfo.def;
		this.Damage(calDamage);
		if (attackInfo.debuff != null) {
			AddBuff(attackInfo.debuff);
		}

		// Emit Event
    if (this.OnAttacked != null)
      this.OnAttacked(attackInfo);
	}

	public delegate void OnDeadHandler();
	public event OnDeadHandler OnDead;
	public virtual void Damage(float damage) {
		if (curHp - damage > 0) {
			curHp -= damage;
		} else {
			curHp = 0;
      if (this.OnDead != null)
        this.OnDead();
		}
	}

	public void SetIdleAnim() {
		// TODO: 播放 Idle 动画
	}

	public void SetMoveAnim() {
		// TODO: 播放 Move 动画
	}
}
