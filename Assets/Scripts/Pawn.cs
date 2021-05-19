using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Pawn : MonoBehaviour
{

    public int CurrentX { set; get; }
    public int CurrentY { set; get; }

    public bool isWhite;

    public void SetPosition(int x, int y)
    {
        CurrentX = x;
        CurrentY = y;
    }

    public virtual bool[,] PossibleMoves()
    {
        return new bool[9, 9];
    }

    public bool Move(int x, int y, ref bool[,] r)
    {
        
        if (x >= 0 && x < 9 && y >= 0 && y < 9)
        {
            Pawn p = BoardManager.Instance.Pawns[x, y];
            if (p == null)
                r[x, y] = true;
            else
            {
                if (isWhite != p.isWhite)
                    r[x, y] = true;
                return true;
            }
        }
        AudioManager.Instance.PawnSelectEffect();
        return false;
    }
}
