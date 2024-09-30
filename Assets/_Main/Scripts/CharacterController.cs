using UnityEngine;
public class CharacterController : MonoBehaviour
{
    private Vector3 _pointSpawn;
    private Rigidbody2D _rb;


    private bool _isDragged;

    [SerializeField]
    private float _velocityGravity;


    public bool IsDragged { get => _isDragged; }

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        _pointSpawn = transform.position;
    }

    public void Movement(Vector2 position)
    {
        /// Nos movemos utilizando el rigibody 
        _rb.position = position;

    }
    public void IsFalling()
    {
        /// Ponemos la variable en true para indicar que ya fue agarrado
        _isDragged = true;
        /// Setiamos la gravedad en un valor para que caiga hacia abajo con una velocidad
        _rb.gravityScale = 1 * _velocityGravity;
    }
        
    public void ResetValues()
    {
        _rb.velocity = Vector3.zero;
        _rb.gravityScale = 0;
        transform.position = _pointSpawn;
        _isDragged = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Catcher"))
        {
            ///Sumar vidas y puntos
            ResetValues();
        }

    }

    
 
}
