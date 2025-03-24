using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using TMPro;
using models;

public class leerPregMultiples : MonoBehaviour //Fin
{
    string lineaLeida = "";
    List<PreguntaMultiple> listaPMF;
    List<PreguntaMultiple> listaPMD;
    public List<PreguntaMultiple> preguntasDisponibles; // Hacerlo público para acceder desde GameControllerTODO
    PreguntaMultiple preguntaActual;

    public int rondaActual = 1; // Agregar la variable rondaActual

    string respuestaPM;

    public TextMeshProUGUI textPregunta;
    public TextMeshProUGUI textResp1;
    public TextMeshProUGUI textResp2;
    public TextMeshProUGUI textResp3;
    public TextMeshProUGUI textResp4;

    public GameObject panelCorrecto;
    public GameObject panelIncorrecto;

    void Start()
    {
        listaPMF = new List<PreguntaMultiple>();
        listaPMD = new List<PreguntaMultiple>();
        preguntasDisponibles = new List<PreguntaMultiple>();

        LecturaPreguntasMultiples();
        separarDificultad();
        mostrarPreguntasMultiples();
        panelCorrecto.SetActive(false);
        panelIncorrecto.SetActive(false);
    }

    public void LecturaPreguntasMultiples()
    {
        try
        {
            StreamReader sr1 = new StreamReader("Assets/Files/ArchivoPreguntasM.txt");
            while ((lineaLeida = sr1.ReadLine()) != null)
            {
                string[] lineaPartida = lineaLeida.Split("-");
                string pregunta = lineaPartida[0];
                string respuesta1 = lineaPartida[1];
                string respuesta2 = lineaPartida[2];
                string respuesta3 = lineaPartida[3];
                string respuesta4 = lineaPartida[4];
                string respuestaCorrecta = lineaPartida[5];
                string versiculo = lineaPartida[6];
                string dificultad = lineaPartida[7].Trim();

                PreguntaMultiple objPM = new PreguntaMultiple(pregunta, respuesta1, respuesta2, respuesta3, respuesta4, respuestaCorrecta, versiculo, dificultad);
                if (dificultad.ToLower() == "facil")
                {
                    listaPMF.Add(objPM);
                }
                else if (dificultad.ToLower() == "dificil")
                {
                    listaPMD.Add(objPM);
                }
            }
            sr1.Close();
            Debug.Log("El tamaño de la lista de preguntas fáciles es " + listaPMF.Count);
            Debug.Log("El tamaño de la lista de preguntas difíciles es " + listaPMD.Count);
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
            preguntasDisponibles.AddRange(listaPMF); // Preguntas fáciles en la primera ronda
            Debug.Log("Ronda 1 Múltiples");
        }
        else if (rondaActual == 2)
        {
            preguntasDisponibles.AddRange(listaPMD); // Preguntas difíciles en la segunda ronda
            Debug.Log("Ronda 2 Múltiples");
        }
    }

    public void mostrarPreguntasMultiples()
    {


        // Seleccionar una pregunta aleatoria
        if (preguntasDisponibles.Count > 0)
        {
            int index = UnityEngine.Random.Range(0, preguntasDisponibles.Count);
            preguntaActual = preguntasDisponibles[index];
            preguntasDisponibles.RemoveAt(index);

            textPregunta.text = preguntaActual.Pregunta;
            textResp1.text = preguntaActual.Respuesta1;
            textResp2.text = preguntaActual.Respuesta2;
            textResp3.text = preguntaActual.Respuesta3;
            textResp4.text = preguntaActual.Respuesta4;
            respuestaPM = preguntaActual.RespuestaCorrecta;

            panelCorrecto.SetActive(false);
            panelIncorrecto.SetActive(false);
        }
        else
        {
            Debug.Log("No hay más preguntas múltiples disponibles.");
        }
    }

    public void comprobarRespuesta(TextMeshProUGUI respuestaSeleccionada)
    {
        if (respuestaSeleccionada.text.Equals(respuestaPM))
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
        mostrarPreguntasMultiples();
    }
} //cambios hoy