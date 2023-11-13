using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class LaserController : MonoBehaviour
{
    GameObject player;
    float ultTimer;
    float timeBetweenUlt = 2.5f;

    void Start()
    {
        player = GameObject.Find("Ship");
    }

    // Update is called once per frame
    void Update()
    {
        ultTimer += Time.deltaTime;
        if(ultTimer > timeBetweenUlt){      //Kod som förstör Lasern efter att Ultimate har varit igång i 2.5f
                Destroy(this.gameObject);
                player.GetComponent<ShipController>().currentUlt = player.GetComponent<ShipController>().minUlt;    //Ändrar currentUlt till minUlt
                player.GetComponent<ShipController>().UpdateUltimateSlider();   //Updaterar Ultimate slidern
                ultTimer = 0;
            }
    }
}