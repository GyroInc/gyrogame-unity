using UnityEngine;

public class PlayerController2 : MonoBehaviour
{
    public Transform pCamera;

    public float movSpeedMult = 1;
    public float lookSpeed = 3;

    [Header("Jump Vars")]
    public float jumpForce = 2.5f;
    public float fallMult = 2f;
    public float lowJumpMult = 1.5f;
    public float jumpSpeedCutoff = 0.3f;

    float roty, rotx;
    Rigidbody rb;
    private bool canLookAround = true;

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
        if (EscapeMenu.Instance.escapeActive)
            return;

        //look around
        if (canLookAround)
        {
            roty += Input.GetAxis("Mouse X") * lookSpeed;
            rotx += Input.GetAxis("Mouse Y") * lookSpeed;
            rotx = Mathf.Clamp(rotx, -90, 90);
            //Camera x rotation
            pCamera.localEulerAngles = new Vector2(-rotx, 0);
            //Player y rotation
            SetAbsRotOnYGravityFixed(roty);
        }

        //Input evaluation
        float velV = Input.GetAxis("Vertical") * movSpeedMult;
        float velH = Input.GetAxis("Horizontal") * movSpeedMult;

        //always be relative to own body
        Vector3 input = (transform.rotation * Vector3.forward * velV) + (transform.rotation * Vector3.right * velH);
        Vector3 gravityPart = Vector3.Scale(new Vector3(Mathf.Abs(Physics.gravity.normalized.x), Mathf.Abs(Physics.gravity.normalized.y), Mathf.Abs(Physics.gravity.normalized.z)), rb.velocity);
        rb.velocity = input + gravityPart;

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

    public void SetCanLookAround(bool look)
    {
        canLookAround = look;
    }
}
