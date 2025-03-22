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
   

   

    public TextMeshProUGUI textPregunta;
    public TextMeshProUGUI textRespuesta;
    public GameObject panelRespuesta;

    void Start()
    {
        listaPreguntasFaciles = new List<PreguntaAbierta>();
        listaPreguntasDificiles = new List<PreguntaAbierta>();
        LecturaPreguntasAbiertas();
       
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
}

