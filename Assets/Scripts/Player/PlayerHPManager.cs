using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHPManager : MonoBehaviour
{
    PlayerStats playerStats;
    Animator _anim;
    public Animator _hpAnim;
    public Image hpValue;
    public float animateTime;
    public ModalsManager modals;
    void Start(){
        playerStats = GetComponent<PlayerStats>();
        _anim = transform.Find("Body").GetComponent<Animator>();
        modals = modals.GetComponent<ModalsManager>();
    }
    public void TakeDamage(int hp){
        playerStats.hp -= hp;
        float var = hpValue.fillAmount - (0.0095f * hp);
        _anim.SetTrigger("Hit");
        _hpAnim.SetTrigger("Hit");
		StartCoroutine(DamageCoroutine(hp, var));
        Debug.Log(playerStats.hp);
    }

    public void Death(){
        _anim.SetBool("Death", true);
        _hpAnim.SetBool("Death", true);
        StartCoroutine(ModalTimeout());
        CharacterController controller = GetComponent<CharacterController>();
        controller.enabled = false;
    }

    IEnumerator DamageCoroutine(int amount, float var){
		while(hpValue.fillAmount >= var){
			yield return hpValue.fillAmount -= (0.0095f * amount) * Time.deltaTime * animateTime;
		}
	}
    IEnumerator ModalTimeout() {
		yield return new WaitForSeconds(2f);
        modals.DeathModalOpen();
	}
}
