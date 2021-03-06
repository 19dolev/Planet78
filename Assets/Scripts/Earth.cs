﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Earth : MonoBehaviour
{

    public int health = 8;
    public Slider healthSlider;
    public Image sliderImage;
    public AttackManager aManager;
    public bool damaged = false;
    public Image hitImage, fadeImage;
    public Color flashColor;
    public float flashSpeed = 3f;
    public Animator anim;
    private float fadeTime;
    public LevelManager lManager;
    public GameObject[] planets;
    public int currentPlanet = 0;
    public AdManager adManager;
    public AudioSource explosionAudio;

    // Use this for initialization
    void Start()
    {

        currentPlanet = PlayerPrefs.GetInt("Current");

        GameObject newPlanet = planets[currentPlanet];

        GetComponent<MeshFilter>().mesh = newPlanet.GetComponent<MeshFilter>().sharedMesh;
        GetComponent<Renderer>().materials = newPlanet.GetComponent<Renderer>().sharedMaterials;



    }

    void Update()
    {

        if (damaged)
        {



            hitImage.color = flashColor;

        }
        else
        {

            hitImage.color = Color.Lerp(hitImage.color, Color.clear, flashSpeed * Time.deltaTime);

        }

        damaged = false;
    }

    void OnCollisionEnter(Collision col)
    {
        if (!healthSlider)
        {
            healthSlider = GameObject.Find("Slider").GetComponent<Slider>();
        }
        if (!hitImage)
        {
            hitImage = GameObject.Find("HitImage").GetComponent<Image>();
        }
        if (!sliderImage)
        {
            sliderImage = GameObject.Find("Fill").GetComponent<Image>();
        }
        if(!adManager)
            adManager = GameObject.Find("AdManager").GetComponent<AdManager>();
        if(!lManager)
            lManager = GameObject.Find("LevelManager").GetComponent<LevelManager>();
        if (!aManager)
            aManager = GameObject.Find("AttackManager").GetComponent<AttackManager>();
        print("kaboom");
        damaged = true;
        health--;
        healthSlider.value--;
        if (health <= 2)
            sliderImage.color = Color.red;

        if (health <= 0)
        {
            //Explode

            //explosionAudio.Play();


            aManager.gameOver = true;



            //			Time.timeScale = 0;


            PlayerPrefs.SetInt("Score", aManager.score);

            if (PlayerPrefs.GetInt("Score") > PlayerPrefs.GetInt("Best"))
            {
                PlayerPrefs.SetInt("Best", PlayerPrefs.GetInt("Score"));

            }
            lManager.BackToMenu();


            Social.ReportScore(PlayerPrefs.GetInt("Score"), "CgkI1KCwvoQTEAIQBw", (bool success) =>
            {
                // handle success or failure
            });

            //show ad
            //adManager.ShowInter();
        }
    }








}
