using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallCube : MonoBehaviour
{
    public bool isOpen;
    private bool isSelected;
    private bool isPaused;
    private bool direction;

    public GameObject translucentWall;
    private GameObject translucentWallTemp;

    private Quaternion cubeWallH = Quaternion.Euler(90, 0, 0);
    private Quaternion cubeWallV = Quaternion.Euler(90, 0, 90);

    BoardManager bm = new BoardManager();

    void Start()
    {
        isOpen = true;
    }

    void Update()
    {
        isSelected = GameObject.Find("ChessBoard").GetComponent<BoardManager>().isSelected;
        isPaused = GameObject.Find("ChessBoard").GetComponent<BoardManager>().isPaused;
        direction = GameObject.Find("ChessBoard").GetComponent<BoardManager>().wallDirection;
    }

    private void OnMouseEnter()
    {
        Debug.Log(direction);

        if (!isSelected && !isPaused)
        {
            if (isOpen)
            {
                if ((bm.isWhiteTurn && bm.leftWallsW > 0) || !bm.isWhiteTurn && bm.leftWallsB > 0)
                {
                    if (direction
                                    && (!Physics.Raycast(this.transform.position, Vector3.left, 0.4f)
                                    && !Physics.Raycast(this.transform.position, Vector3.right, 0.4f)))
                        translucentWallTemp = Instantiate(translucentWall, transform.position, bm.cubeWallOrientationH);

                    else if (!direction
                                    && (!Physics.Raycast(this.transform.position, Vector3.forward, 0.4f)
                                    && !Physics.Raycast(this.transform.position, Vector3.back, 0.4f)))
                        translucentWallTemp = Instantiate(translucentWall, transform.position, bm.cubeWallOrientationV);
                }
            }
        }
    }

    private void OnMouseOver()
    {
        if (!isSelected && !isPaused)
        {
            if (Input.GetMouseButtonDown(1))
            {
                direction = !direction;

                if (direction)
                    translucentWallTemp.transform.rotation = cubeWallH;
                else
                    translucentWallTemp.transform.rotation = cubeWallV;
            }
            Debug.Log("큐브 " + direction);
        }
    }

    private void OnMouseExit()
    {
        if(!isSelected && !isPaused)
            Destroy(translucentWallTemp);
    }
}