using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private ObstacleRandomGeneration oRG;
    private bool sceneReloadedByObsMode;
    private string obsEnabled;
    [SerializeField]
    private Text mode;
    private void Start()
    {
        oRG = (ObstacleRandomGeneration)FindObjectOfType(typeof(ObstacleRandomGeneration));
    }
    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void ChangeObstacleMode()
    {

        if (obsEnabled == null)
        {
            oRG.chaoticRandomGenerationMode = true;
            obsEnabled = "cMode";
            mode.text = "Chaotic";
        }
        else if(obsEnabled=="cMode")
        {
            oRG.chaoticRandomGenerationMode = false;
            oRG.normalOneDirectionMode = true;
            obsEnabled = "oDM";
            mode.text = "One Dir";
        }
        else if (obsEnabled == "oDM")
        {
            oRG.normalOneDirectionMode = false;
            oRG.normalOneDirectionModeV = true;          
            obsEnabled = "oDMV";
            mode.text = "One Dir V";
        }
        else if (obsEnabled == "oDMV")
        {            
            oRG.normalOneDirectionModeV = false;
            oRG.normalTwoDirectionMode = true;
            obsEnabled = "tDM";
            mode.text = "Two Dir";
        }
        else if (obsEnabled == "tDM")
        {
            oRG.normalTwoDirectionMode = false;
            oRG.normalTwoDirectionModeV = true;
            obsEnabled = "tDMV";
            mode.text = "Two Dir V";
        }
        else if (obsEnabled == "tDMV")
        {
            oRG.normalTwoDirectionModeV = false;
            obsEnabled = null;
            mode.text = "None";
        }



    }


}
