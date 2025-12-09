using UnityEngine;
using UnityEngine.InputSystem;

public class Animations : MonoBehaviour
{
    public Animator anim;

    public GameObject Sword;
    public GameObject SwordOnBack;


    public bool CombatMode = false;
    public bool Attack = false;

    void Start()
    {
        if (anim == null) anim = GetComponent<Animator>();
        // establecer estado inicial de la espada según CombatMode
        if (Sword != null && SwordOnBack != null)
        {
            Sword.SetActive(CombatMode);
            SwordOnBack.SetActive(!CombatMode);
        }
    }

    void Update()
    {
        if (anim == null) return;

        // Activar al pulsar botón izquierdo
        if (Input.GetMouseButtonDown(0))
        {
            Attack = true;
            anim.SetTrigger("Attack");
            // resetear flag si sólo la usas para detectar el click
            Attack = false;
        }

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            CombatMode = !CombatMode;
            anim.SetBool("CombatMode", CombatMode);

            if (Sword != null) Sword.SetActive(CombatMode);
            if (SwordOnBack != null) SwordOnBack.SetActive(!CombatMode);
        }
    }
}
