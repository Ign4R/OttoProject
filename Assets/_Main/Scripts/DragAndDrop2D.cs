using UnityEngine;
[RequireComponent(typeof(Collider2D))]
public class DragAndDrop2D : MonoBehaviour
{
    private CharacterController _character;
    private Camera _cam;
    private bool isOutOfArea;

    private void Awake()
    {
        _character = FindObjectOfType<CharacterController>();
        _cam = FindAnyObjectByType<Camera>();
    }

    private void OnMouseDrag()
    {
        if (!_character.IsDragged && !isOutOfArea)
        {
            MouseTracking();
        }
      
    }
    private void OnMouseExit()
    {
        isOutOfArea = true;
    }
    private void OnMouseEnter()
    {
        isOutOfArea = false;
    }
    private void OnMouseUp()
    {
        Drop();   
    }

    private void MouseTracking()
    {
        /// Obtenemos la posicion en el mundo del mouse en la pantalla
        Vector3 mouseWorldPosition = _cam.ScreenToWorldPoint(Input.mousePosition);
        /// Creamos un vector con la posicion del mouse en el mundo pero solo en X
        Vector3 trackingPosX = new(mouseWorldPosition.x, _character.transform.position.y, _character.transform.position.z);
        /// Nos movemos utilizando el rigibody 
        _character.Movement(trackingPosX);

    }
    private void Drop()
    {
        /// Setiamos la gravedad en un valor para que caiga hacia abajo con una velocidad
        _character.IsFalling();
    }



}
