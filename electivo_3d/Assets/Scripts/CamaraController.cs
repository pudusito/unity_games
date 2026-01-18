using UnityEngine;

public class CamaraController : MonoBehaviour
{
    //obtener el jugador
    public GameObject jugador;

    private Vector3 offset;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        offset = transform.position - jugador.transform.position;
    }

    // Update is called once per frame
    private void LateUpdate()
    {
        transform.position = jugador.transform.position + offset;
    }
}
