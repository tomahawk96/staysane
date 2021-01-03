using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ModalClose : MonoBehaviour
{
    public GameObject modal;
    void Start()
    {
        Button btn = GetComponent<Button>();
		btn.onClick.AddListener(ModalCloseHandler);
    }

    // Update is called once per frame
    void ModalCloseHandler(){
        modal.SetActive(false);
    }
}
