using UnityEngine;

public class Food : MonoBehaviour
{
    private int countFood;
    [SerializeField]
    private int _score;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player")) 
        {
            countFood++;

            GameManager.instance.AddScore(_score);
            gameObject.SetActive(false);
        }
    }

}
