using UnityEngine;
using System.Collections;

public class ResetPosition : MonoBehaviour {
	public float threshold;
	public Transform spawnPoint;

	void FixedUpdate () {
		if (transform.position.y < threshold)
			transform.position = spawnPoint.position;
	}
}