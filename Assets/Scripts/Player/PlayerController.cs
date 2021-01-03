
using UnityEngine;
using System.Collections;
using UnityEngine.AI;
public class PlayerController : MonoBehaviour {

    public float walkSpeed = 6.0F;
    private float speed = 0.0F;
    public float jumpValue = 8.0F;
    public float gravity = 20.0F;
    public KeyCode JumpButton;
    public KeyCode KneelingButton;
    private Vector3 moveDirection = Vector3.zero;
    public GameObject player;
    public CharacterController controller;
    Animator _anim;
    void Start () {
	    _anim = player.transform.Find("Body").GetComponent<Animator>();
	}
    
    void Update()
    {
        controller = GetComponent<CharacterController>();
        float horizontal = Input.GetAxis("Horizontal");
        if (controller.isGrounded)
        {
            moveDirection = new Vector3(-horizontal, 0, 0);
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection *= walkSpeed;
            Vector3 forward = transform.TransformDirection(Vector3.forward);
            controller.SimpleMove(forward * speed);
            if (Input.GetKeyDown(JumpButton)){
                moveDirection.y = jumpValue;
                _anim.SetTrigger("Jump");
            }
            if(Input.GetKeyDown(KneelingButton)){
                _anim.SetBool("Kneeling", true);
            }
            if(Input.GetKeyUp(KneelingButton)){
                _anim.SetBool("Kneeling", false);
            }        
        }
        moveDirection.y -= gravity * Time.deltaTime;
        controller.Move(moveDirection * Time.deltaTime);
        if(controller.velocity == Vector3.zero && Input.GetAxisRaw("Horizontal") == 0){
			_anim.SetBool("stay", true);
		}
        if(controller.velocity != Vector3.zero && Input.GetAxisRaw("Horizontal") != 0){
            _anim.SetBool("stay", false);
            if(moveDirection.x > 0){
                player.transform.Find("Body").transform.rotation = Quaternion.Euler(0, 90, 0);
            }
            else{
                player.transform.Find("Body").transform.rotation = Quaternion.Euler(0, -90, 0);
            }
        }
    }
}
