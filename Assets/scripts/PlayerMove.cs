using UnityEngine;
public class InputChecker : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W)) Debug.Log("W down");
        if (Input.GetKeyDown(KeyCode.A)) Debug.Log("A down");
        if (Input.GetKeyDown(KeyCode.S)) Debug.Log("S down");
        if (Input.GetKeyDown(KeyCode.D)) Debug.Log("D down");

        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        if (h != 0f || v != 0f) Debug.Log($"Axis H:{h} V:{v}");
    }
}
