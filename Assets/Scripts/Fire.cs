using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour {

    private GameObject origin;
    private Vector3 direction;
    public float distance;
    private GameObject camera;
    private int layer_mask=1;

	// Use this for initialization
	void Start () {
        origin = GameObject.FindGameObjectWithTag("Fire_Origin");
        camera = GameObject.FindGameObjectWithTag("MainCamera");
        direction = camera.transform.forward;

    }

    // Update is called once per frame
    void Update() {

        RaycastHit hit;
        Ray ray = camera.GetComponent<Camera>().ViewportPointToRay(new Vector3(0.5F, 0.5F, 0));

        if (Input.GetButtonDown("Fire")) {
            if (Physics.Raycast(ray, out hit, distance, layer_mask)){
                Debug.Log(hit.collider.gameObject.name + " colpito");
                if (hit.collider.gameObject.GetComponent<Rigidbody>() != null) {
                    hit.rigidbody.AddForce(hit.transform.forward * 10f, ForceMode.Impulse);
                }
            }
            else {
                Debug.Log("Non colpisco niente");
            }
        }
    }
}
