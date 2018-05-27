using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleControl : MonoBehaviour {
	public Player player;
	public List<Enemy> enemies;
	public BottomBanner banner;

	[System.Serializable]
	public class Battle {
		public Enemy[] battleEnemies;
	}

	public Battle[] battles;

	public BGControl bgControl;
	public GameObject camera;
	public CombineEffect combineEffect;

	public bool battling = false;

	public float bannerInterval = 2.0f;
	IEnumerator GenerateNewChar() {
		yield return new WaitUntil(()=>{
			return battling;
		});
		Debug.Log("加入字");
		banner.AddWord(CharManager.instance.GetRandomCharacter());
		yield return new WaitForSeconds(bannerInterval);
		StartCoroutine(GenerateNewChar());
	}

	void Start() {
		// 监听选字的事件
		banner.ConfirmSelectedCharacter += ConfirmSelectHandler;

		// 监听 player 死亡事件
		if (player != null) {
			player.OnDead += PlayerDeadHandler;
			player.OnAttack += PlayerAttackHandler;
		}

		NextBattle();
		StartCoroutine(GenerateNewChar());
	}
    public delegate void OnCharacterFindHandler(string character);
    public event OnCharacterFindHandler OnCharacterFind;//发布消息
    void ConfirmSelectHandler(List<string> chars) {
		var instance = CharManager.instance;
		var res = instance.SearchAttackByStrings(chars);
		if (res == null) {
			Debug.Log("找不到合成字，单个打出");
			foreach (var i in chars) {
				if (player != null) {
					var id = instance.characterRevDict[i];
					player.Attack(instance.charaterAttackInfoDict[id]);
				}
			}
		} else {
			Debug.Log("找到合成字，攻击");
			if (player != null)
				player.Attack(res);
			if (OnCharacterFind != null)
				OnCharacterFind(instance.SearchAttackCharacterByStrings(chars));//发送发现新字消息给DictionaryController;ZYQ添加
		}
	}

	void OnDestroy() {
		if (player != null) {
			player.OnDead -= PlayerDeadHandler;
			player.OnAttack -= PlayerAttackHandler;
		}
	}

	void PlayEffectOnPlayer(GameObject prefab) {
		var newPrefab = Instantiate(prefab);
		newPrefab.AddComponent(typeof(KillSelf));
		newPrefab.transform.position = player.transform.position;
	}

	void PlayEffectOnEnemy(GameObject prefab) {
		var newPrefab = Instantiate(prefab);
		newPrefab.AddComponent(typeof(KillSelf));
		if (enemies.Count > 0) {
			newPrefab.transform.parent = enemies[0].transform;
			newPrefab.transform.localPosition = new Vector3(0, 0, -4f);
		}
	}

	void PlayerDeadHandler() {
		// TODO: 处理玩家死亡事件: 游戏结束
		Debug.Log("玩家死亡，结束战斗");
	}

	void PlayerAttackHandler(AttackInfo attackInfo, float damage) {
		// 玩家攻击
		// 播放动画，动画结束时才真正造成伤害
		if (battling) {
			// 播放动画
			var id = attackInfo.charId;
			// var id = 1;
			var strs = CharManager.instance.idToStringsDict[id];
			var charName = CharManager.instance.GetCharacterById(id);
			Debug.Log(CharEffectManager.instance.effectDict[charName]);
			if (strs.Count >= 2 && strs.Count < 4) {
				// 如果能使用拼字效果，则播放拼字效果
				Debug.Log("拼字效果");
				combineEffect.MakeEffect(strs, charName, ()=>{
					// 播放技能特效
					if (attackInfo.damage != 0) {
						Debug.Log("Play effect");
						PlayEffectOnEnemy(CharEffectManager.instance.effectDict[charName]);
					}
					if (attackInfo.heal != 0) {
						PlayEffectOnPlayer(CharEffectManager.instance.effectDict[charName]);
					}

					// 结算伤害
					this.PlayerAttack(attackInfo, damage);
				});
			} else {
				// 不能使用拼字效果，直接播放特效并结算
				if (attackInfo.damage != 0) {
					PlayEffectOnEnemy(CharEffectManager.instance.effectDict[charName]);
				}
				if (attackInfo.heal != 0) {
					PlayEffectOnPlayer(CharEffectManager.instance.effectDict[charName]);
				}

				this.PlayerAttack(attackInfo, damage);
			}
		}
	}

	void PlayerAttack(AttackInfo attackInfo, float damage) {
		// FIXME: 不知道为何会中途修改了迭代器？
		// foreach (var i in enemies) {
		// 	i._OnAttacked(attackInfo, damage);
		// }
		for (int i = 0; i < enemies.Count; ++i) {
			enemies[i]._OnAttacked(attackInfo, damage);
		}
	}

	int currentBattleIndex = -1;
	void NextBattle() {
		currentBattleIndex++;
		// 如果还有下一个战斗
		if (currentBattleIndex < battles.Length) {
			// 调整背景图位置
			bgControl.Next();
			// 移动摄像头+玩家
			if (currentBattleIndex != 0) {
				MoveCameraAndPlayer(bgControl.distance);
			}
			LoadBattle(battles[currentBattleIndex]);
		}
	}

	public float moveSpeed = 0.1f;
	void Update() {
		if (move) {
			battling = false;
			var playerNext = Vector3.MoveTowards(player.transform.position, playerMoveTarget, moveSpeed);
			var cameraNext = Vector3.MoveTowards(camera.transform.position, cameraMoveTarget, moveSpeed);
			if (
				Mathf.Abs(playerNext.x - player.transform.position.x) < float.Epsilon &&
				Mathf.Abs(cameraNext.x - camera.transform.position.x) < float.Epsilon
				) {
				move = false;
				battling = true;
			} else {
				player.transform.position = playerNext;
				camera.transform.position = cameraNext;
			}
		}

		// 测试效果
		if (Input.GetKeyDown(KeyCode.S)) {
			battling = !battling;
		}
	}

	private bool move;
	private Vector3 playerMoveTarget;
	private Vector3 cameraMoveTarget;
	void MoveCameraAndPlayer(float distance) {
		move = true;
		var pos = player.transform.position;
		pos.x += distance;
		playerMoveTarget = pos;
		pos = camera.transform.position;
		pos.x += distance;
		cameraMoveTarget = pos;
	}

	void LoadBattle(Battle battle) {
		enemies.Clear();
		// 生成敌人
		foreach (var i in battle.battleEnemies) {
			enemies.Add(i);
			i.gameObject.SetActive(true);
			// 监听敌人事件
			i.OnDead += EnemyDeadHandler;
			i.OnAttack += EnemyAttackHandler;
		}
	}

	int deadEnenmy = 0;
	void EnemyDeadHandler() {
		++deadEnenmy;
		Debug.Log("敌人死亡");
		if (deadEnenmy >= enemies.Count) {
			// 所有敌人死亡
			HandleWin();
		}
	}

	void EnemyAttackHandler(AttackInfo attackInfo, float damage) {
		// 敌人攻击
		// 播放动画，动画结束时才真正造成伤害
		if (battling) {
			// 播放动画
			var charName = CharManager.instance.GetCharacterById(attackInfo.charId);
			// var charName = CharManager.instance.GetCharacterById(1);
			if (attackInfo.damage != 0) {
				PlayEffectOnPlayer(CharEffectManager.instance.effectDict[charName]);
			}
			if (attackInfo.heal != 0) {
				PlayEffectOnEnemy(CharEffectManager.instance.effectDict[charName]);
			}

			// 结算伤害
			this.EnemyAttack(attackInfo, damage);
		}
	}

	public void EnemyAttack(AttackInfo attackInfo, float damage) {
		player._OnAttacked(attackInfo, damage);
	}

	// bool canNext = false;
	void HandleWin() {
		// TODO: 显示胜利提示等
		Debug.Log("本场Battle胜利");
		// canNext = true;
		NextBattle();
	}
}
