using UnityEngine;
using UnityEngine.SceneManagement;

public class Bootstrap : MonoBehaviour
{
    // You could use these to make setting isHost automatic...
    // public bool isHost = false;
    // void Awake()
    // {
    // 	Globals.isHost = isHost;
    // }

    // but, using buttons instead

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
