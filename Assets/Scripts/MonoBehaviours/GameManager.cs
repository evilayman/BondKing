using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private ObstacleRandomGeneration oRG;
    private bool sceneReloadedByObsMode;
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnLevelFinishedLoading;
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void ChangeObstacleMode()
    {

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        sceneReloadedByObsMode = true;


    }

    void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
    {
       
        
            oRG = (ObstacleRandomGeneration)FindObjectOfType(typeof(ObstacleRandomGeneration));
            oRG.chaoticRandomGenerationMode = true;
            print(oRG.chaoticRandomGenerationMode);
        
       
    }
}
