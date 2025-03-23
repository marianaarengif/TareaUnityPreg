using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using TMPro;
using models;

public class leerPregMultiples : MonoBehaviour
{
    string lineaLeida = "";
    List<PreguntaMultiple> listaPMF;
    List<PreguntaMultiple> listaPMD;
    List<PreguntaMultiple> preguntasDisponibles;
    PreguntaMultiple preguntaActual;

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
        mostrarPreguntasMultiples();
    }
    //multiples
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

    public void mostrarPreguntasMultiples()
    {
        if (preguntasDisponibles.Count == 0)
        {
            preguntasDisponibles.AddRange(listaPMF);
        }

        int index = UnityEngine.Random.Range(0, preguntasDisponibles.Count);
        preguntaActual = preguntasDisponibles[index];
        preguntasDisponibles.RemoveAt(index);

        textPregunta.text = preguntaActual.Pregunta;
        textResp1.text = preguntaActual.Respuesta1;
        textResp2.text = preguntaActual.Respuesta2;
        textResp3.text = preguntaActual.Respuesta3;
        textResp4.text = preguntaActual.Respuesta4;
        respuestaPM = preguntaActual.RespuestaCorrecta;
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
}
