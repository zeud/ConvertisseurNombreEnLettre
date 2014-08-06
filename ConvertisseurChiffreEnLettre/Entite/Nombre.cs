﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace ConvertisseurNombreEnLettre
{
    public class Nombre
    {
        // 21, 31, 41, 51, 61
        private static readonly int[] DizaineAvecExceptionPourUneUnite = { 2, 3, 4, 5, 6 };
        private readonly int _nombre;

        public Nombre(int nombre)
        {
            _nombre = nombre;
            DecomposerLeNombre();
        }

        public int NombreDeMillion { get; private set; }

        public int NombreDeMillier { get; private set; }

        public int NombreDeCentaine { get; private set; }

        public int NombreDeDizaine { get; private set; }

        public int NombreUnite { get; private set; }

        public int NombreCentaineDizaineUnite { get; private set; }

        public bool EstUneDizaineAvecExceptionPourUneUnite()
        {
            return DizaineAvecExceptionPourUneUnite.Any(x => x == NombreDeDizaine);
        }

        public IList<PartieDuNombre> RecupererLaDecomposition()
        {
            var chiffreDeCompose = new List<PartieDuNombre>();

            var nombreInitial = new Nombre(_nombre);

            if (NombreDeMillion > 0)
                chiffreDeCompose.Add(new PartieDuNombreEnMillion(nombreInitial, new Nombre(NombreDeMillion)));

            if (NombreDeMillier > 0)
                chiffreDeCompose.Add(new PartieDuNombreEnMillier(nombreInitial, new Nombre(NombreDeMillier)));

            if (NombreCentaineDizaineUnite > 0)
                chiffreDeCompose.Add(new PartieDuNombreEnCentaine(nombreInitial, new Nombre(NombreCentaineDizaineUnite)));



            return chiffreDeCompose;
        }

        private void DecomposerLeNombre()
        {
            var decompositionDuChiffre = new int[10];

            for (int i = 0; i < 10; i++)
                decompositionDuChiffre[i] = (int)((_nombre / Math.Pow(10, i)) % 10);

            NombreDeMillion = RecupererAPartirDeLaDecomposition(decompositionDuChiffre, 6, 8);
            NombreDeMillier = RecupererAPartirDeLaDecomposition(decompositionDuChiffre, 3, 5);
            NombreCentaineDizaineUnite = RecupererAPartirDeLaDecomposition(decompositionDuChiffre, 0, 2);
            NombreDeCentaine = RecupererAPartirDeLaDecomposition(decompositionDuChiffre, 2, 2);
            NombreDeDizaine = RecupererAPartirDeLaDecomposition(decompositionDuChiffre, 1, 1);
            NombreUnite = RecupererAPartirDeLaDecomposition(decompositionDuChiffre, 0, 0);
            //NombreDeMillion = _nombre / 1000000;
            //NombreDeMillier = (_nombre - (NombreDeMillion * 1000000)) / 1000;
            //NombreDeCentaine = (_nombre - (NombreDeMillion * 1000000) - (NombreDeMillier * 1000)) / 100;
            //NombreDeDizaine = (_nombre - (NombreDeMillion * 1000000) - (NombreDeMillier * 1000) - (NombreDeCentaine * 100)) / 10;
            //NombreUnite = _nombre % 10;
        }

        private int RecupererAPartirDeLaDecomposition(int[] decompositionDuChiffre, int indexDebut, int indexFin)
        {
            var nombre = 0;
            for (var i = indexDebut; i <= indexFin; i++)
                nombre += (int)(decompositionDuChiffre[i] * Math.Pow(10, i));
            return (int)(nombre / Math.Pow(10, indexDebut));
        }
    }
}