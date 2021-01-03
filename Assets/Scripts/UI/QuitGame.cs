using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class QuitGame : MonoBehaviour
{
	void Start () {
		Button btn = GetComponent<Button>();
		btn.onClick.AddListener(Quit);
	}
    public void Quit() {
        Application.Quit();
    }
}
