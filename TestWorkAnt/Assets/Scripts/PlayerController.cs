using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerController : MonoBehaviour
{
    //слайдер регулировки скорости
    [SerializeField]
    private Slider slider;

    //Линия траектории
    [SerializeField]
    private LineRenderer line;

    private float speed;

    //Очередь позиций касания пользователя
    private Queue<Vector3> touchPositions = new Queue<Vector3>();

    private void Update()
    {
        //считывание с слайдера значение скорости
        speed = slider.value;

        //для мобильных устройств
        if (Input.touchCount > 0)
        {            
            Touch touch = Input.GetTouch(0);
            Vector3 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
            touchPosition.z = 0f;            
            touchPositions.Enqueue(touchPosition);
        }
        else  //для ПК 
        if (Input.GetMouseButton(0))
        {
            Vector3 touchPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            touchPosition.z = 0f;
            touchPositions.Enqueue(touchPosition);
        }

        if (touchPositions.Count != 0 && transform.position == touchPositions.Peek())
        {
            if (touchPositions.Count != 0) touchPositions.Dequeue();
        }
        line.SetPosition(0, transform.position);
        if (touchPositions.Count != 0)
        {
            line.SetPosition(1, touchPositions.Peek());
            transform.position = Vector3.MoveTowards(transform.position, touchPositions.Peek(), speed * Time.deltaTime);
        }   
    }
}
