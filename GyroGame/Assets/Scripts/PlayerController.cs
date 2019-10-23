using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public AnimationCurve AxisResponse;
    public Transform pCamera;

    
    public float movSpeedMult = 1;
    public float lookSpeed = 3;

    [Header("Jump Vars")]
    public float jumpForce = 2.5f;
    public float fallMult = 2f;
    public float lowJumpMult = 1.5f;
    public float jumpSpeedCutoff = 0.3f;

    Vector2 rotation = new Vector2(0, 0);
    float roty, rotx;

    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        //look around
        roty += Input.GetAxis("Mouse X");
        rotx += Input.GetAxis("Mouse Y");
        rotx = Mathf.Clamp(rotx, -30, 30);
        pCamera.localEulerAngles = new Vector2(-rotx, 0) * lookSpeed;
        transform.localEulerAngles = new Vector3(0, roty) * lookSpeed;

        //move
        float axH = Input.GetAxis("Vertical"), axV = Input.GetAxis("Horizontal");
        //evaluate animation curves
        float velH = 0, velV = 0;
        if (axH > 0)
            velH = AxisResponse.Evaluate(axH);
        else if (axH < 0)
            velH = AxisResponse.Evaluate(-axH) * -1f;
        if (axV > 0)
            velV = AxisResponse.Evaluate(axV);
        else if (axV < 0)
            velV = AxisResponse.Evaluate(-axV) * -1f;
        rb.velocity = (transform.forward * velH * movSpeedMult) + (transform.right * velV * movSpeedMult) + (new Vector3(0,rb.velocity.y,0));

        //jump
        if(Input.GetKeyDown(KeyCode.Space))
            rb.AddForce(new Vector3(0, jumpForce, 0), ForceMode.Impulse);

        //better jump cuz no one cares about physics
        if (rb.velocity.y < jumpSpeedCutoff)
            rb.velocity += Vector3.up * Physics.gravity.y * (fallMult - 1f) * Time.deltaTime;
        else if(rb.velocity.y > jumpSpeedCutoff && !Input.GetKey(KeyCode.Space))
            rb.velocity += Vector3.up * Physics.gravity.y * (lowJumpMult - 1f) * Time.deltaTime;
    }
}
