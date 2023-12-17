using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void LoadArenaMap()
    {
        SceneManager.LoadScene("MainGame1");
    }
    public void LoadWarehouseMap()
    {
        SceneManager.LoadScene("MainGame2");
    }
    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
