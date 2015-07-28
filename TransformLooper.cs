using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TransformLooper : MonoBehaviour {

	public Vector3 Direction = new Vector3(1,0,0);
	public int TileCount = 1;
	public float Distance = 1;
	public float Speed = 1;

	private void Start() {
		if (TileCount > 1) {
			SpawnNextClone();
		}
		StartLoop();
	}

	private void SpawnNextClone() {
		TransformLooper instance = Instantiate(this) as TransformLooper;
		instance.transform.position = transform.position + Direction * Distance;
		instance.TileCount--;
	}

	public void StartLoop() {
		StartCoroutine(Loop());
	}

	private IEnumerator Loop () {
		var wait = new WaitForFixedUpdate();
		var startPos = transform.position;
		while (true) {
			transform.position = GetLoopLerp(startPos, Direction, Distance, Speed);
			yield return wait;
		}
	}

	public static Vector3 GetLoopLerp(Vector3 start, Vector3 direction, float length, float speed) {
		var timespan = length / speed;
		var t = Time.time % timespan / timespan;
		return Vector3.Lerp(start, start + direction * length, t);
	}

}
