using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    public Animator doorAnimator;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Персонаж вошёл в зону двери!");
            doorAnimator.SetTrigger("Open");
        }
    }
}
