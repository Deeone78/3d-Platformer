using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    // Start is called before the first frame update
    GameObject cam;
    Rigidbody myRigidbody;


    void Start()
    {
        cam = GameObject.Find("Main Camera");
        myRigidbody = GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    float rotation = 0.0f;
    float maxSpeed = 10f;
    float camRoatation = 0.0f;
    float rotaiotionSpeed = 5.0f;
    float camRotationSpeed = 5.0f;

    void Update()
    {

        Vector3 newVelocity = transform.forward * Input.GetAxis("Vertical") * maxSpeed;
        newVelocity += transform.right * Input.GetAxis("Horizontal")* maxSpeed ;
        myRigidbody.velocity = new Vector3(newVelocity.x, myRigidbody.velocity.y, newVelocity.z);
       // transform.position = transform.position + (transform.forward * Input.GetAxis("Vertical")* maxSpeed);
        rotation = rotation + Input.GetAxis("Mouse X") * rotaiotionSpeed;
        transform.rotation = Quaternion.Euler(new Vector3(0.0f, rotation, 0.0f));
        camRoatation = camRoatation + Input.GetAxis("Mouse Y") * camRotationSpeed *-1;
        cam.transform.localRotation = Quaternion.Euler(new Vector3(camRoatation, 0.0f,0.0f));
    }
}
