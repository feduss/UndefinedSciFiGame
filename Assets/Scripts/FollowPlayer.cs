using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour {

    //Il player e il suo collider
    private GameObject player;
    private CapsuleCollider player_cc;

    private Vector3 distance_pos; //Distanza della camera dal player
    private float rot_y; //Rotazione della camera intorno al player

    //Flag per eseguire certe istruzioni in maniera molto controllata (una sola volta in certe circostanze)
    private bool onetime = true;
    private bool onemoretime = true;

    //Transform della camera, salvato prima della prima rotazione intorno al player (gioco di parole :D)
    Vector3 camera_pos;
    Vector3 camera_rot;

    // Use thisdistance_pos for initialization
    void Start () {

        player = GameObject.FindGameObjectWithTag("Player");
        player_cc = player.GetComponent<CapsuleCollider>();
        distance_pos = transform.position - player_cc.transform.position;
    }
    

    // Update is called once per frame
    void LateUpdate () {

        print("onetime: " + onetime + " |: " + "onemoretime:" + onemoretime + " | Camerapos: "+ camera_pos + " | Camerarot: " + camera_rot +
              "| Distance: " + distance_pos + " | Current distance: " + (transform.position - player_cc.transform.position));



        
        //Quando il player è fermo, posso ruotare la camera intorno a lui spostando orizzontalmente il mouse
        if (player_cc.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Idle") && player_cc.GetComponent<Animator>().GetBool("Aim")==false) {

            //Salvo la posizione della camera prima della sua rotazione intorno al player...lo faccio una sola volta perchè dovrò utilizzarla per rimetterla in
            //posizione, una volta che il player si sarà mosso
            if (onemoretime && rot_y==0) {
                camera_pos = transform.position;
                camera_rot = transform.eulerAngles;
                onemoretime = false;
            }

            //Rotazione della camera intorno al player
            rot_y = Input.GetAxis("Mouse X");
            transform.RotateAround(player_cc.transform.position, Vector3.up, rot_y * 100 * Time.deltaTime);
            onetime = true;

        }
        else {
            
            //Ristabilisco la posizione originale della camera quando il giocatore si sposta
            if (onetime) {
                transform.position = camera_pos;
                transform.eulerAngles = camera_rot;
                onetime = false;
                onemoretime = true;
            }
            else {

                //La camera segue lo spostamento del giocatore, in base ad una distanza definita nella start
                transform.position = player_cc.transform.position + distance_pos;

                //TODO LA CAMERA DEVE SEGUIRE LA ROTAZIONE DEL PLAYER IN MOVIMENTO WIP
                //transform.eulerAngles = new Vector3(transform.eulerAngles.x, player_cc.transform.eulerAngles.y, transform.eulerAngles.z);



            }

        }
            

        
	}
}
