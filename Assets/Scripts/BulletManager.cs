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
        if (this.name == "BulletS(Clone)") MoveBullet(150);                     //Settaggio delle velocità in relazione alla massa
        else if(this.name == "BulletM(Clone)") MoveBullet(750);
        else MoveBullet(1500);
    }

    void FixedUpdate()
    {
        if (rb.velocity.magnitude <= 5f)                  //Controllo sulla (quasi) immobilità degli oggetti
        {
            if (checkMove == true)
            {
                DestroyRest();                              //Chiamata alla funzione che scandisce il tempo di autodistruzione degli oggetti (quasi) fermi
                checkMove = false;
            }
        }
        else
        {
            checkMove = true;
            deathCountdown = 3;
        }
    }

    public void DestroyRest()                               //Funzione che scandisce il tempo di autodistruzione degli oggetti (quasi) fermi
    {
        if (deathCountdown > 0)
        {
            deathCountdown--;
            Invoke("DestroyRest", 1f);
        }
        else CheckSpawn();
    }

    public void NoCollideTime()                             //Funzione che scandisce il tempo di invulnerabilità dell'oggetto
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
        if (collision.gameObject.tag == "Respawn" && canCollide == false) NoCollideTime();              //Check sulla "fratellanza" degli oggetti che collidono e chiamata a funzione di invulnerabilità

        if ((collision.gameObject.tag == "TriggerOff" || collision.gameObject.tag == "Untagged") && canCollide == true)             //Check sulla "non fratellanza" degli oggetti
        {
            CheckSpawn();                   //Chiamata a funzione che controlla la dimensione degli oggetti che hanno colliso
        }
    }

    void CheckSpawn()                       //Funzione che controlla la dimensione degli oggetti che hanno colliso
    {
        string objName = this.name;

        if (objName == "BulletS(Clone)") Destroy(this.gameObject);
        else if (objName == "BulletM(Clone)") LoopSpawn(BulletPreFS);
        else if (objName == "BulletG(Clone)") LoopSpawn(BulletPreFM);
        canCollide = false;
        Destroy(this.gameObject);
    }

    void LoopSpawn(GameObject BulletPreF)                                   //Funzione che chiama quella di spawn un numero di volte coerente con la massa dell'oggetto distrutto
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

    void Spawn(GameObject BulletPreF)                                       //Funzione di spawn
    {
        xVar = this.transform.localEulerAngles.x + Random.Range(-45, 45);
        yVar = this.transform.localEulerAngles.y + Random.Range(-45, 45);
        GameObject newBullet = Instantiate(BulletPreF, transform.position, Quaternion.Euler(xVar, yVar, this.transform.localEulerAngles.z));
    }

    void MoveBullet(int speed)                                              //Funzione che imprime forza agli oggetti instanziati e ne genera il movimento
    {
        rb.AddForce(transform.forward * speed, ForceMode.Impulse);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Respawn") GetComponent<SphereCollider>().isTrigger = false;            //Disabilitazione del Trigger degli oggetti generati da una collisione, per permetterne altre future
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "TriggerOff")                           //Check che identifica le pareti (per attraversarle la prima volta)
        {
            this.tag = "Untagged";
            GetComponent<SphereCollider>().isTrigger = false;               //Disabilitazione del Trigger degli oggetti generati inizialmente, per permetterne altre future
            canCollide = true;
        }
    }
}