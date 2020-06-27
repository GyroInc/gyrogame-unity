using UnityEngine;

public class ObstacleMover : MonoBehaviour
{
    public bool playerCanRotateObstacles;
    public float selectionRange;
    public Color standbyCubeColor;
    public int cubeColorFadeTime;

    private RotatableObstacle selected;

    void Update()
    {
        if (EscapeMenu.Instance.escapeActive || !playerCanRotateObstacles) return;
        //select on click
        if (Input.GetMouseButtonDown(0))
        {
            DeselectObstacle();

            RaycastHit hit;
            Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
            if (Physics.Raycast(ray, out hit, selectionRange, LayerMask.GetMask("Rotatable")))
            {
                selected = hit.transform.parent.gameObject.GetComponent<RotatableObstacle>();
                if (selected == null)
                {
                    selected = hit.transform.parent.gameObject.GetComponent<ObstacleCoupler>().GetParentObstacle();
                }
                selected.SetActiveObstacle(true);
                if (HardwareInterface.Instance.IsNoCubeMode())
                {
                    GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController2>().SetCanLookAround(false);
                }

                //color stuff
                HardwareInterface.Instance.FadeAllLeds(new CubeColor(selected.cubeColor), cubeColorFadeTime);
            }
            else
            {
                HardwareInterface.Instance.FadeAllLeds(new CubeColor(standbyCubeColor), cubeColorFadeTime);
            }
        }
        else if (Input.GetMouseButtonDown(1))
        {
            DeselectObstacle();
            HardwareInterface.Instance.FadeAllLeds(new CubeColor(standbyCubeColor), cubeColorFadeTime);
        }
    }

    private void DeselectObstacle()
    {
        //Deactivate old object
        if (selected != null)
        {
            selected.SetActiveObstacle(false);
        }
        if (HardwareInterface.Instance.IsNoCubeMode())
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController2>().SetCanLookAround(true);
        }
    }

    public void SetPlayerCanRotateObstacle(bool b)
    {
        playerCanRotateObstacles = b;
    }
}
