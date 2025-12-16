using UnityEngine;
using TMPro;

public class Movimiento : MonoBehaviour
{
    private Rigidbody rb;

    public float velocidad = 1f;

    // Variables para el salto
    public float fuerzaSalto = 5f;
    public float longitud = 0.1f;
    public LayerMask suelo;
    private bool itsGrounded;

    // Guardar la posición inicial para resetear
    private Vector3 positionInicial;

    // Referencia a la cámara para movimiento relativo
    public Transform camara;

    public Animator anim;
    

    public int contador= 0;

    public TextMeshProUGUI textoContador;

    // Sprint
    public float sprintMultiplier = 2f;
    private bool itsrunning = false;

    private bool isCrouched = false;

    private GameObject[] objetosCosas;


    private void Start()
    {
        rb = GetComponent<Rigidbody>(); // Obtener el Rigidbody del jugador
        anim = GetComponent<Animator>();
        positionInicial = transform.position; // Guardar la posición inicial del jugador

        objetosCosas = GameObject.FindGameObjectsWithTag("Cosas");
    }

    private void Update()
    {
        // Detección de suelo con Raycast
        RaycastHit hit;
        itsGrounded = Physics.Raycast(transform.position, Vector3.down, out hit, longitud, suelo);
        anim.SetBool("IsGrounded", itsGrounded);

        // Salto
        if (itsGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            anim.SetTrigger("Jump");
            rb.AddForce(new Vector3(0f, fuerzaSalto, 0f), ForceMode.Impulse);
        }

        // Sprint con LeftShift (mantener pulsado)
        itsrunning = Input.GetKey(KeyCode.LeftShift);

        // Resetear posición con tecla R
        if (Input.GetKeyDown(KeyCode.R))
        {
            transform.position = positionInicial;
            rb.angularVelocity = Vector3.zero; // Detener la rotación
            rb.linearVelocity = Vector3.zero; // Detener el movimiento (corrección)

            contador = 0;
            textoContador.text = "Llevas: 0";

            // Reactivar todos los objetos "Cosas"
            foreach (GameObject obj in objetosCosas)
            {
                obj.SetActive(true);
            }

        }

        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            isCrouched = !isCrouched;   // Alterna entre true / false
            anim.SetBool("Crouch", isCrouched);
        }

    }

    private void FixedUpdate() 
    {
        float ejeX = Input.GetAxis("Horizontal"); // A = -1 (izq) D = +1 (der)
        float ejeY = Input.GetAxis("Vertical"); // S = -1 (atrás) W = +1 (adelante)

        // Normalizar el vector de entrada
        Vector3 input = new Vector3(ejeX, 0f, ejeY);
        if (input.magnitude > 1f) input.Normalize();

        // Movimiento relativo a la cámara, solo avanza hacia adelante
        Vector3 forward = camara.forward;
        forward.y = 0f; // ignorar componente vertical
        forward.Normalize();

        float forwardInput = ejeY;

       // Movimiento en el aire (mucho más fuerte para compensar el root motion)
        if (!itsGrounded)
        {
            if (forwardInput > 0.1f)
            {
                float airMultiplier = itsrunning ? sprintMultiplier : 1f;
                // este valor determina cuánto avanza en el salto//modificar para avanzar mas rapido en el aire
                float airSpeed = velocidad * 5f * airMultiplier;

                rb.MovePosition(rb.position + forward * forwardInput * airSpeed * Time.fixedDeltaTime);
            }
        }

        // el movimiento horizontal vendrá de las animaciones (OnAnimatorMove).
        if (anim != null)
        {   // Aplicar multiplicador de sprint cuando se mantiene Shift
            float multi = itsrunning ? sprintMultiplier : 1f;
            // Amplificar los parámetros del Blend Tree para alcanzar la posición del Run (PosY = 2 en tu Blend Tree)
            anim.SetFloat("VelX", ejeX * multi);
            anim.SetFloat("VelY", forwardInput * multi);

            
        }

        if (forwardInput > 0.1f)
        {
            Quaternion targetRot = Quaternion.LookRotation(forward);
            rb.MoveRotation(Quaternion.Slerp(rb.rotation, targetRot, Time.deltaTime * 10f));
        }
        
    }

        private void OnAnimatorMove()
    {
        if (anim == null || !anim.applyRootMotion) return;

        // deltaPosition/deltaRotation vienen de la animación.
        // Aplicamos solo el movimiento horizontal para no interferir con la gravedad.
        Vector3 delta = anim.deltaPosition;
        delta.y = 0f; // preserva gravedad/jump manejados por física

        // Aplicar multiplicador de sprint cuando se mantiene Shift
        float multi = itsrunning ? sprintMultiplier : 1f;
        delta *= multi;

        rb.MovePosition(rb.position + delta);
        rb.MoveRotation(rb.rotation * anim.deltaRotation);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.down * longitud);
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
