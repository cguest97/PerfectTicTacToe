using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayableSpace : MonoBehaviour {

    public Texture emptyMat, noughtMat, crossMat;
    public AudioClip play1, play2, play3;

    /* 0 = Empty, 1 = Player, 2 = AI */
    int owner = 0;
    GameObject control;
    AudioSource myAudio;

    public int GetOwner() { return owner; }

    public void Setup()
    {
        control = GameObject.FindGameObjectWithTag("GameController");
        myAudio = gameObject.GetComponent<AudioSource>();
        SetOwner(0);
    }

    /* Set the owner of this board piece 0 = unowned, 1 = player, 2 = AI */
    public void SetOwner(int player)
    {
        owner = player;
        PlaySound();
        UpdateTexture();
    }

    /* Destroy this gameobject */
    public void DestroySelf() {
        Destroy(gameObject);
    }

    /* On click change owner to player */
    private void OnMouseDown()
    {
        if (control.GetComponent<GameController>().GetCurrentPlayer() == 1 && GetOwner() == 0 && !control.GetComponent<GameController>().GameOver)
        {
            SetOwner(1);
            control.GetComponent<GameController>().NextPlayer();
        }
    }

    /* Update this spaces texture dependent on its owner */
    void UpdateTexture() {
        Renderer r = gameObject.GetComponent<Renderer>();

        switch (owner) {
            case 0:
                r.material.mainTexture = emptyMat;
                break;
            case 1:
                r.material.mainTexture = noughtMat;
                break;
            case 2:
                r.material.mainTexture = crossMat;
                break;
        }

    }

    /* Use the audio source to play a clip */
    void PlaySound() {
        int clip = Random.Range(1, 3);

        switch (clip) {
            case 1:
                myAudio.clip = play1;
                break;
            case 2:
                myAudio.clip = play2;
                break;
            case 3:
                myAudio.clip = play3;
                break;
        }

        myAudio.Play();
    } 


}
