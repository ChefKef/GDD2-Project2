using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    //Changes game to the Title Screen
    public void GoToMainMenu()
    {
        SceneManager.LoadScene("TitleScene");
    }

    //Changes game to the Controls Screen
    public void GoToControls()
    {
        SceneManager.LoadScene("ControlsScene");
    }

    //Changes game to the Credits Screen
    public void GoToCredits()
    {
        SceneManager.LoadScene("CreditsScene");
    }

    //Changes game to the Level Select Screen
    public void GoToLevelSelect()
    {
        SceneManager.LoadScene("LevelUIScene");
    }

    //Changes game to the specified Level
    public void GoToLevel(int level)
    {
        SceneManager.LoadScene("Level" + level + "Scene");
    }

    //Pauses the current Level
    public void PauseLevel()
    {
        //To Do: Add connections to pausing in level controller
    }

    //Resumes the current Level
    public void ResumeLevel()
    {
        //To Do: Add connections to resuming in level controller
    }

    //Reloads the current Level
    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    //Closes the game
    public void ExitGame()
    {
        Application.Quit();
    }

}

