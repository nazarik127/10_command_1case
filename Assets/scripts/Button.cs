using UnityEngine;
using UnityEngine.SceneManagement; // �����������

public class SceneButton : MonoBehaviour
{
    // ��� �����, ������� ����� ���������
    public string sceneName = "mini_game1";

    // ���� ����� ���������� ��� ������� ������
    public void LoadScene()
    {
        SceneManager.LoadScene(sceneName);
    }
}
