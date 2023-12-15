using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void PlayGame() => SceneManager.LoadScene("In-Game");
    public void Instructions() => SceneManager.LoadScene("Instructions");
    public void Back() => SceneManager.LoadScene("Menu");
    public void QuitGame() => Application.Quit();
}