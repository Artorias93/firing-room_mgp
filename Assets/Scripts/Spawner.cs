using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField]
    GameObject BulletPreFG;
    public GameObject BulletPreFM;
    public GameObject BulletPreFS;
    GameObject BulletPreF;
    public double startCountdown = 1f;

    void Start()
    {
        Countdown();
    }

    void Countdown()                                //Funzione Timer - Spawn di 2 oggetti per volta allo scadere del countdown
    {
        if (startCountdown > 0)
        {
            startCountdown--;
            Invoke("Countdown", 0.2f);
        }
        else
        {
            int size1 = Random.Range(1, 4);                                                             //Assegnazione parametri Primo Oggetto
            Vector3 posVect1 = new Vector3(-187, 126, 50);
            Vector3 rotVect1 = new Vector3(Random.Range(-45, 45), Random.Range(45, 135), 0);

            int size2 = Random.Range(1, 4);                                                             //Assegnazione parametri Secondo Oggetto
            Vector3 posVect2 = new Vector3(87, 126, 50);
            Vector3 rotVect2 = new Vector3(Random.Range(-45, 45), Random.Range(-45, -135), 0);

            Spawn(size1, posVect1, rotVect1);                          //Chiamate a funzione che gestisce lo spawn
            Spawn(size2, posVect2, rotVect2);
            
            startCountdown = 1;
            Countdown();
        }
    }

    void Spawn(int size, Vector3 posVect, Vector3 rotVect)
    {
        if (size == 1) BulletPreF = BulletPreFS;                        //Assegnazione dimensione dell'oggetto da spawnare
        else if (size == 2) BulletPreF = BulletPreFM;
        else BulletPreF = BulletPreFG;
        Instantiate(BulletPreF, posVect, Quaternion.Euler(rotVect));
    }
}
