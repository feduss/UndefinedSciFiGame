using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P_movement : MonoBehaviour {

    public float speed;
    private float prev_speed;
    CharacterController cs;
    private Vector3 moveDirection; //vettore di spostamento del player
    private Vector3 moveRotation; //vettore di rotazione del player
    public int inverted_Y_Axis=-1; //-1=Asse y normale; 1=asse y invertito
    public float gravity = 1000f;
    public float rot_range = 30f;
    private Vector3 temp;

    // Use this for initialization
    void Start () {

        //Recupero il character controller dal gameobject corrente
        cs = GetComponent<CharacterController>();
        prev_speed = speed;


    }
	
	// Update is called once per frame
	void Update () {
        
        //Aggiorna il vettore moveDirection in base allo spostamento dato in input e
        //Moltiplicandolo per un float (speed)
        moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical"));
        moveDirection = transform.TransformDirection(moveDirection); //Sposto il giocatore in base al suo forward
        moveDirection = moveDirection * speed;

        // Aggiungo la gravità
        moveDirection.y -= (gravity * Time.deltaTime);

        //Gestione della corsa
        if (Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.W)) {            
            if(speed!=prev_speed*2) speed *= 2;
        }
        else {
            speed = prev_speed;
        }

        // Sposto il personaggio (il character controller)
        cs.Move(moveDirection * Time.deltaTime);

        //Calcolo la rotazione del player in base al movimento del mouse
        moveRotation = new Vector3(inverted_Y_Axis*Input.GetAxis("Mouse Y"), Input.GetAxis("Mouse X"), 0.0f);

        //Calcolo la nuova rotazione del giocatore
        float new_rot_x = transform.eulerAngles.x + moveRotation.x;

        if (new_rot_x > 180) new_rot_x -= 360f;//capita che, quando x vada sotto zero, riparta da 360 e scenda...quindi normalizzo (thx silvio)

        //Vincolo la rotazione tra -rot_range e rot_range
        if (new_rot_x < -rot_range) new_rot_x = -rot_range;
        else if (new_rot_x > rot_range) new_rot_x = rot_range;

        //Assegno la nuova rotation
        transform.eulerAngles = new Vector3 (new_rot_x,transform.eulerAngles.y+moveRotation.y, 0f);

    }
}
