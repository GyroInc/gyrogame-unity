using TMPro;
using UnityEngine;

public class DialogTextController : MonoBehaviour
{
    private GameObject pCamera;
    private SphereCollider trigger;

    // Start is called before the first frame update
    void Start()
    {
        pCamera = GameObject.FindGameObjectWithTag("MainCamera");
        trigger = GetComponent<SphereCollider>();
        SetTextOpacity(0f);
    }

    // Update is called once per frame
    void Update()
    {
        //float yRot = Mathf.SmoothDamp(transform.localEulerAngles.y, pCamera.transform.localEulerAngles.y, ref rotationFollowVelocity, 0.3f);
        Vector3 dir = (transform.position - pCamera.transform.position).normalized;
        //transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(dir), rotationFollowSpeed * Time.deltaTime);
        transform.rotation = Quaternion.LookRotation(dir);
        transform.localEulerAngles = new Vector3(0, transform.localEulerAngles.y, 0);
    }

    void OnTriggerStay(Collider other)
    {
        if (other.tag != "Player") return;

        float distance = Vector3.Distance(transform.position + trigger.center, pCamera.transform.position);

        float fadeArea = 0.33f * trigger.radius;
        //fadeArea = 4;
        if (fadeArea > 4) fadeArea = 4;

        if (distance < fadeArea)
            SetTextOpacity((distance - 1) / (fadeArea - 1)); 

        else if(distance > trigger.radius - fadeArea)
            SetTextOpacity((trigger.radius - distance) / fadeArea);

        else
            SetTextOpacity(1f);
    }

    void OnTriggerLeave(Collider other)
    {
        if (other.tag != "Player") return;

        SetTextOpacity(0f);
    }

    void SetTextOpacity(float opacity)
    {
        foreach (TextMeshPro tmp in GetComponentsInChildren<TextMeshPro>())
        {
            Color newOpacity = tmp.color;
            newOpacity.a = opacity;
            tmp.color = newOpacity;
        }
    }
}
