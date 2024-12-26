using System.Runtime.CompilerServices;
using UnityEngine;

public class Player : MonoBehaviour
{

    private CharacterController cc;
    private CameraLook cam;
    [Space]
    [Space]
    [SerializeField] private float CrouchSpeed = 2f;
    [SerializeField] private float WalkSpeed = 4f;
    [SerializeField] private float RunSpeed = 7f;
    [SerializeField] private float JumpForce = 5.5f;
    [Space]
    [SerializeField] private float CrouchTransitionSpeed = 5f;


    [SerializeField] private float Gravity = -7f;

    private float gravityAcceleration;
    private float yVelocity;



    void Start()
    {
        cc = GetComponent<CharacterController>();
        cam = GetComponentInChildren<CameraLook>();

        gravityAcceleration = Gravity * Gravity;

        gravityAcceleration *= Time.deltaTime;
    }

  
    void FixedUpdate()
    {
        Movement();
    }

    private void Movement()
    {
        Vector3 moveDir = Vector3.zero;

        if (Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S))
            moveDir.z += 1;
        if (Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.W))
            moveDir.z -= 1;
        if (Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.A))
            moveDir.x += 1;
        if (Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
            moveDir.x -= 1;

        if (Input.GetKey(KeyCode.LeftShift))
        {
            // Lerp calismazsa kaldir
            moveDir *= RunSpeed;

            cam.transform.localPosition = Vector3.Lerp(cam.transform.localPosition, new Vector3(0, 2, 0), CrouchTransitionSpeed * Time.deltaTime);
            cc.height = Mathf.Lerp(cc.height, 2, CrouchTransitionSpeed * Time.deltaTime);
            cc.center = Vector3.Lerp(cc.center, new Vector3(0, 1, 0), CrouchTransitionSpeed * Time.deltaTime);
        }


        else if (Input.GetKey(KeyCode.LeftControl) && !Input.GetKey(KeyCode.LeftShift))
        {
            moveDir *= CrouchSpeed;    // 200 iq

            cam.transform.localPosition = Vector3.Lerp(cam.transform.localPosition, new Vector3(0, 1, 0), CrouchTransitionSpeed * Time.deltaTime);
            cc.height = Mathf.Lerp(cc.height, 1.2f, CrouchTransitionSpeed * Time.deltaTime);
            cc.center = Vector3.Lerp(cc.center, new Vector3(0, 0.59f, 0), CrouchTransitionSpeed * Time.deltaTime);
        }
            
        else
        {
            moveDir *= WalkSpeed;

            cam.transform.localPosition = Vector3.Lerp(cam.transform.localPosition, new Vector3(0, 2, 0), CrouchTransitionSpeed * Time.deltaTime);
            cc.height = Mathf.Lerp(cc.height, 2, CrouchTransitionSpeed * Time.deltaTime);
            cc.center = Vector3.Lerp(cc.center, new Vector3(0, 1, 0), CrouchTransitionSpeed * Time.deltaTime);
        }
            

        if (cc.isGrounded)
        {
            yVelocity = -0.5f;

            if (Input.GetKey(KeyCode.Space))
            {
                yVelocity = JumpForce;
            }
        }
        else
            yVelocity -= gravityAcceleration;
        
        moveDir.y = yVelocity;

        moveDir = transform.TransformDirection(moveDir);
        moveDir *= Time.deltaTime;

        cc.Move(moveDir);

    }
}
