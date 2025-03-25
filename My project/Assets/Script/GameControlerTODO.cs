using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.PackageManager;
using UnityEngine;

public class GameControllerTODO : MonoBehaviour //Fin
{
    [SerializeField] private List<GameObject> listaControllers; // Controladores de preguntas
    private GameObject controlSelected;
    public GameObject panelAbiertas;
    public GameObject panelMultiples;
    public GameObject panelFV;
    public GameObject panelRonda2;
    public GameObject PanelFacil;

    public GameObject panelResultados;
    public TextMeshProUGUI textAciertos;
    public TextMeshProUGUI textErrores;

    // CONTADORES DE RESPUESTAS
    private int aciertos = 0;
    private int errores = 0;


    public int rondaActual = 1;
    private int preguntasRespondidas = 0;
    
    private const int preguntasPorRonda = 9; // 3 de cada tipo

    void Start()
    {
        PanelFacil.SetActive(true);
        Invoke("apagarpanel", 4);

        // Iniciar la primera ronda
        SelectQuestion();
    }

    void Update()
    {
        
    }

     void apagarpanel()
    {
        PanelFacil.SetActive(false);
        
        SelectQuestion();
        
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
                controlMulti.rondaActual = rondaActual; // Pasar la ronda actual
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
                controlFV.rondaActual = rondaActual; // Pasar la ronda actual
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
                controlAbiertas.rondaActual = rondaActual; // Pasar la ronda actual
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
                    preguntasRespondidas = 0;
                    panelRonda2.SetActive(true);
                    Invoke("IniciarRondaDificil", 4);
                }
                else
                {
                    Debug.Log("Todas las preguntas de todos los tipos se han terminado.");



                    MostrarResultados();
                    return;
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
        rondaActual = 2;
        foreach  (GameObject control in listaControllers)
        {
            if (control.GetComponent<leerPregMultiples>() != null)
            {
                leerPregMultiples controlMulti = control.GetComponent<leerPregMultiples>();
                controlMulti.rondaActual = rondaActual; // Pasar la ronda actual
                controlMulti.separarDificultad(); // Pasar la ronda actual
            }
            else if (control.GetComponent<leerPregFV>() != null)
            {
                leerPregFV controlFV = control.GetComponent<leerPregFV>();
                controlFV.rondaActual = rondaActual; // Pasar la ronda actual
                controlFV.separarDificultad(); // Pasar la ronda actual
            }
            else if (control.GetComponent<leerPreguntaAbierta>() != null)
            {
                leerPreguntaAbierta controlAbiertas = control.GetComponent<leerPreguntaAbierta>();
                controlAbiertas.rondaActual = rondaActual; // Pasar la ronda actual 
                controlAbiertas.separarDificultad(); // Pasar la ronda actual
            }
        }
        panelRonda2.SetActive(false);
        SelectQuestion();
    }

    public void RegistrarRespuesta(bool respuestaCorrecta)
    {
        if (respuestaCorrecta)
        {
            aciertos++;
        }
        else
        {
            errores++;
        }
    }



    void MostrarResultados()
    {
        // Desactivar todos los paneles de preguntas
        panelAbiertas.SetActive(false);
        panelMultiples.SetActive(false);
        panelFV.SetActive(false);
        panelRonda2.SetActive(false);
        PanelFacil.SetActive(false);
        // Activar el panel de resultados
        panelResultados.SetActive(true);
        textAciertos.text = "Aciertos: " + aciertos;
        textErrores.text = "Errores: " + errores;
    }
} //carga para los cambios 
//comentario 

