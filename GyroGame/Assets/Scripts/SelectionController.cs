using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionController : MonoBehaviour
{

    public float centerOffset;
    [Space]
    public Transform selectionCube1;
    public Transform selectionCube2;

    [Space]
    public Transform start;
    public Transform connect;
    public Transform instructions;
    public Transform readme;
    public Transform quit;

    void Update()
    {
        
    }

    public void RemoveSelection()
    {
        selectionCube1.position = start.position + (Vector3.up * 10);
        selectionCube2.position = start.position + (Vector3.up * 10);
    }

    public void SelectStart()
    {
        selectionCube1.position = start.position + (Vector3.left * centerOffset);
        selectionCube2.position = start.position + (Vector3.right * centerOffset);
        RandomizeCube();
    }
    public void SelectConnect()
    {
        selectionCube1.position = connect.position + (Vector3.left * centerOffset);
        selectionCube2.position = connect.position + (Vector3.right * centerOffset);
        RandomizeCube();
    }
    public void SelectInstructions()
    {
        selectionCube1.position = instructions.position + (Vector3.left * centerOffset);
        selectionCube2.position = instructions.position + (Vector3.right * centerOffset);
        RandomizeCube();
    }
    public void SelectReadme()
    {
        selectionCube1.position = readme.position + (Vector3.left * centerOffset);
        selectionCube2.position = readme.position + (Vector3.right * centerOffset);
        RandomizeCube();
    }
    public void SelectQuit()
    {
        selectionCube1.position = quit.position + (Vector3.left * centerOffset);
        selectionCube2.position = quit.position + (Vector3.right * centerOffset);
        RandomizeCube();
    }

    void RandomizeCube()
    {
        selectionCube1.GetComponent<Rotate>().Randomize();
        selectionCube2.GetComponent<Rotate>().Randomize();
    }
}
