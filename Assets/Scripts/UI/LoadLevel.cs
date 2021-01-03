using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class LoadLevel : MonoBehaviour
{
	void Start () {
		Button btn = GetComponent<Button>();
		btn.onClick.AddListener(LoadNextScene);
	}
    void LoadNextScene(){
        FadeInOut.sceneEnd = true;
    }
}
