using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCam : MonoBehaviour
{
    public Transform target;

    public float smoothTime = 0.2f;

    private Vector3 _velocity = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate() //метод срабатывает после ВСЕХ Update
    {
        Vector3 targetPosition = new Vector3(
            target.position.x, target.position.y, transform.position.z); //изменяем x и y, но оставляем z

        transform.position = Vector3.SmoothDamp(transform.position,
            targetPosition, ref _velocity, smoothTime); //плавный переход от текущей к целевой позиции
    }
}
