using UnityEngine;
using System.Collections;

public class GizmoDrawSphere : MonoBehaviour {
	void OnDrawGizmosSelected() {
		Gizmos.DrawSphere(transform.position,0.1f);	
	}
}
