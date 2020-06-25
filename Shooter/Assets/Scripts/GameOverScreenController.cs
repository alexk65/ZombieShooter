using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScreenController : MonoBehaviour
{
    public void Restart()
    {
        SceneManager.LoadScene(0);
    }
}
