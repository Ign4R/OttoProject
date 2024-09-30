using UnityEngine;

public class Food : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            ///Sumar puntos
            gameObject.SetActive(false);
        }
    }
}
