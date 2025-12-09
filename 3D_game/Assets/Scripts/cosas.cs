using UnityEngine;

public class Cosas : MonoBehaviour
{
    public Vector3 velocidadRotacion = new Vector3(15, 30, 45);

    void Update()
    {
        //rota el elemento una cantidad diferente en cada direccion y en cada intervalo de tiempo
        //Time.deltaTime es el tiempo transcurrido en cada frame(a 60fps seria 1/60);
       /* transform.Rotate(new Vector3(15, 30, 45) * Time.deltaTime); */
        transform.Rotate(velocidadRotacion * Time.deltaTime);
    }
}

