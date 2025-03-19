using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Models;
using TMPro;

public class leerPregFV : MonoBehaviour
{
    List<preguntasFV> listaPreguntasFaciles;
    List<preguntasFV> listaPreguntasDificiles;
    List<preguntasFV> preguntasDisponibles;
    preguntasFV preguntaActual;

    int rondaActual = 1;
    int preguntasRespondidas = 0;
    const int preguntasPorRonda = 3;

    public TextMeshProUGUI textPregunta;
    public GameObject panelCorrecto;
    public GameObject panelIncorrecto;

    void Start()
    {

        listaPreguntasFaciles = new List<preguntasFV>();
        listaPreguntasDificiles = new List<preguntasFV>();
        preguntasDisponibles = new List<preguntasFV>();
        LecturaPreguntasFV();
        mostrarPreguntasFV();
        panelCorrecto.SetActive(false);
        panelIncorrecto.SetActive(false);       
    }

    void LecturaPreguntasFV()
    {
        try
        {
            StreamReader sr = new StreamReader("Assets/Files/preguntasFalso_Verdadero.txt");
            string lineaLeida;
            while ((lineaLeida = sr.ReadLine()) != null)
            {
                string[] lineaPartida = lineaLeida.Split("-");
                string pregunta = lineaPartida[0];
                bool respuesta = bool.Parse(lineaPartida[1]);
                string versiculo = lineaPartida[2];
                string dificultad = lineaPartida[3].Trim();

                preguntasFV objFV = new preguntasFV(pregunta, respuesta, versiculo, dificultad);
                if (dificultad.ToLower() == "facil")
                {
                    listaPreguntasFaciles.Add(objFV);
                }
                else if (dificultad.ToLower() == "dificil")
                {
                    listaPreguntasDificiles.Add(objFV);
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

    public void mostrarPreguntasFV()
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

        // Mostrar la pregunta actual en la UI
        textPregunta.text = preguntaActual.PreguntaFV;

        preguntasRespondidas++;
    }

    public void comprobarRespuesta(bool respuestaSeleccionada)
    {
        if (respuestaSeleccionada == preguntaActual.Respuesta)
        {
            panelCorrecto.SetActive(true);
            panelIncorrecto.SetActive(false);
        }
        else
        {
            panelCorrecto.SetActive(false);
            panelIncorrecto.SetActive(true);
        }
    }

    public void siguientePregunta()
    {
        panelCorrecto.SetActive(false);
        panelIncorrecto.SetActive(false);
        mostrarPreguntasFV();
    }
}


