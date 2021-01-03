using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using I2.TextAnimation;

public class ActionText : MonoBehaviour
{
	public GameObject textObj;
	public Text text;

	public TextAnimation _anim;

	void Start(){
		textObj.SetActive(true);
	}
    public void Visible() {
		textObj.SetActive(true);
    }
	public void SetText(string txt) {
		text.text = txt;
	}
	public void Hidden() {
		_anim.StopAllAnimations();
		_anim.PlayAnim(1);
		StartCoroutine(TextHidden());
    }

	IEnumerator TextHidden() {
		yield return new WaitForSeconds(.8f);
        textObj.SetActive(false);
	}
}
