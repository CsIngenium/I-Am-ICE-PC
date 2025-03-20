using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

/* mira nada más si es un data miner jajajas, que haces escudriñando un codigo
de un juego GRATUITO, debes estar muy aburrido supongo, ya que... ya tienes lo
que querías, tengo que decirte que yo progamo a mi manera como lo he echo toda
la vida, resuelvo los problemas a mi modo, no busco tutoriales para todo, si a
mi manera funciona para que romperse la cabeza no lo crees?? suerte entendiendolo
porque no dejé muchos comentarios por ser un trabajo que hice solo, yo se para que
funciona cada cosa así que diviertete escudriñando y adivinando ya que estas aquí 
jaja*/


public class pymv : MonoBehaviour
{

    //asociar el rigidbody
    public Rigidbody2D RB;

    //detectar el suelo
    private bool isgrounded;
    public Transform groundcheck;
    public LayerMask cualpiso;

    //animaciones
    private SpriteRenderer sprRender;

    //mip mip
    private float coyotetime = 0.2f;
    private float coyotecount;

    private float buffertime = 0.1f;
    private float buffercount;

    //ojos
    public bool rotado;
    public bool estatico;

    //timer
    public scr_timer timer;
    private float divisor;

    //barra de vida
    public Image barradevida;
    float vidaActual;
    float vidaMax;

    //morir
    public GameObject gcheck;
    public bool flotando, flotando2;
    private float profundidadantes;
	private float cantdesp;
	private float contador;
	private float contador2;
	private float multiplicador;
	public bool cambio = false;
    public bool insta = false;
    public GameObject daño;
    public AudioMixer mixer;
    public static scr_reinicio Instance;

    //ganar
    public bool ganar;
    public float t_ganar;

    //particulas
    public ParticleSystem p_andar_izq, p_andar_der;
    public ParticleSystem p_splash;
    public bool encharco;
    private float ptime;

    public float i;
    public float j;


    void Start()
    {
        sprRender = GetComponent<SpriteRenderer>();
    }

    
    void Update()
    {
        if(ganar == false)
        {
            if(Input.GetKey("a"))
                {
                    RB.AddForce(new Vector2(-800f * Time.deltaTime, 0));
                }
        
                if(Input.GetKey("d"))
                {
                    RB.AddForce(new Vector2(800f * Time.deltaTime, 0));
                }
        
        
        
                if(Input.GetKey("a"))
                {
                    estatico = false;
                }
                else if(Input.GetKey("d"))
                {
                    estatico = false;
                }
                else if(Input.GetKey("w"))
                {
                    estatico = false;
                }
                else
                {
                    estatico = true;
                }
                
                
        
                //detectar el piso
                isgrounded = Physics2D.OverlapCircle(groundcheck.position,.2f,cualpiso);
        
        
                if(isgrounded)
                {
                    coyotecount = coyotetime;

                    //p_andar.Play();
                }
                else
                {
                    coyotecount -= Time.deltaTime;
                }
        
        
                if(Input.GetKey("w") && flotando == false && flotando2 == false)
                {
                    buffercount = buffertime;
                }
                else
                {
                    buffercount -= Time.deltaTime;
                }
        
        
                //si se presionan la tecla de salto
                if(coyotecount > 0f && buffercount > 0f)
                {
                    RB.linearVelocity = new Vector2(RB.linearVelocity.x, 10f);
                    buffercount = 0f;
                }
                
        
                if(Input.GetKeyUp("w") && RB.linearVelocity.y > 0f && flotando == false && flotando2 == false)
                {
                    RB.linearVelocity = new Vector2(RB.linearVelocity.x, RB.linearVelocity.y * 0.5f);
                    coyotecount = 0f;
                }
        
        
                if(RB.linearVelocity.x < 0)
                {
                    sprRender.flipX = true;
                    rotado = true;
                }
                else
                {
                    sprRender.flipX = false;
                    rotado = false;
                }
        }//
    }

    void FixedUpdate()
    {

        if(isgrounded == true && rotado == false && estatico == false)
        {
            p_andar_izq.Play();
        }
        
        if(isgrounded == true && rotado == true && estatico == false)
        {
            p_andar_der.Play();
        }

        if(encharco)
        {
            ptime += Time.deltaTime;
        }

        if(ptime > 0.1f && ptime < 0.2f && encharco)
        {
            p_splash.Play();
        }

        if(ptime > 0.2f)
        {
            ptime = 0;
            encharco = false;
        }

        if(ganar == false)
    	{
            vidaMax = timer.tiempo;
        
            vidaActual = timer.time;
        
            barradevida.fillAmount = vidaActual / vidaMax;
        
            float cons = 0.00075f;
        
            float divi = timer.tiempo / 10f;
        
            divisor = cons / divi;
        
            if(insta == true)
            {
                transform.localScale = new Vector3(0, 0 ,0);
            }
        
            if(transform.localScale.y >= 0.12 && cambio == false)
            {
                transform.localScale -= new Vector3(0, divisor ,0);
                daño.SetActive(false);
                mixer.SetFloat("Voldano", -80f);
            }
        
            if(transform.localScale.y >= 0.12 && cambio == true)
            {
                 transform.localScale -= new Vector3(0, divisor * 8,0);
                 daño.SetActive(true);
                 mixer.SetFloat("Voldano", scr_reinicio.Instance.vol);
            }
        }

		if(transform.position.y <= -6f)
			contador2 += Time.deltaTime;

		if(contador2 >= 3f)
        	SceneManager.LoadScene(SceneManager.GetActiveScene().name);
		

        if(flotando == true)
        {
            gcheck.SetActive(false);
        	cantdesp = 5;
        	profundidadantes = 2.7f;
        	multiplicador = Mathf.Clamp01(-transform.position.y / profundidadantes) * cantdesp;

			RB.AddForce(new Vector3(0f,Mathf.Abs(Physics.gravity.y) * multiplicador,0f), ForceMode2D.Force);

			contador += Time.deltaTime;
        }

        if(flotando2 == true)
        {
            gcheck.SetActive(false);
            cantdesp = i;
            profundidadantes = j;
            multiplicador = Mathf.Clamp01(-transform.position.y / profundidadantes) * cantdesp;

            RB.AddForce(new Vector3(0f,Mathf.Abs(Physics.gravity.y) * multiplicador,0f), ForceMode2D.Force);

            contador += Time.deltaTime;
        }

        if(contador > 0.1f && contador < 0.2f)
        {
            p_splash.Play();
        }


        if(contador >= 3f)
          SceneManager.LoadScene(SceneManager.GetActiveScene().name);


        if(transform.localScale.y < 0.12)
        {
            transform.localScale = new Vector3(0, 0 ,0);
        }

        if(ganar)
        {
            t_ganar += Time.deltaTime;
        }

        if(t_ganar > 2.9f)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
