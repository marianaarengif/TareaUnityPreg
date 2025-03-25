using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Models;
using TMPro;

public class leerPreguntaAbierta : MonoBehaviour //Fin
{
    public GameControllerTODO gameController;
    List<PreguntaAbierta> listaPreguntasFaciles;
    List<PreguntaAbierta> listaPreguntasDificiles;
    public List<PreguntaAbierta> preguntasDisponibles; // Hacerlo público para acceder desde GameControllerTODO
    PreguntaAbierta preguntaActual;

    public int rondaActual = 1; // Agregar la variable rondaActual

    public TextMeshProUGUI textPregunta;
    public TextMeshProUGUI textRespuesta;
    public GameObject panelRespuesta;
    public int contadorRespuestasCorrectas = 0;
    public int contadorRespuestasIncorrectas = 0;

    void Start()
    {
        listaPreguntasFaciles = new List<PreguntaAbierta>();
        listaPreguntasDificiles = new List<PreguntaAbierta>();
        preguntasDisponibles = new List<PreguntaAbierta>();

        LecturaPreguntasAbiertas();
        separarDificultad();
        mostrarPreguntasAbiertas();
        panelRespuesta.SetActive(false);
        gameController = GameObject.Find("GameController").GetComponent<GameControllerTODO>();
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

    public void separarDificultad()
    {
        // Limpiar la lista de preguntas disponibles al inicio de cada ronda
        preguntasDisponibles.Clear();

        if (rondaActual == 1)
        {
            preguntasDisponibles.AddRange(listaPreguntasFaciles); // Preguntas fáciles en la primera ronda
            Debug.Log("Preguntas fáciles en la primera ronda Abiertas");
        }
        else if (rondaActual == 2)
        {
            preguntasDisponibles.AddRange(listaPreguntasDificiles); // Preguntas difíciles en la segunda ronda
            Debug.Log("Preguntas difíciles en la segunda ronda Abiertas");  
        }
    }

    public void mostrarPreguntasAbiertas()
    {

        // Seleccionar una pregunta aleatoria
        if (preguntasDisponibles.Count > 0)
        {
            int index = UnityEngine.Random.Range(0, preguntasDisponibles.Count);
            preguntaActual = preguntasDisponibles[index];
            preguntasDisponibles.RemoveAt(index);

            textPregunta.text = preguntaActual.PreguntaAbiertaTexto;
            panelRespuesta.SetActive(false);
        }
        else
        {
            Debug.Log("No hay más preguntas abiertas disponibles.");
        }
    }


    public void comprobarRespuesta(string respuestaUsuario)
    {
        // Compara la respuesta del usuario con la respuesta correcta (ignorando espacios y mayúsculas/minúsculas)
        bool esCorrecta = respuestaUsuario.Trim().Equals(preguntaActual.Respuesta, System.StringComparison.OrdinalIgnoreCase);

        if (esCorrecta)
        {
            // Puedes mostrar un mensaje o activar un panel de respuesta correcta
            Debug.Log("Respuesta correcta en pregunta abierta");
            gameController.contadorRespuestasCorrectas += 1;
        }
        else
        {
            Debug.Log("Respuesta incorrecta en pregunta abierta");
            gameController.contadorRespuestasIncorrectas += 1;
        }
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
} //hola guardar cambio
//comentario