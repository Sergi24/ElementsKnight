using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : GeneralFunctions, IResistencia {

    private Rigidbody rb;
    private bool inTheGround;
    private GameObject AquaBallMove;
    private bool flying;
    private bool creatingFireballs;

    public float playerVelocity;
    public int handsFireTime;

    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody>();
        inTheGround = true;
        creatingFireballs = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (flying)
        {
            if (!Input.GetKey(KeyCode.Space))
            {
                if (transform.position.y < 6f) rb.AddForce(Vector3.up * 500f, ForceMode.Force);
                else rb.velocity = Vector3.zero;
            //    else if (transform.position.y < 5f) rb.AddForce(Vector3.up * 10f, ForceMode.Impulse);
            }
        }
    }

    // Update is called once per frame
    void Update() {
        float x = Input.GetAxis("Horizontal") * Time.deltaTime * 190.0f;
        float z;
        if (flying) z = Input.GetAxis("Vertical") * Time.deltaTime * (playerVelocity+3);
        else z = Input.GetAxis("Vertical") * Time.deltaTime * playerVelocity;

        transform.Rotate(0, x, 0);
        transform.Translate(0, 0, z);
        if (!flying)
        {
            if (Input.GetKeyDown(KeyCode.Space) && inTheGround)
            {
                rb.AddForce(Vector3.up * 1800f, ForceMode.Impulse);
                inTheGround = false;
            }
            rb.useGravity = true;
        } else
        {
            if (Input.GetKey(KeyCode.Space)) rb.useGravity = true;
            else rb.useGravity = false; 
        }
    }

    public void FireballRepetitivaCreada()
    {
        Invoke("HandsFireDisabled", handsFireTime);
        creatingFireballs = true;
    }

    public bool IsCreatingFireballs()
    {
        return creatingFireballs;
    }

    private void HandsFireDisabled()
    {
        gameObject.transform.Find("HandsFire").gameObject.SetActive(false);
        ParticleSystem.EmissionModule emission = gameObject.transform.Find("Fireball").gameObject.GetComponent<ParticleSystem>().emission;
        emission.rateOverTime = 0;
        creatingFireballs = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (esTerra(collision.gameObject.tag) || esRoca(collision.gameObject.tag)) inTheGround = true;
    }

    public void Destrossar(int dany)
    {
       
    }

    public void FlyTime(int flyingTime)
    {
        inTheGround = false;
        flying = true;
        Invoke("flyingTimeStop", flyingTime);
    }

    private void flyingTimeStop()
    {
        flying = false;
    }

    public void Arralentir()
    {
        if (!flying) playerVelocity -= 2f;
    }

    public void Desarralentir()
    {
        if (!flying) playerVelocity += 2f;
    }
}
