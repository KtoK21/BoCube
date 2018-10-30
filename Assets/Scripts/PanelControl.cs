using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PanelControl : MonoBehaviour {
    
    public void OnRetryButton()
    {
        SceneManager.LoadScene("Main");
    }

    public void OnExitButton()
    {
        Application.Quit();
    }
}
