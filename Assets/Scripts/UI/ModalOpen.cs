using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ModalOpen : MonoBehaviour
{
    public GameObject modal;
    void Start()
    {
        Button btn = GetComponent<Button>();
		btn.onClick.AddListener(ModalOpenHandler);
    }

    // Update is called once per frame
    void ModalOpenHandler(){
        modal.SetActive(true);
    }
}
