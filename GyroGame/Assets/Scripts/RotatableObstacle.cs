using System.Collections.Generic;
using UnityEngine;

public class RotatableObstacle : MonoBehaviour
{
    [ReadOnly] public bool active = false;
    public Color cubeColor;

    private float rotationInterpolation = 8f;
    private float angleSnap = 10;

    private List<ObstacleCoupler> coupledObstacles;

    // Start is called before the first frame update
    void Start()
    {
        coupledObstacles = new List<ObstacleCoupler>();
    }

    // Update is called once per frame
    void Update()
    {
        if (active)
        {
            if (HardwareInterface.Instance.IsConnected())
            {
                //if(!offsetSet)
                //{
                //    offset = selected.transform.rotation * Quaternion.Euler(Vector3.right);
                //    offsetSet = true;
                //}

                Quaternion inputAngle = HardwareInterface.Instance.GetRotation(); // * offset
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


