using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    private ChatManager chatManager;

    void Start()
    {
        chatManager = FindObjectOfType<ChatManager>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M) && (chatManager == null || !chatManager.isWriting))
        {
            SceneManager.LoadScene("Menu");
        }
    }
}
