using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnnemyManager : MonoBehaviour
{

	public Transform spawnPoint1;
	public Transform spawnPoint2;
	public Transform spawnPoint3;
	public Transform spawnPoint4;

	private Transform[] spawnPoints;

	public GameObject ennemyPrefab1;
	// public GameObject ennemyPrefab2;

	private int counter;
	public int[] waves;


    // Start is called before the first frame update
    void Start()
    {
    	spawnPoints = new Transform[4] {spawnPoint1, spawnPoint2, spawnPoint3, spawnPoint4};
		PlayerStats.resetWaveCounter();
    	counter = 0;
        waves = new int[3] {5, 7, 10};
        InvokeRepeating("spawnEnnemy", 0f, 0.5f);
        InvokeRepeating("checkWave", 5f, 3f);
    }

    public void waveCleared() {
		PlayerStats.waveCompleted();
		counter = 0;
    	
    }

    public void spawnEnnemy() {
    	int random = Random.Range(0, 3);
    	if (PlayerStats.currentWave < waves.Length) {
	    	if (counter < waves[PlayerStats.currentWave]) {
	    		Instantiate(ennemyPrefab1, spawnPoints[random].position, spawnPoints[random].rotation);
	    		counter++;
	    	}
	    }
    }

    public void checkWave() {
    	if (PlayerStats.ennemiesKilled == accKills()) {
    		if ((PlayerStats.currentWave + 1) < waves.Length) {
    			waveCleared();
    		}
    		// SceneManager.LoadScene()
    	}
    }

    public int accKills() {
    	int acc = 0;
    	for (int i = 0; i <= PlayerStats.currentWave ; i++) {
    		acc += waves[i];
    	}
    	return acc;
    }
}
