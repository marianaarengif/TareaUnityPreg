using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Models
{
    
    public class preguntasFV
    {
        private string preguntaFV;
        private bool respuesta;
        private string versiculo;
        private string dificultad;

        public preguntasFV()
        {
        }

        public preguntasFV(string preguntaFV, bool respuesta, string versiculo, string dificultad)
        {
            this.preguntaFV = preguntaFV;
            this.respuesta = respuesta;
            this.versiculo = versiculo;
            this.dificultad = dificultad;
        }

        public string PreguntaFV { get => preguntaFV; set => preguntaFV = value; }
        public bool Respuesta { get => respuesta; set => respuesta = value; }
        public string Versiculo { get => versiculo; set => versiculo = value; }
        public string Dificultad { get => dificultad; set => dificultad = value; }


    }
}
