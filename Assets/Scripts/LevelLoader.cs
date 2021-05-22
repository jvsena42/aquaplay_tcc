using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    int currentSceneIndex;

    // Start is called before the first frame update
    void Start()
    {
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
    }

    public void LoadNextScene(){
        SceneManager.LoadScene(currentSceneIndex+1);
    }

     public void LoadPreviousScene(){
        SceneManager.LoadScene(currentSceneIndex-1);
    }

     public void LoadStartScene(){
        SceneManager.LoadScene(0);
    }

    public void LoadScene(string sceneName){
        SceneManager.LoadScene(sceneName);
    }

    public void QuitGame(){
        Application.Quit();
    }
}
