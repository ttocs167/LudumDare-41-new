using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
using UnityEngine.UI;

public class phaseManager : MonoBehaviour
{
    //string FIGHT/BUILD/NONE
    public string currentState;

    private int enemyCount = 5;
    private int waveCounter = 0;
    private float buildTime;
    private GameObject[] enemiesArray;
    //List<GameObject> enemiesList = new List<GameObject>();
    private int turretCounter = 0;
    public float width = 1f;
    public float height = 1f;
    public GameObject spawnArea;
    public GameObject enemyType;
    public GameObject buildManagerGO;
    private BuildingManagerScript buildManager;
    public float maxBuildTime;
    public Text timer;
    public Text waveCount;

    public bool FishSpawning;
    //SetDay Night
    private SetDayNight setDayNight;
    public GameObject setDayNightGO;
    public GameObject rainParticles;
    //BGM Audio
    public float bgmVolume = 0.10f;
    private bool bgPlay = false;
    AudioSource bgMusic;
    public AudioClip fishingMusic1;
    public AudioClip fishingMusic2;

    private GameObject FishSpawner;
    // Use this for initialization
    void Start()
    {
        currentState = "BUILD";
        buildTime = maxBuildTime;
        timer.text = ("" + maxBuildTime);
        FishSpawner = GameObject.Find("FishSpawner");
        buildManagerGO= this.gameObject;
        if(buildManagerGO!=null)
        {
            buildManager = buildManagerGO.GetComponent<BuildingManagerScript>();
        }
        if (setDayNightGO != null)
        {
            setDayNight = setDayNightGO.GetComponent<SetDayNight>();

        }
        //Initializes BG music.
        bgmAudioInit();
    }
    void Update()
    {
        if (currentState == "BUILD" && Input.GetButtonDown("Jump")&&(waveCounter>0))
        {
            if (setDayNight != null)
            {
                setDayNight.StartNight();
            }
            if (rainParticles != null)
            {
                rainParticles.SetActive(true);
            }
            currentState = "SPAWN";
        }
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if(buildManager!=null)
        {
            turretCounter = buildManager.towerCount;
        }        
        //Debug.Log("Current State = " + currentState);
        enemiesArray = GameObject.FindGameObjectsWithTag("Enemy");

        //Audio

        switch (currentState)
        {
            case "BUILD":
                buildState();
                break;
            case "SPAWN":
                spawnState();
                break;
            case "FIGHT":
                fightState();
                break;
        }
    }

    void buildState()
    {
        FishSpawner.GetComponent<FishSpawner>().Stop();
        FishSpawning = false;
        if(waveCounter==0)
        {
            timer.text = ("BUILD A TOWER TO SUMMON THE HORDE!");
            // do build mode things

            if (turretCounter > 0)
            {
                if (setDayNight != null)
                {
                    setDayNight.StartNight();
                }
                if (rainParticles != null)
                {
                    rainParticles.SetActive(true);
                }
                currentState = "SPAWN";
                waveCounter++;
                waveCount.text = ("Wave: " + waveCounter);
            }
        }
        else
        {
            buildTime -= Time.deltaTime;
            timer.text = ("BUILD TIME LEFT: " + Mathf.Floor(buildTime) + "\n PRESS SPACE TO SUMMON THE HORDE!");
            // do build mode things

            if (buildTime < 0)
            {
                if (setDayNight != null)
                {
                    setDayNight.StartNight();
                }
                if (rainParticles != null)
                {
                    rainParticles.SetActive(true);
                }
                currentState = "SPAWN";
                waveCounter++;
                waveCount.text = ("Wave: " + waveCounter);
            }
        }
        
    }

    void spawnState()
    {
        currentState = "FIGHT";

        if (enemiesArray.Length == 0)
        {
            for (int i = 0; i < enemyCount; i++)
            {
                Invoke("SpawnEnemies", 0f);
            }
        }
    }

    void fightState()
    {
        
        //Debug.Log("enemiesList count = " + enemiesArray.Length);
        if (enemiesArray.Length == 0)
        {
            if (setDayNight != null)
            {
                setDayNight.StartDay();
            }
            if (rainParticles != null)
            {
                rainParticles.SetActive(false);
            }
            currentState = "BUILD";
            buildTime = maxBuildTime;
            
            enemyCount += Random.Range(1, 4);
            Debug.Log("number of enemies this wave: " + enemyCount);
            
        }
        if(turretCounter==0)
        {
            timer.text = ("YOU'RE SCREWED");
        }
        else
        {
            timer.text = ("FIGHT OR FISH!");
        }
        
    }

    void SpawnEnemies()
    {
        float xPos = Random.Range(spawnArea.transform.position.x - width, spawnArea.transform.position.x + width);
        float yPos = Random.Range(spawnArea.transform.position.y - height, spawnArea.transform.position.y + height);
        Vector2 spawnLocation = new Vector2(xPos, yPos);
        GameObject enemy = (GameObject)Instantiate(enemyType, spawnLocation, transform.rotation);
        if (!FishSpawning)
        {
            FishSpawner.GetComponent<FishSpawner>().StartCoroutine("addFish");
            FishSpawning = true;
        }


    }

    //BGM Audio Manager Section
    void bgmAudioInit()
    {
        bgMusic = GetComponent<AudioSource>();
        setBgSong(fishingMusic2);
        StartCoroutine(bgmFadeIn(bgMusic, 5f));
    }

    //Fade in/out co-routines
    public IEnumerator bgmFadeIn(AudioSource audioSource, float FadeTime)
    {
        Debug.Log("Fade In Started!");

        audioSource.volume = 0;
        audioSource.loop = true;
        audioSource.Play();

        while (audioSource.volume < bgmVolume)
        {
            audioSource.volume += bgmVolume * Time.deltaTime / FadeTime;

            yield return null;
        }

    }

    public IEnumerator bgmFadeOut(AudioSource audioSource, float FadeTime)
    {
        Debug.Log("Fade Out Started!");
        float startVolume = audioSource.volume;

        while (audioSource.volume > 0)
        {
            audioSource.volume -= startVolume * Time.deltaTime / FadeTime;

            yield return null;
        }

        audioSource.Stop();
        audioSource.volume = startVolume;
    }

    void setBgSong(AudioClip bgmClip)
    {
        bgMusic.clip = bgmClip;
    }

}

