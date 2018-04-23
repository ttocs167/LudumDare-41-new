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
    public GameObject bossType;
    public GameObject buildManagerGO;
    private BuildingManagerScript buildManager;
    public float maxBuildTime;
    public Text timer;
    public Text waveCount;
    public GameObject pauseScreen;
    public GameObject UI;

    public bool FishSpawning;
    //SetDay Night
    private SetDayNight setDayNight;
    public GameObject setDayNightGO;
    public GameObject rainParticles;
    //BGM Audio
    public float bgmVolume = 0.10f;
    public float bgRainVolume = 0.50f;
    private bool bgPlay = false;
    AudioSource bgMusic;    
    AudioSource bgRain;
    public AudioClip fishingMusic1;
    public AudioClip fishingMusic2;
    public AudioClip rainSFX;

    private bool first = true;
    private GameObject FishSpawner;
    public int bossFrequency = 5;
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
        if (currentState == "BUILD" && Input.GetButtonDown("Jump")&&(!first))
        {
            if (setDayNight != null)
            {
                setDayNight.StartNight();
            }
            if (rainParticles != null)
            {
                StartCoroutine(bgmFadeIn(bgRain, 3f, bgRainVolume));
                rainParticles.SetActive(true);
            }
            currentState = "SPAWN";
            waveCounter++;
            if (waveCounter % 5 == 0)
            {
                waveCount.text = ("Boss Wave");
            }
            else
            {
                waveCount.text = ("Wave: " + waveCounter);

            }
        }
        if (Input.GetButtonDown("Cancel"))
        {
            Time.timeScale = 0f;
            UI.SetActive(false);           
            pauseScreen.SetActive(true);

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
        if(waveCounter==0&&first)
        {
            timer.text = ("BUILD A TOWER TO SUMMON THE HORDE!");
            // do build mode things

            if (turretCounter > 0)
            {
                first = false;
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
                    StartCoroutine(bgmFadeIn(bgRain, 3f, bgRainVolume));
                    rainParticles.SetActive(true);
                }
                currentState = "SPAWN";
                waveCounter++;
                if (waveCounter % 5 == 0)
                {
                    waveCount.text = ("Boss Wave");
                }
                else
                {
                    waveCount.text = ("Wave: " + waveCounter);

                }
             }
        }
        
    }

    void spawnState()
    {
        currentState = "FIGHT";
        Debug.Log(waveCounter);
        if (enemiesArray.Length == 0)
        {
            for (int i = 0; i < enemyCount; i++)
            {
                if(waveCounter%5==0&&waveCounter!=0)
                {                 
                    
                    Invoke("SpawnBoss", 0f);
                }
                else
                {
                    Invoke("SpawnEnemies", 0f);
                }                
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
                StartCoroutine(bgmFadeOut(bgRain, 3f));
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
            timer.text = ("ZOMBOSAS INCOMING \n PRESS SPACE BY YOUR CHAIR TO FISH");
        }
        
    }
    void SpawnBoss()
    {
        float xPos = Random.Range(spawnArea.transform.position.x - width, spawnArea.transform.position.x + width);
        float yPos = Random.Range(spawnArea.transform.position.y - height, spawnArea.transform.position.y + height);
        Vector2 spawnLocation = new Vector2(xPos, yPos);
        GameObject enemy = (GameObject)Instantiate(bossType, spawnLocation, transform.rotation);
        if (!FishSpawning)
        {
            FishSpawner.GetComponent<FishSpawner>().StartCoroutine("addFish");
            FishSpawning = true;
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
        AudioSource[] audioSources = GetComponents<AudioSource>();
        bgMusic = audioSources[0];
        bgRain = audioSources[1];
        setBgSong(fishingMusic2);
        bgRain.clip = rainSFX;
        StartCoroutine(bgmFadeIn(bgMusic, 5f, bgmVolume));
    }

    //Fade in/out co-routines
    public IEnumerator bgmFadeIn(AudioSource audioSource, float FadeTime, float MaxVolume)
    {
        Debug.Log("Fade In Started!");

        audioSource.volume = 0;
        audioSource.loop = true;
        audioSource.Play();

        while (audioSource.volume < MaxVolume)
        {
            audioSource.volume += MaxVolume * Time.deltaTime / FadeTime;

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
    }

    void setBgSong(AudioClip bgmClip)
    {
        bgMusic.clip = bgmClip;
    }

}

