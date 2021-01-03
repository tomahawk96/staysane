using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
     Transform CamTransform;
     public Transform Player;
     float followspeed;
 
     void Start()
     {
         CamTransform = Camera.main.transform;
     }
     
     void Update () {
 
         Vector3 targetPosition = new Vector3(Player.position.y, CamTransform.position.x, CamTransform.position.z);
 
         CamTransform.position = Vector3.Lerp(CamTransform.position, targetPosition, Time.deltaTime * followspeed);
     }
}
