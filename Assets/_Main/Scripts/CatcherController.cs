using UnityEngine;
public class CatcherController : MonoBehaviour
{
    private Rigidbody2D _rb;
    private Vector2 _direction;

    private bool isTouched;

    [SerializeField] 
    private float _speed;

    private void Awake()
    {
        // Obtener el Rigidbody2D      
        _rb = GetComponent<Rigidbody2D>();
    }
    private void Start()
    {
        _direction = Vector2.right;
    }
    private void FixedUpdate()
    {
        Movement();
    }

    private void Movement()
    {
        Vector2 newPosition = _rb.position + _direction * _speed * Time.deltaTime;
        // Mover usando Rigidbody2D.MovePosition para respetar la física
        _rb.MovePosition(newPosition);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Limit"))
        {
            // Cambiar la dirección si está tocando o no
            if (_direction == Vector2.right)
            {
                _direction = Vector3.left; // Cambiar de derecha a izquierda
            }
            else if (_direction == Vector2.left)
            {
                _direction = Vector3.right; // Cambiar de izquierda a derecha
            }
        }
    }

}
