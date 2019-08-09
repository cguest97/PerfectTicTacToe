using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    public GameObject PlayableSpacePrefab;
    public GameObject WinText;
    public float SpacePadding = 0;

    protected const int ROWS = 3;
    protected const int COLS = 3;
    protected GameObject[,] board;
    protected int currentPlayer = 1;
    protected int turn = 1;

    public bool GameOver;

    Opponent myOpp;

    public int GetCurrentPlayer() { return currentPlayer; }
    public void NextPlayer() { if (currentPlayer == 1) { currentPlayer = 2; } else { currentPlayer = 1; } CheckForWinner(); turn++; }

	// Use this for initialization
	void Start () {
        SetupBoard();
	}
	
	// Update is called once per frame
	void Update () {
        if (currentPlayer == 2 && !GameOver) {
            PlayOpponentTurn();
        }	
	}

    public void ResetBoard() {
        currentPlayer = 1;

        for (int i = 0; i < ROWS; i++) {
            for (int j = 0; j < COLS; j++) {
                board[i, j].GetComponent<PlayableSpace>().DestroySelf();
            }
        }

        SetupBoard();
    }

    void PlayOpponentTurn() {
        myOpp.setTurn(turn);
        Vector2Int move = myOpp.GetChosenMove();
        if (move.x != -1) {
            PlaySpace(move.x, move.y, currentPlayer);
        }
        NextPlayer();
    }

    void PlaySpace(int x, int y, int p) {
        board[y, x].GetComponent<PlayableSpace>().SetOwner(p);
    }

    /* Generate board for play */
    void SetupBoard() {
        board = new GameObject[ROWS, COLS];
        GameOver = false;
        WinText.GetComponent<WinTextBox>().ChangeText(3);
        for (int i = 0; i < ROWS; i++) {
            for (int j = 0; j < COLS; j++) {
                board[i, j] = Instantiate(PlayableSpacePrefab, new Vector2(i * SpacePadding, j * SpacePadding), Quaternion.identity);
                board[i, j].GetComponent<PlayableSpace>().Setup();
            }
        }
        turn = 1;
        myOpp = new Opponent(board, gameObject, ROWS, COLS, turn);
    }

    void CheckForWinner() {
        DetectWinner myWin = new DetectWinner(ROWS, COLS);
        myWin.SetBoard(board);
        if (myWin.IsGameWon(1)) { WinText.GetComponent<WinTextBox>().ChangeText(1); GameOver = true; }
        if (myWin.IsGameWon(2)) { WinText.GetComponent<WinTextBox>().ChangeText(2); GameOver = true; }
        if (myWin.IsGameDraw()) { WinText.GetComponent<WinTextBox>().ChangeText(0); GameOver = true; }
    }
}
