using System;
using UnityEngine.SceneManagement;

namespace SA.Runtime.Core.Services
{
    public class SceneService
    {
        public void LoadMenu()
        {
            SceneManager.LoadSceneAsync("Menu");
        }

        public void LoadGame()
        {
            SceneManager.LoadSceneAsync("Game");
            SceneManager.LoadSceneAsync("Environment", LoadSceneMode.Additive);
            SceneManager.LoadSceneAsync("UI", LoadSceneMode.Additive);
        }
    }
}