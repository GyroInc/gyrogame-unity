using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuSelectionController : MonoBehaviour
{

    public float selectionCubesCenterOffset;
    [Space]
    public Transform selectionCube1;
    public Transform selectionCube2;

    [Space]
    public Transform start;
    public Transform connect;
    public Transform instructions;
    public Transform blog;
    public Transform quit;

    private Vector3 selectionCubePos;

    void Update()
    {
        selectionCube1.position = Vector3.Lerp(selectionCube1.position, selectionCubePos + (Vector3.left * selectionCubesCenterOffset), 10f * Time.deltaTime);
        selectionCube2.position = Vector3.Lerp(selectionCube2.position, selectionCubePos + (Vector3.right * selectionCubesCenterOffset), 10f * Time.deltaTime);
    }

    void Start()
    {
        SelectStart();
        selectionCube1.position = selectionCubePos + (Vector3.left * selectionCubesCenterOffset);
        selectionCube2.position = selectionCubePos + (Vector3.right * selectionCubesCenterOffset);
    }

    public void SelectStart()
    {
        selectionCubePos = start.position;
        RandomizeCube();
    }
    public void SelectConnect()
    {
        selectionCubePos = connect.position;
        RandomizeCube();
    }
    public void SelectInstructions()
    {
        selectionCubePos = instructions.position;
        RandomizeCube();
    }
    public void SelectBlog()
    {
        selectionCubePos = blog.position;
        RandomizeCube();
    }
    public void SelectQuit()
    {
        selectionCubePos = quit.position;
        RandomizeCube();
    }

    public void LoadGame()
    {
        SceneManager.LoadSceneAsync(1);
    }

    public void BuildingInstructions()
    {
        Application.OpenURL("https://gyrogame.de/2019/10/03/building-the-controller/");
    }

    public void Blog()
    {
        Application.OpenURL("https://gyrogame.de/");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    void RandomizeCube()
    {
        selectionCube1.GetComponent<RotateAndHover>().RandomizeRotation();
        selectionCube2.GetComponent<RotateAndHover>().RandomizeRotation();
    }
}
