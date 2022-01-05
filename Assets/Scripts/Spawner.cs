using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Spawner : MonoBehaviour
{
    bool fireOn = false;
    private IEnumerator countdown;
    public GameObject BulletPreFG;
    public GameObject BulletPreFM;
    public GameObject BulletPreFS;
    GameObject BulletPreF;
    float waitingTime = 0.4f;
    float waiting = 0.4f;

    void Start()
    {
        int sign = (int)Mathf.Sign(transform.position.x);
        countdown = Countdown(sign, waitingTime);
        StartCoroutine(countdown);
    }

    public void UpdateTime(float value)
    {
        waiting = waitingTime/value;
    }

    public void Fire()
    {
        fireOn = !fireOn;
    }

    private IEnumerator Countdown(int sign, float waitTime)                                //Coroutine Timer - Spawn di 1 oggetti allo scadere del countdown
    {
        while (true)
        {
            int size1 = Random.Range(1, 4);                                                             //Assegnazione parametri oggetto
            Vector3 posVect1 = new Vector3(transform.position.x + 15 * sign, 126, 50);
            Vector3 rotVect1 = new Vector3(Random.Range(-45, 45), Random.Range(-sign * 45, -sign * 135), 0);
            waitTime = waiting;

            if (fireOn) Spawn(size1, posVect1, rotVect1);                          //Chiamate a funzione che gestisce lo spawn

            yield return new WaitForSeconds(waitTime);
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
