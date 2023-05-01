using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void startGame()
    {
        SceneManager.LoadScene("IntroLevel");
    }
    public void toCredits()
    {
        SceneManager.LoadScene("Credits");
    }
}
