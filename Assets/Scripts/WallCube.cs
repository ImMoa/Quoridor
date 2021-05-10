using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallCube : MonoBehaviour
{
    public bool isOpen;
    public GameObject translucentWall;
    private GameObject translucentWallTemp;
    private Quaternion cubeWallH = Quaternion.Euler(90, 0, 0);
    private Quaternion cubeWallV = Quaternion.Euler(90, 0, 90);

    WallDirection wd = new WallDirection();
    BoardManager bm = new BoardManager();

    void Start()
    {
        isOpen = true;
    }

    private void OnMouseEnter()
    {
        Debug.Log(wd.wallDirection);

        if (isOpen)
        {
            if ((bm.isWhiteTurn && bm.leftWallsW > 0) || !bm.isWhiteTurn && bm.leftWallsB > 0)
            {
                if (wd.wallDirection)
                    translucentWallTemp = Instantiate(translucentWall, transform.position, bm.cubeWallOrientationH);
                else
                    translucentWallTemp = Instantiate(translucentWall, transform.position, bm.cubeWallOrientationV);
            }
        }
    }

    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(1))
        {
            wd.wallDirection = !wd.wallDirection;

            if (wd.wallDirection)
                translucentWallTemp.transform.rotation = cubeWallH;
            else
                translucentWallTemp.transform.rotation = cubeWallV;
        }
        Debug.Log("큐브 " + wd.wallDirection);
    }

    private void OnMouseExit()
    {
        Destroy(translucentWallTemp);
    }
}
