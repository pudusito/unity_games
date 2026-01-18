using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class contador_ui : MonoBehaviour
{
    public Money money;                   // arrastra aquí el GameObject que tiene el script Money (tu MoneyManager)
    public TextMeshProUGUI counterText;   // arrastra aquí el componente Text dentro del Canvas

    public Button btnpausa;

    public GameObject ui;
    public GameObject menu;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (money == null || counterText == null) return;
        counterText.text = money.MoneyCount.ToString();
    }

    public void PausarJuego()
    {
        Time.timeScale = 0f; // Pausa el juego
        ui.SetActive(false);
        menu.SetActive(true);

    }
}
