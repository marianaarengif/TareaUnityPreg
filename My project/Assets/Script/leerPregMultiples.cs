using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using TMPro;
using models;

public class leerPregMultiples : MonoBehaviour
{
      
    }

  

 

    public void LecturaPreguntasMultiples()
    {
        try
        {
            StreamReader sr1 = new StreamReader("Assets/Script/PM/PreguntaMultiple.cs");
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
}