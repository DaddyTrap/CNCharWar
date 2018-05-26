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

	public float bannerInterval = 2.0f;
	IEnumerator GenerateNewChar() {
		banner.AddWord(CharManager.instance.GetRandomCharacter());
		yield return new WaitForSeconds(bannerInterval);
		StartCoroutine(GenerateNewChar());
	}

	void Start() {
		// 监听选字的事件
		banner.ConfirmSelectedCharacter += ConfirmSelectHandler;

		// 监听 player 死亡事件
		if (player != null)
			player.OnDead += PlayerDeadHandler;

		StartCoroutine(GenerateNewChar());
	}

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
		}
	}

	void OnDestroy() {
		if (player != null)
			player.OnDead -= PlayerDeadHandler;
	}

	void PlayerDeadHandler() {
		// TODO: 处理玩家死亡事件: 游戏结束
		Debug.Log("玩家死亡，结束战斗");
	}

	int currentBattleIndex = -1;
	void NextBattle() {
		currentBattleIndex++;
		// 如果还有下一个战斗
		if (currentBattleIndex < battles.Length) {
			LoadBattle(battles[currentBattleIndex]);
			// TODO: 滚动背景图
		}
	}

	void LoadBattle(Battle battle) {
		// 生成敌人
		// 监听敌人死亡事件
		foreach (var i in battle.battleEnemies) {
			i.gameObject.SetActive(true);
			i.OnDead += EnemeyDeadHandler;
		}
	}

	int deadEnenmy = 0;
	void EnemeyDeadHandler() {
		++deadEnenmy;
		if (deadEnenmy >= enemies.Count) {
			// 所有敌人死亡
			HandleWin();
		}
	}

	void HandleWin() {
		// TODO: 显示胜利提示等
		Debug.Log("本场Battle胜利");
		NextBattle();
	}
}
