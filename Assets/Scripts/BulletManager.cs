using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : MonoBehaviour
{
    [SerializeField]
    public GameObject BulletPreFM;
    public GameObject BulletPreFS;
    public Rigidbody rb;
    bool canCollide = false;
    bool checkMove = true;
    float xVar;
    float yVar;
    public double startCountdown = 3;
    double deathCountdown = 3;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (this.name == "BulletS(Clone)") MoveBullet(150);
        else if(this.name == "BulletM(Clone)") MoveBullet(750);
        else MoveBullet(1500);
    }

    void FixedUpdate()
    {
        if (rb.velocity.magnitude <= 0.1f)
        {
            if (checkMove == true)
            {
                DestroyRest();
                checkMove = false;
            }
        }
        else
        {
            checkMove = true;
            deathCountdown = 3;
        }
    }

    public void DestroyRest()
    {
        if (deathCountdown > 0)
        {
            deathCountdown--;
            Invoke("DestroyRest", 1f);
        }
        else CheckSpawn();
    }

    public void NoCollideTime()
    {
        canCollide = true;

        if (startCountdown > 0)
        {
            startCountdown--;
            Invoke("NoCollideTime", 1f);
        }
        else this.tag = "Untagged";
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Respawn" && canCollide == false) NoCollideTime();

        if ((collision.gameObject.tag == "TriggerOff" || collision.gameObject.tag == "Untagged") && canCollide == true)
        {
            CheckSpawn();
        }
    }

    void CheckSpawn()
    {
        string objName = this.name;

        if (objName == "BulletS(Clone)") Destroy(this.gameObject);
        else if (objName == "BulletM(Clone)") LoopSpawn(BulletPreFS);
        else if (objName == "BulletG(Clone)") LoopSpawn(BulletPreFM);
        canCollide = false;
        Destroy(this.gameObject);
    }

    void LoopSpawn(GameObject BulletPreF)
    {
        if (BulletPreF == BulletPreFS) for (int i = 0; i < 5; i++)
            {
                Spawn(BulletPreF);
            }
        else for (int i = 0; i < 2; i++)
            {
                Spawn(BulletPreF);
            }
    }

    void Spawn(GameObject BulletPreF)
    {
        xVar = this.transform.localEulerAngles.x + Random.Range(-45, 45);
        yVar = this.transform.localEulerAngles.y + Random.Range(-45, 45);
        GameObject newBullet = Instantiate(BulletPreF, transform.position, Quaternion.Euler(xVar, yVar, this.transform.localEulerAngles.z));
    }

    void MoveBullet(int speed)
    {
        rb.AddForce(transform.forward * speed, ForceMode.Impulse);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Respawn") GetComponent<SphereCollider>().isTrigger = false;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "TriggerOff")
        {
            this.tag = "Untagged";
            GetComponent<SphereCollider>().isTrigger = false;
            canCollide = true;
        }
    }
}