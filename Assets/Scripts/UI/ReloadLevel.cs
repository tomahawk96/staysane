using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class ReloadLevel : MonoBehaviour
{
	public string level;
	void Start () {
		Button btn = GetComponent<Button>();
		btn.onClick.AddListener(ReloadScene);
	}
    void ReloadScene(){
        Application.LoadLevel(level);
    }
}
