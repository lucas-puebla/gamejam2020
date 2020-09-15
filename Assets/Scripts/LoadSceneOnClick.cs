using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneOnClick : MonoBehaviour
{
    public void LoadScene(int level = 0) {
    	PlayerStats.currentLevel = level; 
    	SceneManager.LoadScene(PlayerStats.levelName[level]);
    }
}
