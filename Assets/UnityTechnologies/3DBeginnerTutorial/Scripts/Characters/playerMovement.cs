using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 玩家移动组件
/// </summary>
public class playerMovement : MonoBehaviour {

	private float _horizontal;
	private float _vertical;
	/// <summary>
	/// 移动方向
	/// </summary>
	private Vector3 _movement;
	private Animator _animator;
	private Rigidbody _rigidbody;
	private Vector3 _startPosition;

	private AudioSource _audioSource;

	// Start is called before the first frame update
	void Start() {
		this._movement = new Vector3(this._horizontal, 0, this._vertical);
		this._animator = GetComponent<Animator>();
		this._rigidbody = GetComponent<Rigidbody>();
		this._audioSource = GetComponent<AudioSource>();

		this._startPosition = this.transform.position;
	}

	// Update is called once per frame
	void Update() {
		// 获取玩家输入
		this._horizontal = Input.GetAxis("Horizontal");
		this._vertical = Input.GetAxis("Vertical");
	}

	/// <summary>
	/// This function is called every fixed framerate frame, if the MonoBehaviour is enabled.
	/// </summary>
	void FixedUpdate() {
		// 移动
		this._movement.Set(this._horizontal, 0, this._vertical);
		this._movement.Normalize();
		var is_walking = this._movement != Vector3.zero;
		this._animator.SetBool("is_walking", is_walking);
		if (!is_walking) {
			this._audioSource.Stop();
		}
		else if (!this._audioSource.isPlaying){
			this._audioSource.Play();
		}
	}

	/// <summary>
	/// Callback for processing animation movements for modifying root motion.
	/// </summary>
	void OnAnimatorMove() {
		// 移动
		this._rigidbody.MovePosition(this._rigidbody.position + this._movement * this._animator.deltaPosition.magnitude);
		// 旋转
		if (this._movement != Vector3.zero) {
			this._rigidbody.MoveRotation(Quaternion.LookRotation(this._movement));
		}
	}

	public void ReStart() {
		this.transform.position = this._startPosition;
	}

}