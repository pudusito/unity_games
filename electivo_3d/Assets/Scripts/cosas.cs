using UnityEngine;

public class cosas : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //rota el elemento una cantidad diferente en cada direccion y en cada intervalo de tiempo
        //Time.deltaTime es el tiempo transcurrido en cada frame(a 60fps seria 1/60);
        transform.Rotate(new Vector3(15, 30, 45) * Time.deltaTime);
        
    }
}
