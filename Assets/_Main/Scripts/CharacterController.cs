using System;
using System.Linq;
using UnityEngine;
public class CharacterController : MonoBehaviour
{
    private int foodLayer;
    private SpriteRenderer _sRender;
    private Rigidbody2D _rb;
    private GameObject _grabber;
    private CircleCollider2D _coll;
    private Sprite[] _imagesDefault;
  
    [SerializeField]
    private GameObject _prefab;
    [SerializeField]
    private Sprite[] _imagesBasic;
    [SerializeField]
    private Sprite[] _imagesSpeed;
    [SerializeField]
    private PhysicsMaterial2D[] _physMats;

    private Vector3 _pointSpawn;
    private Vector3 _grabPosStart;
    private bool _isDragged;
    private int _countFood;

    [SerializeField]
    private float [] _colliderSizes;
    [SerializeField]
    private float _velocityGravity;
    [SerializeField]
    private int _maxHit = 10;
    [SerializeField]
    private bool isClone;

    public bool IsDragged { get => _isDragged; }

    public static Action<bool> OnResetLevel;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _sRender = GetComponent<SpriteRenderer>();
        _coll = GetComponent<CircleCollider2D>();
        if (transform.childCount != 0)
            _grabber = transform.GetChild(0).gameObject;
    }

    private void Start()
    {
        foodLayer = LayerMask.NameToLayer("Food");
        _imagesDefault = _imagesBasic.ToArray();
      
        if (transform.childCount != 0)
        {
            _grabPosStart = _grabber.transform.localPosition;
            _pointSpawn = transform.position;
        }
    }

    public void Movement(Vector2 position)
    {
        // Nos movemos utilizando el rigibody 
        _rb.position = position;

    }
    public void IsFalling()
    {
        _rb.constraints = RigidbodyConstraints2D.None;
        _grabber.transform.SetParent(null);
        _rb.freezeRotation = false;
        // Ponemos la variable en true para indicar que ya fue agarrado
        _isDragged = true;
        // Setiamos la gravedad en un valor para que caiga hacia abajo con una velocidad
        _rb.gravityScale = 0.5f;
    }
    public void ChangeImages(Sprite[] images)
    {
        _imagesDefault = images.ToArray();
        _sRender.sprite = _imagesDefault[_countFood];
    }
    public void ResetValues()
    {
        _coll.sharedMaterial = _physMats[0];
        _countFood = 0;
        ChangeImages(_imagesBasic);
        _coll.radius = _colliderSizes[0];
        _rb.velocity = Vector3.zero;
        _rb.freezeRotation = true;
        _rb.constraints = RigidbodyConstraints2D.FreezePositionY;
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
            if (isClone)
            {
                gameObject.SetActive(false);
            }
            else
            {
                ///Sumar vidas o puntos
                ResetValues();
                OnResetLevel(true);
            }
      
        }

        if (collision.gameObject.CompareTag("DeadZone"))
        {
            ///Perder vidas
          
            if (isClone)
            {
                gameObject.SetActive(false);
            }
            else
            {
                ResetValues();
                OnResetLevel(true);
            }
             
        }
       
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == foodLayer)
        {

            if (_countFood >= 0 && _countFood < _imagesDefault.Length - 1) // Restamos 1 porque luego incrementamos
            {
                _countFood++; // Incrementar la variable solo si estamos dentro de los límites
                _sRender.sprite = _imagesDefault[_countFood]; // Asignar el sprite correspondiente
            }
            if (_countFood < _colliderSizes.Length)
            {
                _coll.radius = _colliderSizes[_countFood];
            }
            ///A medida que va aumentando una variable se va engordando el sprite
            ///Sumar puntos
        }

        if (collision.gameObject.CompareTag("Speed"))
        {
            _coll.sharedMaterial = _physMats[1];
            _rb.gravityScale = 1 * _velocityGravity;
            ChangeImages(_imagesSpeed);
        }

        if (collision.gameObject.CompareTag("Muffin"))
        {
            Instantiate(_prefab, transform.position, Quaternion.identity);
        }

    }


}
