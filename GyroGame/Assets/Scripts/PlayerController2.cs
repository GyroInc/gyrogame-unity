using UnityEngine;

public class PlayerController2 : MonoBehaviour
{
    //public AnimationCurve AxisPressResponse;
    //public AnimationCurve AxisReleaseResponse;
    public Transform pCamera;

    public float movSpeedMult = 1;
    public float lookSpeed = 3;

    [Header("Jump Vars")]
    public float jumpForce = 2.5f;
    public float fallMult = 2f;
    public float lowJumpMult = 1.5f;
    public float jumpSpeedCutoff = 0.3f;

    float roty, rotx;
    //float tw = 0, ts = 0, ta = 0, td = 0;
    //bool pw = false, ps = false, pa = false, pd = false;

    Rigidbody rb;

    void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Start()
    {
        Time.fixedDeltaTime = 1f / 200f;
        rb = GetComponent<Rigidbody>();

        //Apply initial rotation
        rotx = 360 - pCamera.localEulerAngles.x;
        roty = transform.localEulerAngles.y;

        //Apply saved LookSpeed
        lookSpeed = PlayerPrefs.GetFloat("LookSpeed", lookSpeed);
    }

    void Update()
    {
        if (EscapeMenu.active.escapeActive)
            return;

        //look around
        roty += Input.GetAxis("Mouse X") * lookSpeed;
        rotx += Input.GetAxis("Mouse Y") * lookSpeed;
        rotx = Mathf.Clamp(rotx, -90, 90);
        //Camera x rotation
        pCamera.localEulerAngles = new Vector2(-rotx, 0) ;
        //Player y rotation
        SetAbsRotOnYGravityFixed(roty);

        //Input evaluation
        float velV = Input.GetAxis("Vertical") * movSpeedMult;
        float velH = Input.GetAxis("Horizontal") * movSpeedMult;

        /*
        #region Button input stuff
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
        float valw = 0, vals = 0, vala = 0, vald = 0;

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

        velV = (Mathf.Abs(valw) > Mathf.Abs(vals) ? valw : -vals) * movSpeedMult;
        velH = (Mathf.Abs(vald) > Mathf.Abs(vala) ? vald : -vala) * movSpeedMult;
         */
        //inputs complete

        //always be relative to own body
        Vector3 input = (transform.rotation * Vector3.forward * velV) + (transform.rotation * Vector3.right * velH);
        Vector3 gravityPart = Vector3.Scale(new Vector3(Mathf.Abs(Physics.gravity.normalized.x), Mathf.Abs(Physics.gravity.normalized.y), Mathf.Abs(Physics.gravity.normalized.z)), rb.velocity);
        rb.velocity = input + gravityPart;

        /* probably wont be used and causes too many problems
        //sweep test for collision detection
        if (rb.SweepTestAll(input, 1f).Length == 0 || gravityPart.magnitude == 0)
            rb.velocity = input + gravityPart;
        else
            rb.velocity = gravityPart;
        */

        //jump        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (gravityPart.magnitude < 0.2f && gravityPart.magnitude > -0.2f)
                rb.AddForce(-Physics.gravity.normalized * jumpForce, ForceMode.Impulse);
        }

        //better jump cuz no one cares about physics
        Vector3 gravityVector = new Vector3(Physics.gravity.normalized.x, Physics.gravity.normalized.y, Physics.gravity.normalized.z);
        float gravityVelocity = Vector3.Scale(-Physics.gravity, rb.velocity).x + Vector3.Scale(-Physics.gravity, rb.velocity).y + Vector3.Scale(-Physics.gravity, rb.velocity).z;
        if (gravityVelocity < jumpSpeedCutoff)
            rb.velocity += gravityVector * (fallMult - 1f) * Time.deltaTime;
        else if (gravityVelocity > jumpSpeedCutoff && !Input.GetKey(KeyCode.Space))
            rb.velocity += gravityVector * (lowJumpMult - 1f) * Time.deltaTime;

        //debug
        //Debug.DrawRay(transform.position, (transform.forward * velV * movSpeedMult + transform.up * 0 + transform.right * velH * movSpeedMult).normalized * 0.3f);
    }

    void SetAbsRotOnYGravityFixed(float degrees)
    {
        transform.localRotation = Quaternion.FromToRotation(Vector3.up, -Physics.gravity) * Quaternion.Euler(0, degrees, 0);
    }

    public void SetLookSpeed(float speed)
    {
        lookSpeed = speed;
        PlayerPrefs.SetFloat("LookSpeed", lookSpeed);
    }

    public float GetLooksSpeed()
    {
        return lookSpeed;
    }
}
