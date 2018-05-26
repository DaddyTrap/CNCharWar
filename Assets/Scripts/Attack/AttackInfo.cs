[System.Serializable]
public class AttackInfo {
  // !!! 必须手动给到 charId
  public int charId;
  public float damage;
  public float heal;
  public BuffInfo buff;
  public BuffInfo debuff;
}