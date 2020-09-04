using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformerPlayer : MonoBehaviour
{
    public float speed = 250.0f;
    private Rigidbody2D _body;

    private Animator _anim;

    public float jumpForce = 12.0f;

    private BoxCollider2D _box;

    // Start is called before the first frame update
    void Start()
    {
        _body = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        _box = GetComponent<BoxCollider2D>();

        float deltaX = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        Vector2 movement = new Vector2(deltaX, _body.velocity.y); //задаем только смещение по X
        _body.velocity = movement;


        //ниже проверяем, стоит ли персонаж на поверхности или нет
        Vector3 max = _box.bounds.max;
        Vector3 min = _box.bounds.min;

        Vector2 corner1 = new Vector2(max.x, min.y - .1f);
        Vector2 corner2 = new Vector2(min.x, min.y - .2f);
        Collider2D hit = Physics2D.OverlapArea(corner1, corner2);

        bool grounded = false;
        if (hit != null)
        {
            grounded = true;
        }

        _body.gravityScale = grounded && deltaX == 0 ? 0 : 1; //Остановка при нахожении на поверхности в статичном состоянии

        if (grounded && Input.GetKeyDown(KeyCode.Space))
        {
            _body.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse); //добавление силы для прыжка при нажатии пробела
        }

        MovingPlatform platform = null;
        if (hit != null)
        {
            platform = hit.GetComponent<MovingPlatform>(); //проверяем, двигается ли платформа под персонажем
        }

        Vector3 pScale = Vector3.one;
        if (platform != null) //связываем персонажа с платформой
        {
            transform.parent = platform.transform;
            pScale = platform.transform.localScale;
        } else
        {
            transform.parent = null;
        }

        if (deltaX != 0)
        {
            transform.localScale = new Vector3(Mathf.Sign(deltaX) /
                pScale.x, 1 / pScale.y, 1);
        }

        _anim.SetFloat("speed", Mathf.Abs(deltaX)); //скорость больше 0 даже при отрицаетльных значениях velocity

    }
}
