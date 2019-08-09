using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectWinner  {

    int[,] board;
    int ROWS, COLS;
    int winLength;

    public DetectWinner(int r, int c) {
        ROWS = r;
        COLS = c;
        winLength = r;
        board = new int[r, c];
    }

    /* Based on current board state has player p won?? */
    public bool IsGameWon(int p) {
        if (CheckVerticalWin(p)) return true;
        if (CheckHorizontalWin(p)) return true;
        if (CheckDiagonalWin(p)) return true;

        return false;
    }

    public bool IsGameDraw() {
        for (int i = 0; i < ROWS; i++) {
            for (int j = 0; j < COLS; j++) {
                if (board[j, i] == 0)
                    return false;
            }
        }
        return true;
    }

    /* Check win conditions in different directions */

    bool CheckVerticalWin(int p) {
        for (int i = 0; i < COLS; i++) {
            int count = 0;
            for (int j = 0; j < winLength; j++) {
                if (board[j, i] == p) {
                    count++;
                }
            }
            if (count == winLength)
                return true;
        }

        return false;
    }

    bool CheckHorizontalWin(int p) {
        for (int i = 0; i < ROWS; i++) {
            int count = 0;
            for (int j = 0; j < winLength; j++) {
                if (board[i, j] == p) {
                    count++;
                }
            }
            if (count == winLength)
                return true;
        }

        return false;
    }

    /* Check the 2 possible diagonal wins */
    bool CheckDiagonalWin(int p) {
        int count1 = 0;
        int count2 = 0;

        for (int i = 0; i < winLength; i++) {
            if (board[i, i] == p)
                count1++;
            if (board[ROWS - i - 1, i] == p)
                count2++;
        }

        if (count1 == winLength || count2 == winLength)
            return true;
        
        return false;
    }

    /* Setup integer board from interactable gameobjects */
    public void SetBoard(GameObject[,] b) {
        for (int i = 0; i < ROWS; i++) {
            for (int j = 0; j < COLS; j++) {
                board[i, j] = b[i, j].GetComponent<PlayableSpace>().GetOwner();
            }
        }
    }

    /* Edit a space */
    public void EditSpace(int x, int y, int p) {
        board[y, x] = p;
    }

    /* Given the board how many ways are there to win */
    public int HowManyWaysToWin(int player) {
        int count = 0;

        for (int i = 0; i < ROWS; i++) {
            for (int j = 0; j < COLS; j++) {
                if (board[j, i] == 0) {
                    board[j, i] = player;
                    if (IsGameWon(player))
                        count++;
                    board[j, i] = 0;
                }
            }
        }
        return count;
    }

}
