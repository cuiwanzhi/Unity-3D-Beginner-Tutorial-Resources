using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Ghost : MonoBehaviour {


	/// <summary>
	/// 路径的目标点
	/// </summary>
	public Transform[] wayPoints;

	/// <summary>
	/// 寻路组件
	/// </summary>
	private NavMeshAgent _navMeshAgent;

	/// <summary>
	/// 当前的目标点index
	/// </summary>
	private int _currentWayPointIndex = 0;

	// Start is called before the first frame update
	void Start() {
		this._navMeshAgent = this.GetComponent<NavMeshAgent>();
		this.transform.position = this.wayPoints[this._currentWayPointIndex].position;
		this._navMeshAgent.SetDestination(this.wayPoints[this._currentWayPointIndex].position);
		// 使用 Warp 方法同步更新 NavMeshAgent 位置
        this._navMeshAgent.Warp(transform.position);
	}

	// Update is called once per frame
	void Update() {
		if (this._navMeshAgent.remainingDistance < this._navMeshAgent.stoppingDistance) {
			this._currentWayPointIndex = (this._currentWayPointIndex + 1) % this.wayPoints.Length;
			this._navMeshAgent.SetDestination(this.wayPoints[this._currentWayPointIndex].position);
		}
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
