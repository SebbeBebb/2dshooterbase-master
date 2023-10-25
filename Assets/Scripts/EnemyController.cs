using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField]
    GameObject explosionPrefab;

    GameObject player;

    void Start()
    {
        player = GameObject.Find("Ship");

        float x = Random.Range(-5.5f, 5.5f);
        Vector2 pos = new Vector2(x, Camera.main.orthographicSize + 1);

        transform.position = pos;
    }

    void Update()
    {
        float speed = 4; // rutor per sekund
        Vector2 movement = Vector2.down * speed * Time.deltaTime;
        transform.Translate(movement);
        if (transform.position.y < -Camera.main.orthographicSize - 0.5f)
        {
            Destroy(this.gameObject);
            player.GetComponent<ShipController>().currentHp -= 10;
            player.GetComponent<ShipController>().UpdateHealthSlider();
        }
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag=="bolt" || other.gameObject.tag=="ship" || other.gameObject.tag=="laser"){
            Destroy(this.gameObject);
            GameObject explosion = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            Destroy(explosion, 0.3f);
            if(player.GetComponent<ShipController>().currentUlt != 100 && other.gameObject.tag!="laser"){
            player.GetComponent<ShipController>().currentUlt += 10;
            player.GetComponent<ShipController>().UpdateUltimateSlider();
            }
        }
    }
}
