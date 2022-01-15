using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public Text uitimer;
   [SerializeField] private float _uitimer=30f;
    Scene scene;
    private float moveInput;
    public bool onGrav = true;
    public bool offGrav = false;
    public GameObject player;
    private float _speed = 10f;
   [SerializeField] private int _countCoins = 0;
       public GameObject point;

    private Rigidbody rb;
    void Win()
    {
        if (_countCoins>= 6)
        {
            int nextSceneIndex=scene.buildIndex+1;
            SceneManager.LoadScene(nextSceneIndex);
        }
    }
    void Lose()
    {
        if (_uitimer <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
   
    
    void Start()
    {
        scene = SceneManager.GetActiveScene();

        uitimer.text = _uitimer.ToString();
        rb = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        Win();
        Lose();
        OnGravity();
    }
    void FixedUpdate()
    {
        _uitimer -= Time.deltaTime;
        uitimer.text = Mathf.Round(_uitimer).ToString();
        moveInput = Input.GetAxis("Horizontal");
       
        rb.velocity = new Vector2(moveInput * (_speed * -1), rb.velocity.y);

        
    }


    public void OnGravity()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (onGrav == false && offGrav)
            {
                //StartCoroutine(WaitGrav());
                onGrav = true;
                offGrav = false;
                Physics.gravity = new Vector3(0, -10f, 0);
                print("гравитация включена");
            }
            else
            {
                //StartCoroutine(WaitGrav());
                onGrav = false;
                offGrav = true;
                Physics.gravity = new Vector3(0, 10f, 0);
                print("гравитация выключена");
            }

        }



    }

    IEnumerator  WaitGrav()
    {
        yield return new WaitForSeconds(20f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "point")
        {
            _countCoins++;
            _uitimer++;
            Destroy(other.gameObject);
        }
    }
    













}