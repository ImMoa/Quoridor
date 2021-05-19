/* 더 이상 사용되지 않는 코드들 정리 필요
 * 409번 라인의 삭제 코드는 게임이 종료될 시의 UI 처리에 따라서 결정
 * ESC 키를 누를 때 게임이 바로 종료되지 않게 처리 필요
 */

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BoardManager : MonoBehaviour
{
    public static BoardManager Instance { get; set; }
    private bool[,] allowedMoves { get; set; }

    private const float TILE_SIZE = 1.0f;
    private const float TILE_OFFSET = 0.5f;

    private int selectionX = -1;
    private int selectionY = -1;
    private int count = 1;

    public float time = 0f;
    public Text result_Elapased_Time;
    public Text result_Turn_Count;
    public Text In_Game_Turn_Count;

    public int leftWallsW = 9;
    public int leftWallsB = 9;

    public List<GameObject> pawnPrefabs;
    private List<GameObject> activePawn;
    public List<GameObject> wallPrefab;
    private List<GameObject> activeWall;
    private List<GameObject> holdingWallOne;
    private List<GameObject> holdingWallTwo;
    private WallCube wallCube;
    public List<GameObject> cubeList;

    private Quaternion whiteOrientation = Quaternion.Euler(0, 270, 0);
    private Quaternion blackOrientation = Quaternion.Euler(0, 90, 0);
    private Quaternion wallOneOrientation = Quaternion.Euler(-90, 90, 0);
    private Quaternion wallTwoOrientation = Quaternion.Euler(-90, -90, 0);
    public Quaternion cubeWallOrientationH = Quaternion.Euler(90, 0, 0);
    public Quaternion cubeWallOrientationV = Quaternion.Euler(90, 0, 90);

    public Pawn[,] Pawns { get; set; }
    private Pawn selectedPawn;

    public bool isWhiteTurn = true;
    private bool cubeTemp = false;
    public bool isSelected = false;
    public bool wallDirection = true;
    public bool isPaused = false;

    public GameObject BlackesTurn,WhitesTurn;
    public GameObject Winner_Black, Winner_White;
    public GameObject EndCanvas;
    private Material previousMat;
    public Material selectedMat;

    public Camera camA, camB;

    void Start()
    {
        activeWall = new List<GameObject>();

        GameObject temp;
        Instance = this;

        temp = Resources.Load("Prefabs/wall") as GameObject;
        wallPrefab.Add(temp);

        temp = Resources.Load("Prefabs/WallCube") as GameObject;
        wallPrefab.Add(temp);

        temp = Resources.Load("Prefabs/wallOnMouse") as GameObject;
        wallPrefab.Add(temp);

        SpawnAllPawns();
    }

    void Update()
    {
        if (!isPaused)
        {
            time += Time.deltaTime;
            UpdateSelection();
            if (!isSelected)
                SetWall();

            if (Input.GetMouseButtonDown(0))
            {
                if (selectionX >= 0 && selectionY >= 0)
                {
                    if (selectedPawn == null)
                    {
                        // Select the pawn
                        SelectPawn(selectionX, selectionY);
                    }
                    else
                    {
                        // Move the pawn
                        MovePawn(selectionX, selectionY);
                    }
                }
            }
        }

        Debug.Log("업데이트 " + wallDirection);

        Debug.DrawRay(GameObject.FindWithTag("White").transform.position, Vector3.forward * 1.25f, Color.blue);
        Debug.DrawRay(GameObject.FindWithTag("White").transform.position, Vector3.back * 1.25f, Color.red);
        Debug.DrawRay(GameObject.FindWithTag("White").transform.position, Vector3.right * 1.25f, Color.yellow);
        Debug.DrawRay(GameObject.FindWithTag("White").transform.position, Vector3.left * 1.25f, Color.green);

        Debug.DrawRay(GameObject.FindWithTag("Black").transform.position, Vector3.forward * 1.25f, Color.blue);
        Debug.DrawRay(GameObject.FindWithTag("Black").transform.position, Vector3.back * 1.25f, Color.red);
        Debug.DrawRay(GameObject.FindWithTag("Black").transform.position, Vector3.right * 1.25f, Color.yellow);
        Debug.DrawRay(GameObject.FindWithTag("Black").transform.position, Vector3.left * 1.25f, Color.green);

        if (Input.GetKey("escape"))
        {
            if (isPaused)
                Time.timeScale = 1;

            SceneManager.LoadScene("MenuScene");
        }
            
    }

    private void SelectPawn(int x, int y)
    {
        if (Pawns[x, y] == null)
        {
            isSelected = false;
            return;
        }

        if (Pawns[x, y].isWhite != isWhiteTurn) return;

        bool hasAtLeastOneMove = false;

        allowedMoves = Pawns[x, y].PossibleMoves();
        for (int i = 0; i < 9; i++)
        {
            for (int j = 0; j < 9; j++)
            {
                if (allowedMoves[i, j])
                {
                    hasAtLeastOneMove = true;
                    i = 9;
                    break;
                }
            }
        }

        if (!hasAtLeastOneMove)
            return;

        selectedPawn = Pawns[x, y];
        previousMat = selectedPawn.GetComponent<MeshRenderer>().material;
        selectedMat.mainTexture = previousMat.mainTexture;
        selectedPawn.GetComponent<MeshRenderer>().material = selectedMat;
        BoardHighlights.Instance.HighLightAllowedMoves(allowedMoves);
        isSelected = true;
    }

    private void MovePawn(int x, int y)
    {
        if (allowedMoves[x, y])
        {
            Pawn p = Pawns[x, y];

            if (p != null && p.isWhite != isWhiteTurn)
            {
                EndGame();
                return;
            }

            if (y == 8 && isWhiteTurn)
            {
                EndGame();
                return;
            }

            else if (y == 0 && !isWhiteTurn)
            {
                EndGame();
                return;
            }

            Pawns[selectedPawn.CurrentX, selectedPawn.CurrentY] = null;
            selectedPawn.transform.position = GetTileCenter(x, y);
            selectedPawn.SetPosition(x, y);
            Pawns[x, y] = selectedPawn;
            AudioManager.Instance.PawnMoveEffect();

            if (isWhiteTurn)
                White_Turn_End();
            else
                Black_Turn_End();

            isWhiteTurn = !isWhiteTurn;
            GameObject.Find("InGameCanvas").GetComponent<FadeScript>().Fade();
            isSelected = false;

        }

        selectedPawn.GetComponent<MeshRenderer>().material = previousMat;
        BoardHighlights.Instance.HideHighlights();
        selectedPawn = null;
    }

    private void UpdateSelection()
    {
        if (!Camera.main) return;

        RaycastHit hit;
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 50.0f, LayerMask.GetMask("ChessPlane")))
        {
            selectionX = (int)hit.point.x;
            selectionY = (int)hit.point.z;
        }
        else
        {
            selectionX = -1;
            selectionY = -1;
            isSelected = false;
        }
    }

    private void SpawnPawn(int index, int x, int y, bool isWhite)
    {
        Vector3 position = GetTileCenter(x, y);
        GameObject go;

        if (isWhite)
        {
            go = Instantiate(pawnPrefabs[index], position, whiteOrientation) as GameObject;
        }
        else
        {
            go = Instantiate(pawnPrefabs[index], position, blackOrientation) as GameObject;
        }

        go.transform.SetParent(transform);
        Pawns[x, y] = go.GetComponent<Pawn>();
        Debug.Log(x + " " + y);
        Pawns[x, y].SetPosition(x, y);
        activePawn.Add(go);
    }

    public void ChangeCamView()
    {

        //GameObject.Find("InGameCanvas").GetComponent<FadeScript>().Fade();

        if (isWhiteTurn)
        {
            camA.gameObject.SetActive(true);
            camB.gameObject.SetActive(false);
        }
        else
        {
            camA.gameObject.SetActive(false);
            camB.gameObject.SetActive(true);
        }
    }

    private void SpawnWall(Vector3 pos, bool isWhite)
    {

        Vector3 position = pos;
        Vector3 cubePos;
        GameObject go;

        cubePos.x = 1.0f;
        cubePos.y = 0;
        cubePos.z = 1.0f;

        if (!cubeTemp)
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    go = Instantiate(wallPrefab[1], cubePos, cubeWallOrientationH) as GameObject;
                    go.transform.SetParent(transform);
                    cubePos.x += 1.0f;
                    cubeList.Add(go);
                }

                cubePos.x = 1.0f;
                cubePos.z += 1.0f;
            }

            cubeTemp = !cubeTemp;
        }

        if (isWhite)
        {
            for (int i = 0; i < 10; i++)
            {
                go = Instantiate(wallPrefab[0], position, wallOneOrientation) as GameObject;
                go.transform.SetParent(transform);
                position.x += 1.0f;
                holdingWallOne.Add(go);
            }

            Debug.Log(holdingWallOne.Count);
        }
        else
        {
            for (int i = 0; i < 10; i++)
            {
                go = Instantiate(wallPrefab[0], position, wallTwoOrientation) as GameObject;
                go.transform.SetParent(transform);
                position.x -= 1.0f;
                holdingWallTwo.Add(go);
            }

            Debug.Log(holdingWallTwo.Count);
        }
    }

    private void SetWall()
    {
        if (!Camera.main) return;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        RaycastHit hitTemp;

        Debug.DrawRay(ray.origin, ray.direction * 50.0f);
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 50.0f) && hit.collider.name.Equals("WallCube(Clone)"))
        {
            wallCube = hit.transform.gameObject.GetComponent<WallCube>();
            if (wallCube.isOpen)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    if (hit.collider.tag == "WallCube")
                    {
                        if (isWhiteTurn && leftWallsW >= 0)
                        {
                            Debug.Log(Physics.Raycast(hit.transform.position, Vector3.left, out hitTemp, 1.0f));
                            Debug.Log(hit.collider.name);
                            Debug.Log(Physics.Raycast(hit.transform.position, Vector3.right, out hitTemp, 1.0f));
                            Debug.Log(hit.collider.name);

                            if (wallDirection
                                && (!Physics.Raycast(hit.transform.position, Vector3.left, 0.4f)
                                && !Physics.Raycast(hit.transform.position, Vector3.right, 0.4f)))
                            {
                                SetWallH(hit);
                                AfterSetWall();
                            }

                            else if (!wallDirection
                                && (!Physics.Raycast(hit.transform.position, Vector3.forward, 0.4f)
                                && !Physics.Raycast(hit.transform.position, Vector3.back, 0.4f)))
                            {
                                SetWallV(hit);
                                AfterSetWall();
                            }
                        }

                        else if (!isWhiteTurn && leftWallsB >= 0)
                        {
                            if (wallDirection
                                && (!Physics.Raycast(hit.transform.position, Vector3.left, 0.4f)
                                && !Physics.Raycast(hit.transform.position, Vector3.right, 0.4f)))
                            {
                                SetWallH(hit);
                                AfterSetWall();
                            }

                            else if (!wallDirection
                                && (!Physics.Raycast(hit.transform.position, Vector3.forward, 0.4f)
                                && !Physics.Raycast(hit.transform.position, Vector3.back, 0.4f)))
                            {
                                SetWallV(hit);
                                AfterSetWall();
                            }

                        }
                    }
                }

                else if (Input.GetMouseButtonDown(1))
                {
                    if (hit.collider.tag == "WallCube")
                    {
                        wallDirection = !wallDirection;
                        Debug.Log(wallDirection);
                    }
                }
            }
        }
    }

    private void SetWallH(RaycastHit hit)
    {
        GameObject temp;

        temp = Instantiate(wallPrefab[0], hit.transform.position, cubeWallOrientationH);
        activeWall.Add(temp);
    }

    private void SetWallV(RaycastHit hit)
    {
        GameObject temp;

        temp = Instantiate(wallPrefab[0], hit.transform.position, cubeWallOrientationV);
        activeWall.Add(temp);
    }

    private void AfterSetWall()
    {
        if (isWhiteTurn)
        {
            Destroy(holdingWallOne[leftWallsW]);
            holdingWallOne.RemoveAt(holdingWallOne.Count - 1);
            leftWallsW--;
            isWhiteTurn = !isWhiteTurn;
            wallCube.isOpen = !wallCube.isOpen;
            wallDirection = true;
            Debug.Log("White");
            GameObject.Find("InGameCanvas").GetComponent<FadeScript>().Fade();
            White_Turn_End();
        }

        else
        {
            Destroy(holdingWallTwo[leftWallsB]);
            holdingWallTwo.RemoveAt(holdingWallTwo.Count - 1);
            leftWallsB--;
            isWhiteTurn = !isWhiteTurn;
            wallCube.isOpen = !wallCube.isOpen;
            wallDirection = true;
            Debug.Log("Black");
            GameObject.Find("InGameCanvas").GetComponent<FadeScript>().Fade();
            Black_Turn_End();
        }
        AudioManager.Instance.SpawnWallEffect();
    }

    private void White_Turn_End()
    {
        count++;
        In_Game_Turn_Count.text = count.ToString();
        WhitesTurn.SetActive(false);
        BlackesTurn.SetActive(true);
    }

    private void Black_Turn_End()
    {
        count++;
        In_Game_Turn_Count.text = count.ToString();
        WhitesTurn.SetActive(true);
        BlackesTurn.SetActive(false);
    }

    private Vector3 GetTileCenter(int x, int y)
    {
        Vector3 origin = Vector3.zero;
        origin.x += (TILE_SIZE * x) + TILE_OFFSET;
        origin.z += (TILE_SIZE * y) + TILE_OFFSET;

        return origin;
    }

    private void SpawnAllPawns()
    {
        activePawn = new List<GameObject>();
        Pawns = new Pawn[9, 9];

        holdingWallOne = new List<GameObject>();
        holdingWallTwo = new List<GameObject>();

        Vector3 positionWhite, positionBlack;

        positionWhite.x = 0f;
        positionWhite.y = 0;
        positionWhite.z = -1.0f;

        positionBlack.x = 9.0f;
        positionBlack.y = 0;
        positionBlack.z = 10.0f;

        /////// White ///////
        SpawnPawn(0, 4, 0, true);
        SpawnWall(positionWhite, true);


        /////// Black ///////
        SpawnPawn(1, 4, 8, false);
        SpawnWall(positionBlack, false);
    }

    private void EndGame()
    {
        result_Elapased_Time.text = Mathf.Round(time)+" (s)";
        result_Turn_Count.text = count + " turns";

        AudioManager.Instance.EndGameEffect();
        if (isWhiteTurn)
        {
            Winner_White.SetActive(true);
            Winner_Black.SetActive(false);
        }
        else
        {
            Winner_White.SetActive(false);
            Winner_Black.SetActive(true);
        }
            

        // 게임이 종료될 때 오브젝트를 삭제 후 원위치 시킬지 결정 필요
        foreach (GameObject go in activePawn)
            Destroy(go);

        foreach (GameObject go in activeWall)
            Destroy(go);

        isWhiteTurn = true;
        BoardHighlights.Instance.HideHighlights();
        SpawnAllPawns();
        EndCanvas.SetActive(true);
    }
}