using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {

    public float speed;
    CharacterController cs;
    public Vector3 moveDirection; //vettore di spostamento del player
    public Vector3 moveRotation; //vettore di rotazione del player
    public int inverted_Y_Axis=-1; //-1=Asse y normale; 1=asse y invertito
    public float gravity = 1000f;
    public float minRotation = -30f;
    public float maxRotation = 30f;
    public Vector3 temp;

    // Use this for initialization
    void Start () {

        //Recupero il character controller dal gameobject corrente
        cs = GetComponent<CharacterController>();


    }
	
	// Update is called once per frame
	void Update () {

        //Aggiorna il vettore moveDirection in base allo spostamento dato in input, normalizzandolo e
        //Moltiplicandolo per un float (speed)
        moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical"));
        moveDirection = transform.TransformDirection(moveDirection);
        moveDirection = moveDirection * speed;

        // Apply gravity
        moveDirection.y = moveDirection.y - (gravity * Time.deltaTime);

        // Sposto il personaggio (il character controller)
        cs.Move(moveDirection * Time.deltaTime);

        

        //Calcolo la rotazione del player in base al movimento del mouse
        moveRotation = new Vector3(inverted_Y_Axis*Input.GetAxis("Mouse Y"), Input.GetAxis("Mouse X"), 0.0f);

        temp = transform.eulerAngles+moveRotation;

        if (temp.x < minRotation) {
            print("min rot");
            temp.x = minRotation;            
            //moveRotation = temp;

        }
        else if(temp.x > maxRotation) {
            temp.x = maxRotation;
            //moveRotation = temp;
        }


        transform.eulerAngles += moveRotation;








    }
}
