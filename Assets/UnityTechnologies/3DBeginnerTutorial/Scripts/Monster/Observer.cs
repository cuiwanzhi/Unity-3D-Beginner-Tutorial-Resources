using System;
using UnityEngine;

public class Observer : MonoBehaviour {
	// Start is called before the first frame update
	void Start() {

	}

	// Update is called once per frame
	void Update() {

	}

	/// <summary>
	/// OnTriggerEnter is called when the Collider other enters the trigger.
	/// </summary>
	/// <param name="other">The other Collider involved in this collision.</param>
	void OnTriggerEnter(Collider other) {
		if (other.gameObject.tag == "Player") {
			// 投射一条射线，检测是否触碰到了玩家
			// 需要获取所有的碰撞器，然后判断是否有碰撞器碰撞到了玩家
			RaycastHit[] hits = Physics.RaycastAll(this.transform.position, other.transform.position - this.transform.position + Vector3.up, 100);
			Array.Sort(hits, this.HitComparison); // 将结果按照远近排序
			for (int idx = 0; idx < hits.Length; idx++) {
				var hit = hits[idx];
				if (hit.collider.gameObject.tag == "monster") {
					continue;
				}
				if (hit.collider.gameObject.tag == "Player") {
					GameEnding.instance().OnFail(other.gameObject);
					return;
				}
				if (hit.collider.gameObject.tag != "Player") {
					return;
				}
			}
		}
	}

	private int HitComparison(RaycastHit a, RaycastHit b) {
		if (a.distance <= b.distance) {
			return -1;
		}
		return 1;
	}
}
