using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class VersionNo : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Text>().text = Application.version;   
    }
}
