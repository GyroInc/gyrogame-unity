using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//---------------------------------------------------------------------
//                     Written by Simon König
//---------------------------------------------------------------------



public class PlayerController : MonoBehaviour
{
    public AnimationCurve AxisPressResponse;
    public AnimationCurve AxisReleaseResponse;
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
    float tw, ts, ta, td;
    bool pw = false, ps = false, pa = false, pd = false;

    Rigidbody rb;

    void Start()
    {
        Time.fixedDeltaTime = 1f / 200f;
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

        //new Move
        //button times
        #region Button stuff
        if (Input.GetKeyDown(KeyCode.W))
            tw = 0;
        if (Input.GetKeyDown(KeyCode.A))
            ta = 0;
        if (Input.GetKeyDown(KeyCode.S))
            ts = 0;
        if (Input.GetKeyDown(KeyCode.D))
            td = 0;
        if (Input.GetKey(KeyCode.W))
        {
            tw += Time.deltaTime;
            pw = true;
        }            
        if (Input.GetKey(KeyCode.A))
        {
            ta += Time.deltaTime;
            pa = true;
        }            
        if (Input.GetKey(KeyCode.S))
        {
            ts += Time.deltaTime;
            ps = true;
        }            
        if (Input.GetKey(KeyCode.D))
        {
            td += Time.deltaTime;
            pd = true;
        }          
        if (Input.GetKeyUp(KeyCode.W))
        {
            tw = 0;
            pw = false;
        }        
        if (Input.GetKeyUp(KeyCode.A))
        {
            ta = 0;
            pa = false;
        }            
        if (Input.GetKeyUp(KeyCode.S))
        {
            ts = 0;
            ps = false;
        }            
        if (Input.GetKeyUp(KeyCode.D))
        {
            td = 0;
            pd = false;
        }
        tw += Time.deltaTime;
        ta += Time.deltaTime;
        ts += Time.deltaTime;
        td += Time.deltaTime;
        #endregion

        float velH;
        float velV;
        float valw, vals, vala, vald;

        if (pw)
            valw = AxisPressResponse.Evaluate(tw);
        else
            valw = AxisReleaseResponse.Evaluate(tw);
        if (ps)
            vals = AxisPressResponse.Evaluate(ts);
        else
            vals = AxisReleaseResponse.Evaluate(ts);
        if (pd)
            vald = AxisPressResponse.Evaluate(td);
        else
            vald = AxisReleaseResponse.Evaluate(td);
        if (pa)
            vala = AxisPressResponse.Evaluate(ta);
        else
            vala = AxisReleaseResponse.Evaluate(ta);

        velV = Mathf.Abs(valw) > Mathf.Abs(vals) ? valw : -vals;
        velH = Mathf.Abs(vald) > Mathf.Abs(vala) ? vald : -vala;

        if (rb.SweepTestAll((transform.forward * velV * movSpeedMult + transform.up * 0 + transform.right * velH * movSpeedMult).normalized, 0.3f).Length == 0 || rb.velocity.y == 0)
            rb.velocity = (transform.forward * velV * movSpeedMult) + (transform.right * velH * movSpeedMult) + (new Vector3(0, rb.velocity.y, 0));
        else
            rb.velocity = new Vector3(0, rb.velocity.y, 0);

        Debug.DrawRay(transform.position, (transform.forward * velV * movSpeedMult + transform.up * 0 + transform.right * velH * movSpeedMult).normalized * 0.3f);
            

        //jump
        if(Input.GetKeyDown(KeyCode.Space))
        {
            if(rb.velocity.y > 0)
                rb.AddForce(new Vector3(0, jumpForce - rb.velocity.y * 3, 0), ForceMode.Impulse);
            rb.AddForce(new Vector3(0, jumpForce, 0), ForceMode.Impulse);
        }
            

        //better jump cuz no one cares about physics
        if (rb.velocity.y < jumpSpeedCutoff)
            rb.velocity += Vector3.up * Physics.gravity.y * (fallMult - 1f) * Time.deltaTime;
        else if(rb.velocity.y > jumpSpeedCutoff && !Input.GetKey(KeyCode.Space))
            rb.velocity += Vector3.up * Physics.gravity.y * (lowJumpMult - 1f) * Time.deltaTime;
    }
}
