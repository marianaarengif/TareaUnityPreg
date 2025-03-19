using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Models
{
    public class PreguntaAbierta
    {
        private string preguntaAbierta;
        private string respuesta;
        private string versiculo;
        private string dificultad;

        public PreguntaAbierta()
        {
        }

        public PreguntaAbierta(string preguntaAbierta, string respuesta, string versiculo, string dificultad)
        {
            this.preguntaAbierta = preguntaAbierta;
            this.respuesta = respuesta;
            this.versiculo = versiculo;
            this.dificultad = dificultad;
        }

        public string PreguntaAbiertaTexto { get => preguntaAbierta; set => preguntaAbierta = value; }
        public string Respuesta { get => respuesta; set => respuesta = value; }
        public string Versiculo { get => versiculo; set => versiculo = value; }
        public string Dificultad { get => dificultad; set => dificultad = value; }
    }
}


