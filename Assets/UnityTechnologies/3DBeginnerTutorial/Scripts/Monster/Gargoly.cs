using UnityEngine;

/// <summary>
/// 静止怪物类型
/// </summary>
public class Gargoly : MonoBehaviour {

	/// <summary>
	/// 怪物的动画控制器
	/// </summary>
	private Animator _animator;

	/// <summary>
	/// 怪物的碰撞器
	/// </summary>
	// CapsuleCollider _collider;

	// Start is called before the first frame update
	void Start() {
		this._animator = this.GetComponent<Animator>();
	}

	// Update is called once per frame
	void Update() {

	}
}
