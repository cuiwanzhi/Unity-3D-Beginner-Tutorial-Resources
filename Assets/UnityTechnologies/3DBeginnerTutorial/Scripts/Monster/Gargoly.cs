using System;
using System.Collections;
using System.Collections.Generic;
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
		// this._collider = this.GetComponent<CapsuleCollider>();
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
			GameEnding.instance().OnFail(other.gameObject);
		}
	}

	
}
