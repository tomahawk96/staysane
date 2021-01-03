using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModalsManager : MonoBehaviour
{
    public GameObject DeathModal;

    public void DeathModalOpen(){
        DeathModal.SetActive(true);
    }
}
