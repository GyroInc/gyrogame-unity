using UnityEngine;

public class RotateAndHover : MonoBehaviour
{
    public bool rotate;
    public bool randomRotation;
    public Vector3 rotateDirection;
    public float rotateSpeed;
    public bool hover;
    public float hoverHeight;
    public float hoverTime = 1;
    public AnimationCurve hoverCurve;
    public bool hoverRandomStart;

    private Vector3 basePosition;
    private float timeElapsed;
    
    private void Start()
    {
        if (randomRotation)
            rotateDirection = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f));

        if (hoverRandomStart)
            timeElapsed = Random.Range(0, hoverTime);

        basePosition = transform.position;
    }

    void Update()
    {
        //Rotation
        if (rotate)
        {
            transform.Rotate(rotateDirection * rotateSpeed * Time.deltaTime);
        }

        //Hover
        if (hover)
        {
            timeElapsed += Time.deltaTime;
            if (timeElapsed > hoverTime)
                timeElapsed -= hoverTime;

            float newY = hoverHeight * hoverCurve.Evaluate(timeElapsed / hoverTime);
            transform.position = new Vector3(transform.position.x, basePosition.y + newY, transform.position.z);
        }
    }

    public void RandomizeRotation()
    {
        rotateDirection = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f));
    }
}
