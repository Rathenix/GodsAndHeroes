using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour {

    public CellGrid CellGrid;
    private float GridHeight = 0f;
    private float GridWidth = 0f;
    public int CameraBoundary = 10;
    public float ScrollDelay = 0.5f; //eventually use this to make the mouse stay in the boundary for a period before moving
    public int ScrollBoundary = 50;
    public int ScrollSpeed = 5;

    private void Start()
    {
        GridHeight = 9;
        GridWidth = 9; //these are the number of tiles high and wide the grid is. Need to calculate it somehow
    }

    private void Update()
    {
        if (Input.mousePosition.x > Screen.width - ScrollBoundary && Input.mousePosition.x < Screen.width && transform.position.x < CellGrid.transform.position.x + CameraBoundary)
        {
            transform.position += new Vector3(ScrollSpeed * Time.deltaTime, 0, 0);
        }

        if (Input.mousePosition.x < ScrollBoundary && Input.mousePosition.x > 0 && transform.position.x > CellGrid.transform.position.x - CameraBoundary + GridWidth)
        {
            transform.position -= new Vector3(ScrollSpeed * Time.deltaTime, 0, 0);
        }

        if (Input.mousePosition.y > Screen.height - ScrollBoundary && Input.mousePosition.y < Screen.height && transform.position.y < CellGrid.transform.position.y + CameraBoundary)
        {
            transform.position += new Vector3(0, ScrollSpeed * Time.deltaTime, 0);
        }

        if (Input.mousePosition.y < ScrollBoundary && Input.mousePosition.y > 0 && transform.position.y > CellGrid.transform.position.y - CameraBoundary + GridHeight)
        {
            transform.position -= new Vector3(0, ScrollSpeed * Time.deltaTime, 0);
        }
    }
}
