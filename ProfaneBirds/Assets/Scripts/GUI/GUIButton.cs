using UnityEngine;
using System.Collections;


public class GUIButton : MonoBehaviour {
	public Action onPressAction = Action.None;
	
	void OnMouseEnter() {
		Vector2 offset = renderer.material.mainTextureOffset;
		offset.y += renderer.material.mainTextureScale.y;
		renderer.material.mainTextureOffset = offset;
	}
	
	void OnMouseExit() {
		Vector2 offset = renderer.material.mainTextureOffset;
		offset.y = 0;
		renderer.material.mainTextureOffset = offset;
	}
	
	void OnMouseDown() {
		Logic.Execute(onPressAction);	
	}
}
