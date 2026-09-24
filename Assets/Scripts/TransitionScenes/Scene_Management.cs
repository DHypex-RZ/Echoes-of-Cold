using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Threading.Tasks;

public class Scene_Management : MonoBehaviour
{
    public Animator animator;

    public float transitionTime;

    public void LoadNextScene(string getScene)
    {
        LoadLevel(getScene);
    }

    public async void LoadLevel(string SceneName)
    {

        animator.SetTrigger("ChangingScene");
        
        await Awaitable.WaitForSecondsAsync(transitionTime);
        
        SceneManager.LoadScene(SceneName);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
