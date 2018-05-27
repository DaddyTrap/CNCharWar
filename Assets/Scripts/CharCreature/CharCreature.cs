using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//update更新bufftime

public class CharCreature : MonoBehaviour {
	public CharCreatureInfo basicInfo;
	public CharCreatureInfo deltaInfo;
	public int maxSlotSize = 10;
	public int curSlotSize {
		get {
			return Mathf.CeilToInt(curHp / basicInfo.maxHp * maxSlotSize);
		}
	}


    public CharCreatureInfo curInfo {
		get { return basicInfo + deltaInfo; }
	}
	public float curHp = 1000;

	public class Buff {
		public BuffInfo buff;
		public float pastTime;//过去的时间
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

	public delegate void OnAttackHandler(AttackInfo attackInfo, float damage);
	public event OnAttackHandler OnAttack;
	public virtual void Attack(AttackInfo attackInfo) {
		// 如果有buff，则增加buff
		if (attackInfo.buff != null) {
			AddBuff(attackInfo.buff);
		}
		// 如果有治疗，则治疗
		if (attackInfo.heal != 0) {
			MusicManager.instance.PlaySE("heal");
			Heal(attackInfo.heal);
		}
		// 如果有伤害，则计算
		var damage = attackInfo.damage * this.curInfo.atk;
		Debug.Log(gameObject.name + " 发动攻击");
		// 触发Attack事件
		if (this.OnAttack != null) {
			this.OnAttack(attackInfo, damage == 0 ? 0f : damage);
		}


	}
    public int slotsize = 0;

	void Update() {
        slotsize = curSlotSize;
		var newDeltaInfo = new CharCreatureInfo(0, 0, 0);
		for(int i = buffs.Count - 1; i >= 0; --i) {
			buffs[i].pastTime += Time.deltaTime;
			if (buffs[i].pastTime >= buffs[i].buff.time) {
					buffs.RemoveAt(i); //删除该buff
			} else {
				newDeltaInfo += buffs[i].buff.charCreatureInfo;
			}
		}
		deltaInfo = newDeltaInfo;
	}



	public delegate void OnAttackedHandler(AttackInfo attackInfo);
	public event OnAttackedHandler OnAttacked;

	protected void AddBuff(BuffInfo info) {
		bool found = false;
		foreach (var i in buffs) {
			// 检查是否是相同的 buff
      // FIXME: 不能确定引用是否保持
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
		this.Damage(finalDamage);
		if (attackInfo.debuff != null) {
			AddBuff(attackInfo.debuff);
		}

		// Emit Event
    if (this.OnAttacked != null)
      this.OnAttacked(attackInfo);
	}

	public delegate void OnDeadHandler();
	public event OnDeadHandler OnDead;
	public delegate void OnCurSlotSizeChangedHandler(int size);
	public event OnCurSlotSizeChangedHandler OnCurSlotSizeChanged;
    public delegate void OnHPChangedHandler(float HPPercentage);//ZYQ HP改变时发射
    public event OnHPChangedHandler OnHpChanged;

	public virtual void Damage(float damage) {
    int lastCurSlotSize = curSlotSize;

    // Deal Damage
		if (curHp - damage > 0) {
			curHp -= damage;
		} else {
			curHp = 0;
			Debug.Log(gameObject.name + ": 死亡");
      if (this.OnDead != null)
        this.OnDead();
		}
        if (this.OnHpChanged != null)
            this.OnHpChanged(curHp / curInfo.maxHp);//发射此时HP占比
        if (curSlotSize != lastCurSlotSize) {//!!!
			if (this.OnCurSlotSizeChanged != null)
				this.OnCurSlotSizeChanged(curSlotSize);
            Debug.Log("changed!");//改变！
		}
	}

  public virtual void Heal(float heal) {
    int lastCurSlotSize = curSlotSize;

    // Deal Heal
		var maxHp = curInfo.maxHp;
		if (curHp + maxHp * heal <= curInfo.maxHp) {
			curHp += maxHp * heal;
		} else {
			curHp = curInfo.maxHp;
		}
        if (this.OnHpChanged != null)
            this.OnHpChanged(curHp/curInfo.maxHp);
        if (curSlotSize != lastCurSlotSize) {
			if (this.OnCurSlotSizeChanged != null)
				this.OnCurSlotSizeChanged(curSlotSize);
		}
  }

	public virtual void SetIdleAnim() {
		// TODO: 播放 Idle 动画
	}

	public virtual void SetMoveAnim() {
		// TODO: 播放 Move 动画
	}

    public virtual void SetStop() {
        // TODO: 人物暂停不动
    }

    public virtual void ShowSkill()
    {
        // TODO: 人物发出技能
    }
}
