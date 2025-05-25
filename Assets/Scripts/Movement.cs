using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Random = UnityEngine.Random;

public class Movement : MonoBehaviour
{
    private static readonly int Walking = Animator.StringToHash("Walking");
    public float movementSpeed;
    private float hoz;
    private float ver;
    public float sunburnNum;
    public Slider sunburnSlider;
    public bool speedBoost;
    public bool burnReduction;
    public float burnRedNum;
    public bool isFinished = false;
    public Animator animator;
    public float speedTimer;
    public float redTimer;
    public TimerManager timerManager;
    public float finishTime;
    public AudioClip[] sfx;
    public AudioSource audioSource;
    public GameObject bestTimeGO;
    public TMP_Text bestText;
    public float bestTime;
    // Start is called before the first frame update
    void Start()
    {
        bestTime = PlayerPrefs.GetFloat("BestTime");
        bestText = bestTimeGO.GetComponent<TMP_Text>();
        bestText.text = PlayerPrefs.GetFloat("BestTime").ToString();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //movement
        hoz = Input.GetAxisRaw("Horizontal");
        ver = Input.GetAxisRaw("Vertical");
        Vector2 movement = new Vector2(hoz * movementSpeed * Time.deltaTime, ver * movementSpeed * Time.deltaTime);
        transform.Translate(movement * (movementSpeed * Time.deltaTime), Space.World);
        //rotate towards movement
        if (movement != Vector2.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(Vector3.forward, movement);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, 300f * Time.deltaTime);
        }
        sunburnSlider.value = sunburnNum;

        //animation
        if (hoz != 0 || ver != 0)
        {
            animator.SetBool(Walking, true);
            int walkSFX = Random.Range(4,5);
            audioSource.clip = sfx[walkSFX];
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }
        else
        {
            animator.SetBool(Walking, false);
        }
        
        //slider increase
        sunburnNum += (burnRedNum * Time.deltaTime);
        if (sunburnNum >= 20)
        {
            sunburnNum = 20;
        }
        
        //speed boost stuff
        if (speedTimer >= 0)
        {
            speedTimer = speedTimer -= 1 * Time.deltaTime;
        }
        if (speedTimer <= 0)
        {
            movementSpeed = 15f;
            speedTimer = 0;
        }
        if (speedBoost)
        {
            movementSpeed = 24f;
            speedTimer = 5;

            speedBoost = false;
        }
        
        //burn reduction stuff
        if (redTimer >= 0)
        {
            redTimer = redTimer -= 1 * Time.deltaTime;
        }
        if (redTimer <= 0)
        {
            burnRedNum = 3f;
            redTimer = 0;
        }
        if (burnReduction)
        {
            burnRedNum = 1f;
            redTimer = 5;
            
            burnReduction = false;
        }
        
        
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Shade"))
        {
            audioSource.clip = sfx[1];
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
            burnRedNum = 0;
            sunburnNum -= 2 * Time.deltaTime;
            if (sunburnNum <= 0)
            {
                sunburnNum = 0;
            }
        }
        else
        {
            burnRedNum = 1;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Water":
                sunburnNum -= 5;
                if (sunburnNum <= 0)
                {
                    sunburnNum = 0;
                }
                audioSource.clip = sfx[3];
                audioSource.Play();
                Destroy(collision.gameObject);
                break;
            case "Suncream":
                burnReduction = true;
                audioSource.clip = sfx[2];
                audioSource.Play();
                Destroy(collision.gameObject);
                break;
            case "EnergyDrink":
                speedBoost = true;
                audioSource.clip = sfx[0];
                audioSource.Play();
                Destroy(collision.gameObject);
                break;
            case "Finish":
                finishTime = 100 - timerManager.currentTime;
                PlayerPrefs.SetFloat("BestTime", finishTime);
                bestText.text = $"finishTime.ToString()";
                isFinished = true;
                break;
        }
    }
}
