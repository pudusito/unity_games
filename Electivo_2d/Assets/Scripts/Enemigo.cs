using UnityEngine;

public class Enemigo : MonoBehaviour
{
public Rigidbody2D rb2d;
    
    public float velocidadDeMovimiento;
    public LayerMask CapaAbajo;
    public LayerMask CapaEnFrente;

    public float distanciaAbajo;
    public float distanciaEnFrente;

    public Transform controladorAbajo;
    public Transform controladorEnFrente;
    public bool informacionAbajo;
    public bool informacionEnFrente;

    private bool mirandoADerecha = true;

    public void Update()
    {
        rb2d.linearVelocity = new Vector2(velocidadDeMovimiento, rb2d.linearVelocity.y);

        informacionEnFrente = Physics2D.Raycast(controladorEnFrente.position, transform.right, distanciaEnFrente, CapaEnFrente);
        informacionAbajo = Physics2D.Raycast(controladorAbajo.position, transform.up * -1, distanciaAbajo, CapaAbajo);

        if (informacionEnFrente || !informacionAbajo)
        {
            Girar();
        }
    }

    private void Girar()
    {
        mirandoADerecha = !mirandoADerecha;
        transform.eulerAngles = new Vector3(0, mirandoADerecha ? 0 : 180, 0);
        velocidadDeMovimiento *= -1;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(controladorEnFrente.position, controladorEnFrente.position + transform.right * distanciaEnFrente);
        Gizmos.DrawLine(controladorAbajo.position, controladorAbajo.position + transform.up * -1 * distanciaAbajo);
    }
}


