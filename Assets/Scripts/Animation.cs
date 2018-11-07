using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animation : MonoBehaviour {

    private Animator an; //Animator del player
    private float rot_y; //Rotazione del player sull'asse y

    private new GameObject camera;

    private bool onetime; //Idem come in followplayer.cs

    // Use this for initialization
    void Start () {
        an = GetComponent<Animator>();
        camera = GameObject.FindGameObjectWithTag("MainCamera");
        onetime = true;
	}
	
	// Update is called once per frame
	void Update () {

        //Gestione del movimento del player
        if (Input.GetAxis("Vertical")!=0 || Input.GetAxis("Horizontal")!=0) {

            //Spostamento nelle quattro direzioni
            an.SetBool("Walk", true);
            an.SetFloat("Speed", Input.GetAxis("Vertical"));
            an.SetFloat("Rotation", Input.GetAxis("Horizontal"));            
        }
        else {
            an.SetBool("Walk", false);         
        }     

        //Corsa
        if (Input.GetKey(KeyCode.LeftShift)) {
            an.SetBool("Run", true);
        }
        else {
            an.SetBool("Run", false);
        }

        //Mira
        if (Input.GetKey(KeyCode.Mouse1)) {
            an.SetBool("Aim", true);
        }
        else {
            an.SetBool("Aim", false);
        }
	}

    private void LateUpdate() {

        //Lo spostamento orizzontale del mouse fa ruotare il player, se non è in idle (fermo)
        if (an.GetCurrentAnimatorStateInfo(0).IsName("Idle") == false) {
            rot_y = (Input.GetAxis("Mouse X"));
            Vector3 temp = transform.eulerAngles;
            temp.y += rot_y;
            transform.eulerAngles = temp;
        }
    }
}
