using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameControllerTODO : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> ListaDeControles;
    GameObject controllSelected;

    public GameObject panelPreguntasMultiples;
    public GameObject panelPreguntasAbiertas;
    public GameObject panelFalsoVerdadero;
    public GameObject panelRonda2;

    private int rondaActual = 1;
    private int preguntasRespondidas = 0;
    private const int preguntasPorRonda = 9; // 3 de cada tipo

    private List<PanelScriptPair> panelesFaciles;
    private List<PanelScriptPair> panelesDificiles;
    private List<PanelScriptPair> panelesDisponibles;

    void Start()
    {
        panelesFaciles = new List<PanelScriptPair> {
            new PanelScriptPair(panelPreguntasMultiples, panelPreguntasMultiples.GetComponent<leerPregMultiples>()),
            new PanelScriptPair(panelPreguntasAbiertas, panelPreguntasAbiertas.GetComponent<leerPreguntaAbierta>()),
            new PanelScriptPair(panelFalsoVerdadero, panelFalsoVerdadero.GetComponent<leerPregFV>())
        };

        panelesDificiles = new List<PanelScriptPair> {
            new PanelScriptPair(panelPreguntasMultiples, panelPreguntasMultiples.GetComponent<leerPregMultiples>()),
            new PanelScriptPair(panelPreguntasAbiertas, panelPreguntasAbiertas.GetComponent<leerPreguntaAbierta>()),
            new PanelScriptPair(panelFalsoVerdadero, panelFalsoVerdadero.GetComponent<leerPregFV>())
        };

        panelesDisponibles = new List<PanelScriptPair>(panelesFaciles);

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
                panelesDisponibles = new List<PanelScriptPair>(panelesDificiles);
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
                panelesDisponibles = rondaActual == 1 ? new List<PanelScriptPair>(panelesFaciles) : new List<PanelScriptPair>(panelesDificiles);
            }

            int index = UnityEngine.Random.Range(0, panelesDisponibles.Count);
            PanelScriptPair panelScriptSeleccionado = panelesDisponibles[index];
            panelesDisponibles.RemoveAt(index);

            // Desactivar todos los paneles antes de activar el seleccionado
            DesactivarTodosLosPaneles();
            panelScriptSeleccionado.panel.SetActive(true);

            // Llamar al método para mostrar la pregunta en el script correspondiente
            if (panelScriptSeleccionado.script is leerPregMultiples)
            {
                ((leerPregMultiples)panelScriptSeleccionado.script).mostrarPreguntasMultiples();
            }
            else if (panelScriptSeleccionado.script is leerPreguntaAbierta)
            {
                ((leerPreguntaAbierta)panelScriptSeleccionado.script).mostrarPreguntasAbiertas();
            }
            else if (panelScriptSeleccionado.script is leerPregFV)
            {
                ((leerPregFV)panelScriptSeleccionado.script).mostrarPreguntasFV();
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

[System.Serializable]
public class PanelScriptPair
{
    public GameObject panel;
    public MonoBehaviour script;

    public PanelScriptPair(GameObject panel, MonoBehaviour script)
    {
        this.panel = panel;
        this.script = script;
    }
}