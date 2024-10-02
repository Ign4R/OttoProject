using UnityEngine;

public class Obstacle : MonoBehaviour
{
    private int _countHit;
    [SerializeField]
    private int _maxHit=10;

    private void Start()
    {
        CharacterController.OnResetLevel += ResetObstacle;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Clone"))
        {
            _countHit++;
            if (_countHit >= _maxHit)
            {
                ResetObstacle(false);
            }
        }
    }

    public void ResetObstacle(bool value)
    {
        _countHit = 0;
        if (this == null || gameObject == null) return;
        gameObject.SetActive(value);
    }
}
