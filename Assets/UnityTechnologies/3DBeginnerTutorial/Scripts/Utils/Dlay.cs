using System;
using System.Collections;
using UnityEngine;

public class Delay : MonoBehaviour {
	public static IEnumerator Do(Action action, float delaySeconds) {
		yield return new WaitForSeconds(delaySeconds);
		action();
	}
}