using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelPortal : MonoBehaviour
{
    public string level;
    public string portalText;
    public ActionText actionText;
    
    void OnTriggerEnter(Collider other){
        actionText.SetText(portalText);
        actionText.Visible();
    }

    void OnTriggerExit(Collider other){
        actionText.Hidden();
    }
}
