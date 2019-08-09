using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinTextBox : MonoBehaviour {

    string pc_win = "PC WINS!";
    string player_win = "PLAYER WINS!";
    string draw = "GAME DRAW";

    public void ChangeText(int p) {
        Text t = gameObject.GetComponent<Text>();

        switch (p) {
            case 0:
                t.text = draw;
                break;
            case 1:
                t.text = player_win;
                break;
            case 2:
                t.text = pc_win;
                break;
            case 3:
                t.text = "";
                break;
        }       
    }
}
        
    

