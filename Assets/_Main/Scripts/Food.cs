using UnityEngine;

public class Food : MonoBehaviour
{
    [SerializeField]
    private int _score;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player")) 
        {
            GameManager.instance.AddScore(_score);
            gameObject.SetActive(false);
        }
    }

}
