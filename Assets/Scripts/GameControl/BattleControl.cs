using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleControl : MonoBehaviour {
	public BackGroundController bgController;
	public Player player;
	public List<Enemy> enemies;

	[System.Serializable]
	public class Battle {
		public Enemy[] battleEnemies;
	}

	public Battle[] battles;

	void Start() {
		// TODO: 监听选字的事件

		// 监听 player 死亡事件
		player.OnDead += PlayerDeadHandler;
	}

	void OnDestroy() {
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
