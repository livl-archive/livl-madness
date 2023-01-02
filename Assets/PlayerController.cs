using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMotor))]
public class PlayerController : MonoBehaviour
{

    [SerializeField]
    private float speed = 3f;
    
    [SerializeField]
    private float xMouseSensitivity = 8f;
    
    [SerializeField]

    private float yMouseSensitivity = 12f;

    
    private PlayerMotor motor;

    private void Start()
    {
        motor = GetComponent<PlayerMotor>();
    }

    private void Update()
    {
        // Compute velocity of the player movement
        float xMove = Input.GetAxisRaw("Horizontal");
        float zMove = Input.GetAxisRaw("Vertical");
        
        Vector3 moveHorizontal = transform.right * xMove;
        Vector3 moveVertical = transform.forward * zMove;
        
        Vector3 velocity = (moveHorizontal + moveVertical).normalized * speed;
        
        // Apply movement
        motor.Move(velocity);
        
        // Compute rotation of the player
        float yRot = Input.GetAxisRaw("Mouse X");
        
        Vector3 rotation = new Vector3(0f, yRot, 0f) * xMouseSensitivity;

        motor.Rotate(rotation);
        
        // Compute camera rotation of the player
        float xRot = Input.GetAxisRaw("Mouse Y");
        
        Vector3 cameraRotation = new Vector3(xRot, 0f, 0f) * yMouseSensitivity;
        
        motor.RotateCamera(cameraRotation);
        
    }

}
