using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{{
    public void PlayGame() => SceneManager.LoadScene("In-Game");

    
    public void QuitGame()
    {
        Application.Quit();
       
    }
}}