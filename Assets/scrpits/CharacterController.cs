using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterController : MonoBehaviour
{
    // Start is called before the first frame update
    GameObject cam;
    Rigidbody myRigidbody;
    bool isOnGround = false;
    public GameObject groundChecker;
    public LayerMask groundLayer;
    public float jumpForce = 3000.0f; 
    public float maxSprint = 5.0f;
    float sprintTimer;
    Animator myAnim;
    
    void Start()
    {
        
        myAnim = GetComponentInChildren<Animator>();
        cam = GameObject.Find("Main Camera");
        myRigidbody = GetComponent<Rigidbody>();
        sprintTimer = maxSprint;

    }

    // Update is called once per frame
    float rotation = 0.0f;
    public float normalSpeed = 10f;
    public float maxSpeed;
    public float sprintSpeed = 20f; 

    float camRoatation = 0.0f;
    float rotaiotionSpeed = 5.0f;
    float camRotationSpeed = 5.0f;
    

    void Update()
    {


        
        
        isOnGround = Physics.CheckSphere(groundChecker.transform.position, 0.1f, groundLayer);
        myAnim.SetBool("isOnGround", isOnGround);
        
        if (isOnGround == true && Input.GetKeyDown(KeyCode.Space))
        {
            myAnim.SetTrigger("jumped");
            myRigidbody.AddForce(transform.up* jumpForce);
              
        }
       
        
        if (Input.GetKey(KeyCode.LeftShift)&& sprintTimer > 0.0f)
        {
            maxSpeed = sprintSpeed;
            sprintTimer = sprintTimer - Time.deltaTime;
        }else
        {   
            maxSpeed = normalSpeed;
            if (Input.GetKey(KeyCode.LeftShift)==false){
                sprintTimer = sprintTimer + Time.deltaTime;
            }

        }
        sprintTimer = Mathf.Clamp(sprintTimer, 0.0f, maxSprint);

        Vector3 newVelocity = transform.forward * Input.GetAxis("Vertical") * maxSpeed;
        myAnim.SetFloat("speed", newVelocity.magnitude);
        newVelocity += transform.right * Input.GetAxis("Horizontal")* maxSpeed ;
        myRigidbody.velocity = new Vector3(newVelocity.x, myRigidbody.velocity.y, newVelocity.z);
       // transform.position = transform.position + (transform.forward * Input.GetAxis("Vertical")* maxSpeed);
        rotation = rotation + Input.GetAxis("Mouse X") * rotaiotionSpeed;
        transform.rotation = Quaternion.Euler(new Vector3(0.0f, rotation, 0.0f));
        camRoatation = camRoatation + Input.GetAxis("Mouse Y") * camRotationSpeed *-1;
        cam.transform.localRotation = Quaternion.Euler(new Vector3(camRoatation, 0.0f,0.0f));
    }

    void OnTriggerEnter (Collider other)
    {
        if (other.tag == "Scene Change")
        {
            SceneManager.LoadScene(1);
        }
    }
}