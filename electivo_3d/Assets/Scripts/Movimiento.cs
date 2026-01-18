using UnityEngine;
using TMPro;


public class Movimiento : MonoBehaviour
{
    private Rigidbody rb;

    public float velocidad = 1f;

    public int contador= 0;

    public TextMeshProUGUI textoContador;


    private void Start()
    {
        rb= GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
    //inicio movimiento
    //para sincronizar los frame del motor de la fisica
    //variables para capturar el movimiento horizontal y vertical en nuestro teclado
    float movimientoH= Input.GetAxis("Horizontal");
    float movimientoV= Input.GetAxis("Vertical");

    Vector3 movimiento= new Vector3(movimientoH,0.0f,movimientoV)* 10; 
    rb.AddForce(movimiento*velocidad);
    //fin movimiento
    }

    private void OnTriggerEnter(Collider cubito)
    {
        if(cubito.gameObject.CompareTag("Cosas"))
        {
            cubito.gameObject.SetActive(false);
            Debug.Log("Recogidos: " + ++contador);
            textoContador.text= "Llevas: " +contador.ToString();
        }
    }




}
