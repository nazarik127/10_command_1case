using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // <- важно

public class trigger : MonoBehaviour
{
    public string sceneName = "mini_game2";
    int counter = 0;

    private void OnTriggerEnter(Collider other)
    {
        // Проверяем, что это предмет
        if (other.CompareTag("Item"))
        {
            counter++;
            Destroy(other.gameObject);
        }

        if (counter >= 1) // на всякий случай >=
        {
            SceneManager.LoadScene(sceneName);
        }
    }
}
