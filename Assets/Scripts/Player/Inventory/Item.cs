using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Item : MonoBehaviour {
	public string title;
	public int id;
	[HideInInspector]
	public int count;
	public bool isStackable;

	[Multiline(5)]
	public string description;
	public Sprite icon;
	public bool isRemovable;
	public bool isDroped;
	public UnityEvent customEvent;
}
