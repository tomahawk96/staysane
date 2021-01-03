using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TooltipObject : MonoBehaviour {
	
	public string objectName;
	public GameObject TooltipWindow;
	public Text DisplayName;

	void OnMouseEnter(){
		TooltipWindow.SetActive(true);

		if(TooltipWindow != null){
			DisplayName.text = objectName;
		}
	}

	void OnMouseExit(){
		TooltipWindow.SetActive(false);
	}

}
