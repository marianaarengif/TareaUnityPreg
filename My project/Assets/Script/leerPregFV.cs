using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using TMPro;
using models;
using Models;

public class leerPregFV : MonoBehaviour
{
    List<preguntasFV> listaPreguntasFaciles;
    List<preguntasFV> listaPreguntasDificiles;
    public List<preguntasFV> preguntasDisponibles; // Hacerlo público para acceder desde GameControllerTODO
    preguntasFV preguntaActual;

    public int rondaActual = 1; // Agregar la variable rondaActual

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
        separarDificultad();
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

    public void separarDificultad()
    {
        // Limpiar la lista de preguntas disponibles al inicio de cada ronda
        preguntasDisponibles.Clear();

        if (rondaActual == 1)
        {
            preguntasDisponibles.AddRange(listaPreguntasFaciles); // Preguntas fáciles en la primera ronda
            Debug.Log("Preguntas fáciles en la primera ronda FV");
        }
        else if (rondaActual == 2)
        {
            preguntasDisponibles.AddRange(listaPreguntasDificiles); // Preguntas difíciles en la segunda ronda
            Debug.Log("Preguntas difíciles en la segunda ronda FV");
        }
        
    }

    public void mostrarPreguntasFV()
    {
        // Seleccionar una pregunta aleatoria
        if (preguntasDisponibles.Count > 0)
        {
            int index = UnityEngine.Random.Range(0, preguntasDisponibles.Count);
            preguntaActual = preguntasDisponibles[index];
            preguntasDisponibles.RemoveAt(index);

            textPregunta.text = preguntaActual.PreguntaFV;

            preguntasRespondidas++;
            panelCorrecto.SetActive(false);
            panelIncorrecto.SetActive(false);
        }
        else
        {
            Debug.Log("No hay más preguntas de falso/verdadero disponibles.");
        }
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