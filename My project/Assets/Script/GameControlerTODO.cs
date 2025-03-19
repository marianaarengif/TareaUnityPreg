using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameControllerTODO : MonoBehaviour
{
    public GameObject panelPreguntasMultiples;
    public GameObject panelPreguntasAbiertas;
    public GameObject panelFalsoVerdadero;
    public GameObject panelRonda2;

    private int rondaActual = 1;
    private int preguntasRespondidas = 0;
    private const int preguntasPorRonda = 9; // 3 de cada tipo

    private List<GameObject> panelesFaciles;
    private List<GameObject> panelesDificiles;
    private List<GameObject> panelesDisponibles;

    void Start()
    {
        panelesFaciles = new List<GameObject> {
            panelPreguntasMultiples,
            panelPreguntasAbiertas,
            panelFalsoVerdadero
        };

        panelesDificiles = new List<GameObject> {
            panelPreguntasMultiples,
            panelPreguntasAbiertas,
            panelFalsoVerdadero
        };

        panelesDisponibles = new List<GameObject>(panelesFaciles);

        // Iniciar la primera ronda
        MostrarPanelAleatorio();
    }

    void MostrarPanelAleatorio()
    {
        if (preguntasRespondidas >= preguntasPorRonda)
        {
            if (rondaActual == 1)
            {
                rondaActual = 2;
                preguntasRespondidas = 0;
                panelesDisponibles = new List<GameObject>(panelesDificiles);
                panelRonda2.SetActive(true);
                Invoke("IniciarRondaDificil", 3); // Esperar 3 segundos antes de iniciar la ronda difícil
            }
            else
            {
                Debug.Log("Juego completado.");
                return;
            }
        }
        else
        {
            if (panelesDisponibles.Count == 0)
            {
                panelesDisponibles = rondaActual == 1 ? new List<GameObject>(panelesFaciles) : new List<GameObject>(panelesDificiles);
            }

            int index = UnityEngine.Random.Range(0, panelesDisponibles.Count);
            GameObject panelSeleccionado = panelesDisponibles[index];
            panelesDisponibles.RemoveAt(index);

            // Desactivar todos los paneles antes de activar el seleccionado
            DesactivarTodosLosPaneles();
            panelSeleccionado.SetActive(true);

            // Llamar al método para mostrar la pregunta en el script correspondiente
            if (panelSeleccionado == panelPreguntasMultiples)
            {
                panelPreguntasMultiples.GetComponent<leerPregMultiples>().mostrarPreguntasMultiples();
            }
            else if (panelSeleccionado == panelPreguntasAbiertas)
            {
                panelPreguntasAbiertas.GetComponent<leerPreguntaAbierta>().mostrarPreguntasAbiertas();
            }
            else if (panelSeleccionado == panelFalsoVerdadero)
            {
                panelFalsoVerdadero.GetComponent<leerPregFV>().mostrarPreguntasFV();
            }

            preguntasRespondidas++;
        }
    }

    void IniciarRondaDificil()
    {
        panelRonda2.SetActive(false);
        MostrarPanelAleatorio();
    }

    void DesactivarTodosLosPaneles()
    {
        panelPreguntasMultiples.SetActive(false);
        panelPreguntasAbiertas.SetActive(false);
        panelFalsoVerdadero.SetActive(false);
    }

    public void SiguientePregunta()
    {
        MostrarPanelAleatorio();
    }
}

