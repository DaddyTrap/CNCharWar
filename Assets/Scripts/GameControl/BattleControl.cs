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
		// TODO: 监听选字的事件
		banner.ConfirmSelectedCharacter += ConfirmSelectHandler;

		// 监听 player 死亡事件
		if (player != null)
			player.OnDead += PlayerDeadHandler;

		StartCoroutine(GenerateNewChar());
	}

	void ConfirmSelectHandler(List<string> chars) {
		var res = CharManager.instance.SearchCharacterByStrings(chars);
		if (res == null) {
			// TODO: 找不到字，惩罚
		} else {
			player.Attack(chars);
		}
	}

	void OnDestroy() {
		if (player != null)
			player.OnDead -= PlayerDeadHandler;
	}

	void PlayerDeadHandler() {
		// TODO: 处理玩家死亡事件: 游戏结束
	}

	int currentBattleIndex = -1;
	void NextBattle() {
		currentBattleIndex++;
		// 如果还有下一个战斗
		if (currentBattleIndex < battles.Length) {
			LoadBattle(battles[currentBattleIndex]);
		}
	}

	void LoadBattle(Battle battle) {
		// TODO: 生成敌人
		// TODO: 监听敌人死亡事件
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
		NextBattle();
	}
}
