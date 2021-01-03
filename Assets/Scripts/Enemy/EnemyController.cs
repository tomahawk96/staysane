using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public float lookRadius = 10f;
    Transform target;
    NavMeshAgent agent;
    public GameObject player;
    float attackTimer = 0f;
    PlayerStats targetStats;
    PlayerHPManager hpManager;
    Animator _anim;
    public int damage;
    public float speedAttack;
    void Start()
    {
        target = PlayerManager.instance.player.transform;
        agent = GetComponent<NavMeshAgent>();
        targetStats = player.GetComponent<PlayerStats>();
        hpManager = player.GetComponent<PlayerHPManager>();
        _anim = transform.Find("Body").GetComponent<Animator>();
    }

    void Update(){
        float distance = Vector3.Distance(target.position, transform.position);
        if(distance <= lookRadius){
            agent.SetDestination(target.position);
            if(distance <= agent.stoppingDistance){
                FaceTarget();
                if(attackTimer <= 0f) attackTimer = speedAttack;
                if(attackTimer > 0f){
                    attackTimer -= Time.deltaTime;
                    if(attackTimer <= 0f){
                        AttackTarget();
                    }
                } 
            }
            
        }
    }

    void FaceTarget(){
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3 (direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }
    void AttackTarget(){
        if(targetStats.hp >= 0){
            hpManager.TakeDamage(damage);
            _anim.SetTrigger("Hit");
        }
        else{
            hpManager.Death();
        }
    }

    void Death(){
        Debug.Log("Смерть собаке");
    }

    void OnDrawGizmosSelected(){
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }
}
