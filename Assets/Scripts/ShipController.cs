using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;


public class ShipController : MonoBehaviour
{
    [SerializeField]
    int maxHp = 100;
    public int currentHp;
    int minUlt = 0;
    int maxUlt = 100;
    public int currentUlt;

    [SerializeField]
    float speed = 5; // rutor per sekund

    [SerializeField]
    GameObject bulletPrefab;

    [SerializeField]
    GameObject laserPrefab;

    [SerializeField]
    Transform gunPosition;

    float shotTimer = 0;
    float timeBetweenShots = 0.25f;
    float ultTimer = 0;

    float timeBetweenUlt = 2.0f;

    [SerializeField]
    Slider healthSlider;

    [SerializeField]
    TMP_Text healthText;

    [SerializeField]
    Slider ultimateSlider;

    [SerializeField]
    TMP_Text ultimateText;
    void Awake() {
        currentHp = maxHp;
        UpdateHealthSlider();
        currentUlt = minUlt;
        UpdateUltimateSlider();
    }

    // Update is called once per frame
    void Update()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");
        Vector2 movement = new Vector2(moveX, moveY).normalized * speed * Time.deltaTime;
        transform.Translate(movement);

        //Skjutakod
        shotTimer += Time.deltaTime;
        ultTimer += Time.deltaTime;

        if (Input.GetAxisRaw("Fire1") > 0 && shotTimer > timeBetweenShots)
        {
            Instantiate(bulletPrefab, gunPosition.position, Quaternion.identity);
            shotTimer = 0;
        }
        if (Input.GetKeyDown(KeyCode.Q) && currentUlt >= maxUlt){
            GameObject ultBeam = Instantiate(laserPrefab, gunPosition.position, Quaternion.identity);
            ultBeam.transform.position += new Vector3(0, ultBeam.GetComponent<Collider2D>().bounds.size.y / 2);
            ultBeam.transform.parent = transform;
            if(ultTimer > timeBetweenUlt){
                Destroy(laserPrefab.gameObject);
                currentUlt = 0;
                ultTimer = 0;
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "enemy")
        {
            currentHp -= 10;
            UpdateHealthSlider();
        }
    }
    public void UpdateHealthSlider(){
        healthSlider.maxValue = maxHp;
        healthSlider.value = currentHp;
        healthText.text = currentHp + "/" + maxHp;
        if (currentHp <= 0){
            SceneManager.LoadScene(2);
            }
    }
    public void UpdateUltimateSlider(){
        ultimateSlider.maxValue = maxUlt;
        ultimateSlider.value = currentUlt;
        ultimateText.text = currentUlt + "/" + maxUlt;
    }
}