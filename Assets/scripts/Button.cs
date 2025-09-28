using UnityEngine;
using UnityEngine.SceneManagement; // обязательно

public class SceneButton : MonoBehaviour
{
    // Имя сцены, которую будем загружать
    public string sceneName = "mini_game1";

    // Этот метод вызывается при нажатии кнопки
    public void LoadScene()
    {
        SceneManager.LoadScene(sceneName);
    }
}
