using UnityEngine;
using UnityEngine.SceneManagement;

public class Bootstrap : MonoBehaviour
{
    public void SetHost()
    {
        Globals.isHost = true;
        SceneManager.LoadScene("Menu");
    }

    public void SetClient()
    {
        Globals.isHost = true;
        SceneManager.LoadScene("Menu");
    }
}
