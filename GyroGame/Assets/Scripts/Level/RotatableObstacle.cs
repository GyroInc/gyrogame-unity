using System.Collections.Generic;
using UnityEngine;

public class RotatableObstacle : MonoBehaviour
{
    [ReadOnly] public bool active;
    public Color cubeColor;

    private readonly float rotationInterpolation = 8f;
    private readonly float angleSnap = 10;

    private readonly List<ObstacleCoupler> coupledObstacles = new List<ObstacleCoupler>();
    private Quaternion noCubeRotation;

    void Start()
    {
        noCubeRotation = transform.rotation;
    }

    void Update()
    {
        if (active)
        {
            Quaternion inputAngle = transform.rotation;
            if (HardwareInterface.Instance.IsConnected())
            {
                inputAngle = HardwareInterface.Instance.GetRotation();
            } else if (HardwareInterface.Instance.IsNoCubeMode())
            {

                float noCubeX = noCubeRotation.eulerAngles.x + Input.GetAxis("Mouse Y");
                float noCubeY = noCubeRotation.eulerAngles.y + Input.GetAxis("Mouse X");
                noCubeRotation = Quaternion.Euler(noCubeX, noCubeY, 0);
                inputAngle = noCubeRotation;
            }
            Quaternion outputAngle = inputAngle;
            //snap angles
            float x = Mathf.RoundToInt((inputAngle.eulerAngles.x / 360f) * 4f) * 90;
            float y = Mathf.RoundToInt((inputAngle.eulerAngles.y / 360f) * 4f) * 90;
            float z = Mathf.RoundToInt((inputAngle.eulerAngles.z / 360f) * 4f) * 90;
            Quaternion clippedAngle = Quaternion.Euler(x, y, z);
            if (Mathf.Abs(inputAngle.eulerAngles.y) % 90 > 90 - angleSnap || Mathf.Abs(inputAngle.eulerAngles.y) % 90 < angleSnap)
                outputAngle = clippedAngle;

            transform.rotation = Quaternion.Slerp(transform.rotation, outputAngle, rotationInterpolation * Time.deltaTime);
        }
    }

    public void SetActiveObstacle(bool active)
    {
        this.active = active;
        if (active)
        {
            GetComponentInChildren<Light>(true).transform.parent.gameObject.SetActive(true);
            if (coupledObstacles.Count > 0)
            {
                foreach (ObstacleCoupler obstacle in coupledObstacles)
                {
                    obstacle.gameObject.GetComponentInChildren<Light>(true).transform.parent.gameObject.SetActive(true);
                }
            }
        }
        else
        {
            GetComponentInChildren<Light>(true).transform.parent.gameObject.SetActive(false);
            if (coupledObstacles.Count > 0)
            {
                foreach (ObstacleCoupler obstacle in coupledObstacles)
                {
                    obstacle.gameObject.GetComponentInChildren<Light>(true).transform.parent.gameObject.SetActive(false);
                }
            }
        }
    }

    public void AddCoupledObstacle(ObstacleCoupler obstacle)
    {
        coupledObstacles.Add(obstacle);
    }
}


