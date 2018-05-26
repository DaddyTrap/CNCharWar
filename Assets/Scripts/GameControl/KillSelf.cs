using UnityEngine;
using System.Collections;

public class KillSelf : MonoBehaviour {
  void Start() {
    StartCoroutine(Kill());
  }

  IEnumerator Kill() {
    yield return new WaitForSeconds(2f);
    Destroy(gameObject);
  }
}
