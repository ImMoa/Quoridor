using System.Collections;
using UnityEngine;

public class Player : Pawn
{
    public override bool[,] PossibleMoves()
    {
        bool[,] r = new bool[9, 9];

        RaycastHit hit;

        if (isWhite)
        {
            if (!Physics.Raycast(GameObject.FindWithTag("White").transform.position, Vector3.forward, out hit, 1.25f))
                Move(CurrentX, CurrentY + 1, ref r); // Up
            else if (hit.collider.tag.Equals("Black"))
            {
                if (!Physics.Raycast(GameObject.FindWithTag("Black").transform.position, Vector3.forward, 1.25f))
                    Move(CurrentX, CurrentY + 2, ref r); // Up 2

                else 
                {
                    if (!Physics.Raycast(GameObject.FindWithTag("Black").transform.position, Vector3.right, 1.25f))
                        Move(CurrentX + 1, CurrentY + 1, ref r); // Go Right
                    if (!Physics.Raycast(GameObject.FindWithTag("Black").transform.position, Vector3.left, 1.25f))
                        Move(CurrentX - 1, CurrentY + 1, ref r); // Go Left
                }
            }

            if (!Physics.Raycast(GameObject.FindWithTag("White").transform.position, Vector3.back, out hit, 1.25f))
                Move(CurrentX, CurrentY - 1, ref r); // Down
            else if (hit.collider.tag.Equals("Black"))
            {
                if (!Physics.Raycast(GameObject.FindWithTag("Black").transform.position, Vector3.back, 1.25f))
                    Move(CurrentX, CurrentY - 2, ref r); // Down 2
                else 
                {
                    if (!Physics.Raycast(GameObject.FindWithTag("Black").transform.position, Vector3.right, 1.25f))
                        Move(CurrentX + 1, CurrentY - 1, ref r); // Go Right
                    if (!Physics.Raycast(GameObject.FindWithTag("Black").transform.position, Vector3.left, 1.25f))
                        Move(CurrentX - 1, CurrentY - 1, ref r); // Go Left
                }
            }
            if (!Physics.Raycast(GameObject.FindWithTag("White").transform.position, Vector3.right, out hit, 1.25f))
                Move(CurrentX + 1, CurrentY, ref r); // Right
            else if (hit.collider.tag.Equals("Black"))
            {
                if (!Physics.Raycast(GameObject.FindWithTag("Black").transform.position, Vector3.right, 1.25f))
                    Move(CurrentX + 2, CurrentY, ref r); // Right 2
                else 
                {
                    if (!Physics.Raycast(GameObject.FindWithTag("Black").transform.position, Vector3.forward, 1.25f))
                        Move(CurrentX + 1, CurrentY + 1, ref r); // Go Up
                    if (!Physics.Raycast(GameObject.FindWithTag("Black").transform.position, Vector3.back, 1.25f))
                        Move(CurrentX + 1, CurrentY - 1, ref r); // Go Down
                }
            }

            if (!Physics.Raycast(GameObject.FindWithTag("White").transform.position, Vector3.left, out hit, 1.25f))
                Move(CurrentX - 1, CurrentY, ref r); // Left
            else if (hit.collider.tag.Equals("Black"))
            {
                if (!Physics.Raycast(GameObject.FindWithTag("Black").transform.position, Vector3.left, 1.25f))
                    Move(CurrentX - 2, CurrentY, ref r); // Left 2
                else 
                {
                    if (!Physics.Raycast(GameObject.FindWithTag("Black").transform.position, Vector3.forward, 1.25f))
                        Move(CurrentX - 1, CurrentY + 1, ref r); // Go Up
                    if (!Physics.Raycast(GameObject.FindWithTag("Black").transform.position, Vector3.back, 1.25f))
                        Move(CurrentX - 1, CurrentY - 1, ref r); // Go Down
                }
            }
        }

        else
        {
            if (!Physics.Raycast(GameObject.FindWithTag("Black").transform.position, Vector3.forward, out hit, 1.25f))
                Move(CurrentX, CurrentY + 1, ref r); // Up
            else if (hit.collider.tag.Equals("White"))
            {
                if (!Physics.Raycast(GameObject.FindWithTag("White").transform.position, Vector3.forward, 1.25f))
                    Move(CurrentX, CurrentY + 2, ref r); // Up 2

                else
                {
                    if (!Physics.Raycast(GameObject.FindWithTag("White").transform.position, Vector3.right, 1.25f))
                        Move(CurrentX + 1, CurrentY + 1, ref r); // Go Right
                    if (!Physics.Raycast(GameObject.FindWithTag("White").transform.position, Vector3.left, 1.25f))
                        Move(CurrentX - 1, CurrentY + 1, ref r); // Go Left
                }
            }

            if (!Physics.Raycast(GameObject.FindWithTag("Black").transform.position, Vector3.back, out hit, 1.25f))
                Move(CurrentX, CurrentY - 1, ref r); // Down
            else if (hit.collider.tag.Equals("White"))
            {
                if (!Physics.Raycast(GameObject.FindWithTag("White").transform.position, Vector3.back, 1.25f))
                    Move(CurrentX, CurrentY - 2, ref r); // Down 2
                else
                {
                    if (!Physics.Raycast(GameObject.FindWithTag("White").transform.position, Vector3.right, 1.25f))
                        Move(CurrentX + 1, CurrentY - 1, ref r); // Go Right
                    if (!Physics.Raycast(GameObject.FindWithTag("White").transform.position, Vector3.left, 1.25f))
                        Move(CurrentX - 1, CurrentY - 1, ref r); // Go Left
                }
            }
            if (!Physics.Raycast(GameObject.FindWithTag("Black").transform.position, Vector3.right, out hit, 1.25f))
                Move(CurrentX + 1, CurrentY, ref r); // Right
            else if (hit.collider.tag.Equals("White"))
            {
                if (!Physics.Raycast(GameObject.FindWithTag("White").transform.position, Vector3.right, 1.25f))
                    Move(CurrentX + 2, CurrentY, ref r); // Right 2
                else
                {
                    if (!Physics.Raycast(GameObject.FindWithTag("White").transform.position, Vector3.forward, 1.25f))
                        Move(CurrentX + 1, CurrentY + 1, ref r); // Go Up
                    if (!Physics.Raycast(GameObject.FindWithTag("White").transform.position, Vector3.back, 1.25f))
                        Move(CurrentX + 1, CurrentY - 1, ref r); // Go Down
                }
            }

            if (!Physics.Raycast(GameObject.FindWithTag("Black").transform.position, Vector3.left, out hit, 1.25f))
                Move(CurrentX - 1, CurrentY, ref r); // Left
            else if (hit.collider.tag.Equals("White"))
            {
                if (!Physics.Raycast(GameObject.FindWithTag("White").transform.position, Vector3.left, 1.25f))
                    Move(CurrentX - 2, CurrentY, ref r); // Left 2
                else
                {
                    if (!Physics.Raycast(GameObject.FindWithTag("White").transform.position, Vector3.forward, 1.25f))
                        Move(CurrentX - 1, CurrentY + 1, ref r); // Go Up
                    if (!Physics.Raycast(GameObject.FindWithTag("White").transform.position, Vector3.back, 1.25f))
                        Move(CurrentX - 1, CurrentY - 1, ref r); // Go Down
                }
            }
        }

        return r;
    }
}