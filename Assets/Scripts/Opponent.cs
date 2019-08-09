using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Opponent {

    protected GameObject[,] board;
    protected GameObject controller;
    protected List<Vector2Int> validMoves;

    protected int ROWS, COLS;
    protected int turn;

    bool playEdge = false;

    public Opponent(GameObject[,] b, GameObject c, int r, int co, int t) {
        board = b;
        controller = c;
        ROWS = r;
        COLS = co;
        turn = t;
        validMoves = new List<Vector2Int>();
    }

    public void setTurn(int t) {
        turn = t;
    }

    public Vector2Int GetChosenMove() {
        DecideMove();

        if (validMoves.Count > 0) {
            return validMoves[Random.Range(0, validMoves.Count - 1)];
        }

        return new Vector2Int(-1, -1);
    } 

    void DecideMove() {
        /* Decide on move based on priority list */
        validMoves.Clear();

        if (turn == 2 && (IsSpaceOwned(0, 0, 1) || IsSpaceOwned(0, 2, 1) || IsSpaceOwned(2, 0, 1) || IsSpaceOwned(2, 2, 1)))
        {
            validMoves.Add(new Vector2Int(1, 1));
            playEdge = true;
        }
        else if (CanWin(2))
        {
            /* Can Computer Win */

        }
        else if (CanWin(1)) {
            /* Can human win */
        }
        else if (turn == 4 && playEdge)
        {
            AreSidesFree();
            playEdge = false;
        }
        else if (CanFork(2))
        {
            /* Can Computer Fork */
        }
        else if (CanFork(1))
        {
            /* Can Human Fork */
        }
        else if (IsSpaceOwned(1, 1, 0))
        {
            /* Play Center if free */
            validMoves.Add(new Vector2Int(1, 1));

        }
        else if (CanPlayOppositeCorner())
        {
            /* Play Opposite corner to opponent */
        }
        else if (AreCornersFree())
        {
            /* Play Empty Corner */

        }
        else if (AreSidesFree())
        {
            /* Play Empty side square */
        }

    }

    bool IsSpaceOwned(int x, int y, int p) {
        if (board[y, x].GetComponent<PlayableSpace>().GetOwner() == p)
            return true;
        return false;
    }

    bool AreCornersFree() {
        bool free = false;
        if (IsSpaceOwned(0, 0, 0)) { free = true; validMoves.Add(new Vector2Int(0, 0)); }
        if (IsSpaceOwned(2, 0, 0)) { free = true; validMoves.Add(new Vector2Int(2, 0)); }
        if (IsSpaceOwned(0, 2, 0)) { free = true; validMoves.Add(new Vector2Int(0, 2)); }
        if (IsSpaceOwned(2, 2, 0)) { free = true; validMoves.Add(new Vector2Int(2, 2)); }
        return free;
    }

    bool AreSidesFree() {
        bool free = false;
        if (IsSpaceOwned(0, 1, 0)) { free = true; validMoves.Add(new Vector2Int(0, 1)); }
        if (IsSpaceOwned(1, 0, 0)) { free = true; validMoves.Add(new Vector2Int(1, 0)); }
        if (IsSpaceOwned(2, 1, 0)) { free = true; validMoves.Add(new Vector2Int(2, 1)); }
        if (IsSpaceOwned(1, 2, 0)) { free = true; validMoves.Add(new Vector2Int(1, 2)); }
        return free;
    }

    bool CanPlayOppositeCorner() {
        bool free = false;
        if (IsSpaceOwned(0, 0, 1) && IsSpaceOwned(2, 2, 0)) { free = true; validMoves.Add(new Vector2Int(2, 2)); }
        if (IsSpaceOwned(2, 0, 1) && IsSpaceOwned(0, 2, 0)) { free = true; validMoves.Add(new Vector2Int(0, 2)); }
        if (IsSpaceOwned(0, 2, 1) && IsSpaceOwned(2, 0, 0)) { free = true; validMoves.Add(new Vector2Int(2, 0)); }
        if (IsSpaceOwned(2, 2, 1) && IsSpaceOwned(0, 0, 0)) { free = true; validMoves.Add(new Vector2Int(0, 0)); }
        return free;
    }

    bool CanFork(int player) {
        /* Check if there is a scenario where a player can create two possible ways to win */
        DetectWinner isWinner = new DetectWinner(ROWS, COLS);
        for (int i = 0; i < ROWS; i++) {
            for (int j = 0; j < COLS; j++) {
                /* Place a temporary counter at coords and then see if there are 2 ways to win the game */
                if (IsSpaceOwned(j, i, 0)) {
                    isWinner.SetBoard(board);
                    isWinner.EditSpace(j, i, player);
                    if (isWinner.HowManyWaysToWin(player) >= 2)
                    {
                        validMoves.Add(new Vector2Int(j, i));
                        return true;
                    }
                }
            }
        }

        return false;
    }

    /* Check if p can win this round */
    bool CanWin(int p) {
        DetectWinner isWinner = new DetectWinner(ROWS, COLS);
        for (int i = 0; i < ROWS; i++) {
            for (int j = 0; j < COLS; j++) {
                if (IsSpaceOwned(j, i, 0))
                {
                    isWinner.SetBoard(board);
                    isWinner.EditSpace(j, i, p);
                    if (isWinner.IsGameWon(p))
                    {
                        validMoves.Add(new Vector2Int(j, i));
                        return true;
                    }
                }
            }
        }

        return false;
    }

}
