using UnityEngine;
public class CharacterController : MonoBehaviour
{
    private SpriteRenderer _sRender;
    private Rigidbody2D _rb;
    private GameObject _grabber;
    private CircleCollider2D _coll;

    [SerializeField] 
    private Sprite[] _sprites;

    private Vector3 _pointSpawn;
    private Vector3 _grabPosStart;
    private bool _isDragged;
    private int _countFood;

    [SerializeField]
    private float _velocityGravity;
    [SerializeField]
    private float [] _colliderSizes;

    public bool IsDragged { get => _isDragged; }

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _sRender = GetComponent<SpriteRenderer>();
        _grabber = transform.GetChild(0).gameObject;
        _coll = GetComponent<CircleCollider2D>();
    }

    private void Start()
    {
        _pointSpawn = transform.position;
        _grabPosStart = _grabber.transform.localPosition;
    }

    public void Movement(Vector2 position)
    {
        // Nos movemos utilizando el rigibody 
        _rb.position = position;

    }
    public void IsFalling()
    {
        _grabber.transform.SetParent(null);
        _rb.freezeRotation = false;
        // Ponemos la variable en true para indicar que ya fue agarrado
        _isDragged = true;
        // Setiamos la gravedad en un valor para que caiga hacia abajo con una velocidad
        _rb.gravityScale = 1 * _velocityGravity;
    }

    public void ResetValues()
    {
        _countFood = 0;
        _sRender.sprite = _sprites[0];
        _coll.radius = _colliderSizes[0];
        _rb.freezeRotation = true;
        _rb.velocity = Vector3.zero;
        _rb.gravityScale = 0;
        _rb.rotation = 0;
        _grabber.transform.position = _grabPosStart;
        _grabber.transform.SetParent(gameObject.transform,false);
        transform.position = _pointSpawn;
        _isDragged = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Catcher"))
        {
            ///Sumar vidas o puntos
            ResetValues();
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Food"))
        {

            if (_countFood >= 0 && _countFood < _sprites.Length - 1) // Restamos 1 porque luego incrementamos
            {
                _countFood++; // Incrementar la variable solo si estamos dentro de los límites
                _sRender.sprite = _sprites[_countFood]; // Asignar el sprite correspondiente
            }
            if (_countFood < _colliderSizes.Length)
            {
                _coll.radius = _colliderSizes[_countFood];
            }
            ///A medida que va aumentando una variable se va engordando el sprite
            ///Sumar puntos
        }
    }


}
