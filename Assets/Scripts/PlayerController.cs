using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    private Rigidbody rb;
    private bool inTheGround;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
        inTheGround = true;
    }
	
	// Update is called once per frame
	void Update () {
        var x = Input.GetAxis("Horizontal") * Time.deltaTime * 190.0f;
        var z = Input.GetAxis("Vertical") * Time.deltaTime * 5.0f;

        transform.Rotate(0, x, 0);
        transform.Translate(0, 0, z);
        if (Input.GetKeyDown(KeyCode.Space) && inTheGround)
        {
            rb.AddForce(Vector3.up * 500f);
            inTheGround = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Terra" || collision.gameObject.tag == "TerraRoca")
        inTheGround = true;
    }
}
