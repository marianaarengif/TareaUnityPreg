using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Models;
using TMPro;

public class leerPreguntaAbierta : MonoBehaviour
{
    List<PreguntaAbierta> listaPreguntasFaciles;
    List<PreguntaAbierta> listaPreguntasDificiles;
    List<PreguntaAbierta> preguntasDisponibles;
    PreguntaAbierta preguntaActual;

    int rondaActual = 1;
    int preguntasRespondidas = 0;
    const int preguntasPorRonda = 3;

    public TextMeshProUGUI textPregunta;
    public TextMeshProUGUI textRespuesta;
    public GameObject panelRespuesta;

    void Start()
    {
        listaPreguntasFaciles = new List<PreguntaAbierta>();
        listaPreguntasDificiles = new List<PreguntaAbierta>();
        preguntasDisponibles = new List<PreguntaAbierta>();
        LecturaPreguntasAbiertas();
        mostrarPreguntasAbiertas();
    }

    void LecturaPreguntasAbiertas()
    {
        try
        {
            StreamReader sr = new StreamReader("Assets/Files/ArchivoPreguntasAbiertas.txt");
            string lineaLeida;
            while ((lineaLeida = sr.ReadLine()) != null)
            {
                string[] lineaPartida = lineaLeida.Split("-");
                string pregunta = lineaPartida[0];
                string respuesta = lineaPartida[1];
                string versiculo = lineaPartida[2];
                string dificultad = lineaPartida[3].Trim();

                PreguntaAbierta objPA = new PreguntaAbierta(pregunta, respuesta, versiculo, dificultad);
                if (dificultad.ToLower() == "facil")
                {
                    listaPreguntasFaciles.Add(objPA);
                }
                else if (dificultad.ToLower() == "dificil")
                {
                    listaPreguntasDificiles.Add(objPA);
                }
            }
            sr.Close();
            Debug.Log("El tamaño de la lista de preguntas fáciles es " + listaPreguntasFaciles.Count);
            Debug.Log("El tamaño de la lista de preguntas difíciles es " + listaPreguntasDificiles.Count);
        }
        catch (Exception e)
        {
            Debug.Log("ERROR!!!!! " + e.ToString());
        }
    }

    public void mostrarPreguntasAbiertas()
    {
        if (preguntasRespondidas >= preguntasPorRonda)
        {
            if (rondaActual == 1)
            {
                rondaActual = 2;
                preguntasRespondidas = 0;
                preguntasDisponibles.Clear();
                preguntasDisponibles.AddRange(listaPreguntasDificiles);
            }
            else
            {
                Debug.Log("No hay más preguntas disponibles.");
                return;
            }
        }

        if (preguntasDisponibles.Count == 0)
        {
            if (rondaActual == 1)
            {
                preguntasDisponibles.AddRange(listaPreguntasFaciles);
            }
            else
            {
                preguntasDisponibles.AddRange(listaPreguntasDificiles);
            }
        }

        int index = UnityEngine.Random.Range(0, preguntasDisponibles.Count);
        preguntaActual = preguntasDisponibles[index];
        preguntasDisponibles.RemoveAt(index);

        
        textPregunta.text = preguntaActual.PreguntaAbiertaTexto;

        preguntasRespondidas++;
    }

    public void mostrarRespuesta()
    {
        
        textRespuesta.text = preguntaActual.Respuesta;
        panelRespuesta.SetActive(true);
    }

    public void siguientePregunta()
    {
        panelRespuesta.SetActive(false);
        mostrarPreguntasAbiertas();
    }
}

