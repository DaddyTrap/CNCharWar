[System.Serializable]
public class CharCreatureInfo {
	public float maxHp = 1000;
	public float atk = 100;
	public float def = 10;

	public CharCreatureInfo(float maxHp, float atk, float def) {
			this.maxHp = maxHp;
			this.atk = atk;
			this.def = def;
	}

	public static CharCreatureInfo operator+(CharCreatureInfo info0, CharCreatureInfo info1) {
		return new CharCreatureInfo(
			info0.maxHp + info1.maxHp,
			info0.atk + info1.atk,
			info0.def + info1.def);
	}
}
