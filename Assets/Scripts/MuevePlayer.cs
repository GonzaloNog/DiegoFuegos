using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MuevePlayer : MonoBehaviour
{
    float h; //Horizontal
    float d; //Profundidad
    public Rigidbody rb; //Apunta a un componente tipo RigidBody
    GameObject GO; //Apuntar objeto
    public GameObject UIWinCanvas;
    public GameObject fuegos;
    Vector3 dirMov; //Direccion en la que me muevo    
    public float velocidadSalto = 100;
    public float tiemposalto = 0.5f;
    public int maxJumps = 2;
    private int jumps = 0;
    private bool win = false;

    
    void Start()
    {
        //Si esto fuera otro script distinto, un game manager por ejemplo
        //GO = GameObject.Find("Player");
        //rb = GO.GetComponent<Rigidbody>();
        jumps = maxJumps;
    }

    void Update()
    {
        h = Input.GetAxisRaw("Horizontal");//
        d = Input.GetAxisRaw("Vertical");
        dirMov = new Vector3(h, 0, d).normalized;
        if (this.transform.position.y < 0)
            SceneManager.LoadScene(1);
       //dirMov = new Vector3(h, 0, d);

        //Movimineto cinematico (mano invisible)
        //se aplica sobre la transform de un objeto
        //NO le afectaan las físicas

        //this.transform.Translate(dirMov * Time.deltaTime*3);

        
        //if(Input.GetMouseButtonDown(0))
        //{
           // Debug.Log("SALTO!");
            //rb.AddForce(Vector3.up * 6, ForceMode.Impulse); //El empujon no se suaviza con el time delta time

        //}


        //1 - Saltar solo cuando esté en el suelo ---- OnCollision

        //2 - Boton de freno - Velocity
        //rb.velocity
        //Frenar manipulando rb.velocity
        //Con cooldown de 5 segundos

        //3 - Objetivo - Colisiones tipo trigger

        //4 - volver a empezar ->
        //O bien con colliders
        //O bien la bola mira a que altura estan en y
        //5 - hacer vuetro propio nivel

      if(Input.GetKeyDown(KeyCode.Q) && jumps >= 1 && !win)
        {
            rb.AddForce(new Vector3(0, velocidadSalto * 10, 0), ForceMode.Impulse);
            jumps--;
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(0);
        }
    }
    private void FixedUpdate()
    {//Funciona igual que un update normal pero solo se centra en funciones fisicas

        //Movimineto dinámico,sobre cuerpo rigido
        //Equivalente al translate del movimineto cinemático
        if (!win)
            rb.AddForce(dirMov * Time.deltaTime * 70);
        else
            rb.velocity = Vector3.zero;

        //rb.AddForce(dirMov * Time.deltaTime * 70, ForceMode.Impulse); <<--- NO HACER EN UPDATE
        //Impulse == empujón --- Hacerlo una sola vez
        //rb.velocity <------- Aquí se va acumulando la velocidad FÍSICA
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
            jumps = maxJumps;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Win")
        {
            UIWinCanvas.SetActive(true);
            fuegos.SetActive(true);
            win = true;
        }
    }
}
