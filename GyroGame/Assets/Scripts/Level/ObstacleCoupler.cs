using UnityEngine;

[ExecuteInEditMode]
public class ObstacleCoupler : MonoBehaviour
{
    public RotatableObstacle parent;
    public Vector3 offset;
    
    void Start()
    {
        parent.AddCoupledObstacle(this);
    }

    void Update()
    {
        if (parent != null)
        {
            transform.rotation = Quaternion.Euler(offset) * parent.transform.rotation;
        }
    }

    internal RotatableObstacle GetParentObstacle()
    {
        return parent;
    }
}
