using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Models;
using TMPro;
using UnityEngine.UI;

public class leerPregFV : MonoBehaviour
{
    List<preguntasFV> listaPFVF;
    List<preguntasFV> listaPFVD;

    public TextMeshProUGUI textPregunta;
    public GameObject panelCorrecto;
    public GameObject panelIncorrecto;

    void Start()
    {


        listaPFVF = new List<preguntasFV>();

        listaPFVD = new List<preguntasFV>();
        LecturaPreguntasFV();

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
                    listaPFVF.Add(objFV);
                }
                else if (dificultad.ToLower() == "dificil")
                {
                    listaPFVD.Add(objFV);
                }
            }
            sr.Close();
            Debug.Log("El tamaño de la lista de preguntas fáciles es " + listaPFVF.Count);
            Debug.Log("El tamaño de la lista de preguntas difíciles es " + listaPFVD.Count);
        }
        catch (Exception e)
        {
            Debug.Log("ERROR!!!!! " + e.ToString());
        }
    }

}