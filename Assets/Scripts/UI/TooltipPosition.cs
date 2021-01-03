using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TooltipPosition : MonoBehaviour {
	
	private float xPosition;
	private float yPosition;

	void Update(){
		this.transform.position = new Vector2(Input.mousePosition.x + xPosition, Input.mousePosition.y + (yPosition + 50));
	}

}
