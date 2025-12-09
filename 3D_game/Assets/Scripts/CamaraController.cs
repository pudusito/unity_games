using UnityEngine;

public class CamaraController : MonoBehaviour
{
    //obtener el jugador
    public GameObject jugador;
    private Vector3 offset;

    public float mouseSensitivity = 3.5f;
    public float minPitch = -45f;
    public float maxPitch = 75f;

    private float yaw = 0f;   // rotación en Y (gira al jugador)
    private float pitch = 10f; // rotación en X (mira arriba/abajo)

    public float gizmoRayLength = 2f;


    void Start()
    {
        offset = transform.position - jugador.transform.position;
        Vector3 e = transform.eulerAngles;
        yaw = e.y;
        pitch = e.x;

        // bloquear cursor al iniciar (opcional)
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    private void LateUpdate()
    {

        // Lectura del ratón
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        yaw += mouseX;
        pitch -= mouseY;
        pitch = Mathf.Clamp(pitch, minPitch, maxPitch);

        // Calcular la nueva posición y rotación de la cámara(para orvientarla hacia el jugador)
        Quaternion rot = Quaternion.Euler(pitch, yaw, 0f);
        transform.position = jugador.transform.position + rot * offset;
        transform.rotation = rot;
    }

//traza gizmo en linea desde jugador a donde apunta
    void OnDrawGizmos()
    {
        if (jugador == null)
            return;

        Gizmos.color = Color.cyan;
        Gizmos.DrawLine(transform.position, jugador.transform.position);
        Gizmos.color = Color.yellow;
        Gizmos.DrawRay(transform.position, transform.forward * gizmoRayLength);
    }
}
