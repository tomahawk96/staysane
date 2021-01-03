using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Steps : MonoBehaviour
{
    public AudioSource audioS;
    public AudioClip[]
        footsteps_asphalt;
    public void PlaySteps(int f){
        audioS.clip = footsteps_asphalt[f];
        audioS.Play();
    }
}