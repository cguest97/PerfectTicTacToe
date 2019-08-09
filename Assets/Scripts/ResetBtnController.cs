using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetBtnController : MonoBehaviour {

    public void OnButtonPress()
    {
        GameObject g = GameObject.FindGameObjectWithTag("GameController");
        g.GetComponent<GameController>().ResetBoard();
    }
}
