using UnityEngine;

public class DeadZone : MonoBehaviour
{
    private CharacterController _player;

    private void Awake()
    {
        _player = FindAnyObjectByType<CharacterController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            ///Perder vidas
            _player.ResetValues();

        }
         
    }
}
