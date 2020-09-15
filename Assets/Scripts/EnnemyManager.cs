using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnnemyManager : MonoBehaviour
{

	public Transform spawnPoint1;
	public Transform spawnPoint2;
	public Transform spawnPoint3;
	public Transform spawnPoint4;
	public Transform spawnPoint5;
	public int activeSpawnNum = 5;

	private Transform[] spawnPoints;
	private Transform[] activeSpawns;
	public GameObject ennemyPrefab1;
	// public GameObject ennemyPrefab2;

	private int accKills;
	private int counter;
	private int[] waves;
	public int wave1;
	public int wave2;
	public int wave3;
	public int wave4;
	public int wave5;

	public bool testMode;
	public float testSpawnTime;
	private Timer testSpawnTimer;


    // Start is called before the first frame update
    void Start()
    {
    	spawnPoints = new Transform[5] {spawnPoint1, spawnPoint2, spawnPoint3, spawnPoint4, spawnPoint5};
    	activeSpawns = new Transform[activeSpawnNum];
    	for (int i = 0 ; i < activeSpawnNum; i++) {
    		activeSpawns[i] = spawnPoints[i]; 
    	}
		PlayerStats.resetWaveCounter();
    	accKills = PlayerStats.ennemiesKilled;
    	counter = 0;
        waves = new int[5] {wave1, wave2, wave3, wave4, wave5};
        InvokeRepeating("spawnEnnemy", 0f, 0.5f);
        InvokeRepeating("checkWave", 5f, 3f);

        testSpawnTimer = new Timer(testSpawnTime);
    }

    void Update() {
    	if (Input.GetButtonDown("Fire2") && testSpawnTimer.isEnabled()) {
    		int random = Random.Range(0, 4);
			Instantiate(ennemyPrefab1, activeSpawns[random].position, activeSpawns[random].rotation);
			testSpawnTimer.reset();
		}
		testSpawnTimer.countDown();
    }

    public void waveCleared() {
		PlayerStats.waveCompleted();
		counter = 0;
    }

    public void spawnEnnemy() {
    	if (!testMode) {
	    	int random = Random.Range(0, 4);
	    	if (PlayerStats.currentWave < waves.Length) {
		    	if (counter < waves[PlayerStats.currentWave]) {
		    		Instantiate(ennemyPrefab1, activeSpawns[random].position, activeSpawns[random].rotation);
		    		counter++;
		    	}
		    }
	    }
    }

    public void checkWave() {
    	// TODO fix bug about restarting and waves not starting
    	if (PlayerStats.ennemiesKilled == totalKills(accKills)) {
    		if ((PlayerStats.currentWave + 1) < waves.Length) {
    			waveCleared();
    		} else {
	    		PlayerStats.levelCompleted();
	    		SceneManager.LoadScene(PlayerStats.levelName[PlayerStats.currentLevel]);
    		}
    	}
    }

    public int totalKills(int accKills = 0) { 
    	int acc = accKills;
    	for (int i = 0; i <= PlayerStats.currentWave ; i++) {
    		acc += waves[i];
    	}
    	return acc;
    }
}
