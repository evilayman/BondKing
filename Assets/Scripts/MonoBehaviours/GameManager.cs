using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private ChoticObstacles cO;
    private NormalOneDirObs nODO;
    private NormalOneDirWithStandingObs nODWSO;
    private NormalTwoDirObs nTDO;
    private NormalTwoDirWithStandingObs nTDWSO;
    private string obsEnabled;
    [SerializeField]
    private Text mode;
    private void Start()
    {
        cO = (ChoticObstacles)FindObjectOfType(typeof(ChoticObstacles));
        nODO = (NormalOneDirObs)FindObjectOfType(typeof(NormalOneDirObs));
        nODWSO = (NormalOneDirWithStandingObs)FindObjectOfType(typeof(NormalOneDirWithStandingObs));
        nTDO = (NormalTwoDirObs)FindObjectOfType(typeof(NormalTwoDirObs));
        nTDWSO = (NormalTwoDirWithStandingObs)FindObjectOfType(typeof(NormalTwoDirWithStandingObs));
    }
    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void ChangeObstacleMode()
    {

        if (obsEnabled == null)
        {
            cO.chaoticRandomGenerationMode = true;
            obsEnabled = "cMode";
            mode.text = "Chaotic";
        }
        else if(obsEnabled=="cMode")
        {
            cO.chaoticRandomGenerationMode = false;
            nODO.normalOneDirectionMode = true;
            obsEnabled = "oDM";
            mode.text = "One Dir";
        }
        else if (obsEnabled == "oDM")
        {
            nODO.normalOneDirectionMode = false;
            nODWSO.normalOneDirectionWithStandingMode = true;          
            obsEnabled = "oDMWS";
            mode.text = "One Dir Standing";
        }
        else if (obsEnabled == "oDMWS")
        {
            nODWSO.normalOneDirectionWithStandingMode = false;
            nTDO.normalTwoDirectionMode = true;
            obsEnabled = "tDM";
            mode.text = "Two Dir";
        }
        else if (obsEnabled == "tDM")
        {
            nTDO.normalTwoDirectionMode = false;
            nTDWSO.normalTwoDirectionWithStandingMode = true;
            obsEnabled = "tDMWS";
            mode.text = "Two Dir Standing";
        }
        else if (obsEnabled == "tDMWS")
        {
            nTDWSO.normalTwoDirectionWithStandingMode = false;
            obsEnabled = null;
            mode.text = "None";
        }



    }


}
