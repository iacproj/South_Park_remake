using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField] Scene MainGame;
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject GuiPanel;
    

    private void Start()
    {
        pausePanel.SetActive(false);
    }

    public void StartGame()
    {
        SceneManager.LoadScene("HagashaCity");
        
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!pausePanel.activeInHierarchy)
            {
                PauseGame();
            }
            else if (pausePanel.activeInHierarchy)
            {
                ContinueGame();
            }
        }
    }

    private void PauseGame()
    {
        Time.timeScale = 0;
        pausePanel.SetActive(true);
        GuiPanel.SetActive(false);
       
        //Disable scripts that still work while timescale is set to 0
    }
    public void ContinueGame()
    {
        Time.timeScale = 1;
        pausePanel.SetActive(false);
        GuiPanel.SetActive(true);
        
        //enable the scripts again
    }
}
