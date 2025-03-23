using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControllerTODO : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> listaControllers; // Controladores de preguntas
    private GameObject controlSelected;
    public GameObject panelAbiertas;
    public GameObject panelMultiples;
    public GameObject panelFV;
    public GameObject panelRonda2;

    private int rondaActual = 1;
    private int preguntasRespondidas = 0;
    private const int preguntasPorRonda = 9; // 3 de cada tipo

    void Start()
    {
        // Iniciar la primera ronda
        SelectQuestion();
    }

    void Update()
    {
    }

    public void SelectQuestion()
    {
        // Verificar si todavía hay controladores disponibles
        if (listaControllers.Count > 0)
        {
            System.Random random = new System.Random();
            int numero = random.Next(0, listaControllers.Count);
            controlSelected = listaControllers[numero];

            if (controlSelected.GetComponent<leerPregMultiples>() != null)
            {
                leerPregMultiples controlMulti = controlSelected.GetComponent<leerPregMultiples>();
                if (controlMulti.preguntasDisponibles.Count > 0)
                {
                    panelFV.SetActive(false);
                    panelAbiertas.SetActive(false);
                    controlMulti.mostrarPreguntasMultiples();
                    panelMultiples.SetActive(true);
                }
                else
                {
                    // Si no hay más preguntas múltiples, eliminar el controlador
                    listaControllers.Remove(controlSelected);
                    SelectQuestion(); // Intentar seleccionar otra pregunta
                    return;
                }
            }
            else if (controlSelected.GetComponent<leerPregFV>() != null)
            {
                leerPregFV controlFV = controlSelected.GetComponent<leerPregFV>();
                if (controlFV.preguntasDisponibles.Count > 0)
                {
                    panelAbiertas.SetActive(false);
                    panelMultiples.SetActive(false);
                    controlFV.mostrarPreguntasFV();
                    panelFV.SetActive(true);
                }
                else
                {
                    // Si no hay más preguntas FV, eliminar el controlador
                    listaControllers.Remove(controlSelected);
                    SelectQuestion(); // Intentar seleccionar otra pregunta
                    return;
                }
            }
            else if (controlSelected.GetComponent<leerPreguntaAbierta>() != null)
            {
                leerPreguntaAbierta controlAbiertas = controlSelected.GetComponent<leerPreguntaAbierta>();
                if (controlAbiertas.preguntasDisponibles.Count > 0)
                {
                    panelFV.SetActive(false);
                    panelMultiples.SetActive(false);
                    controlAbiertas.mostrarPreguntasAbiertas();
                    panelAbiertas.SetActive(true);
                }
                else
                {
                    // Si no hay más preguntas abiertas, eliminar el controlador
                    listaControllers.Remove(controlSelected);
                    SelectQuestion(); // Intentar seleccionar otra pregunta
                    return;
                }
            }

            preguntasRespondidas++;

            if (preguntasRespondidas >= preguntasPorRonda)
            {
                if (rondaActual == 1)
                {
                    rondaActual = 2;
                    preguntasRespondidas = 0;
                    panelRonda2.SetActive(true);
                    Invoke("IniciarRondaDificil", 3); // Esperar 3 segundos antes de iniciar la ronda difícil
                }
                else
                {
                    Debug.Log("Todas las preguntas de todos los tipos se han terminado.");
                }
            }
        }
        else
        {
            Debug.Log("Todas las preguntas de todos los tipos se han terminado.");
        }
    }

    void IniciarRondaDificil()
    {
        panelRonda2.SetActive(false);
        SelectQuestion();
    }
}






