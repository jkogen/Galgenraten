using Galgenraten.Core.Models;
using Galgenraten.Core.ViewModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace Galgenraten.Test
{
    [TestClass]
    public class ModelTests
    {
        Spiel spiel;

        [TestMethod]
        public void ErzeugeSpiel()
        {
            spiel = new Spiel();
        }

        [TestMethod]
        public void StarteSpiel()
        {
            if (spiel is null)
                ErzeugeSpiel();

            spiel.StarteSpiel();
        }

        [TestMethod]
        public void SpielHatWort()
        {
            if (spiel is null)
                StarteSpiel();

            Assert.IsTrue(spiel.AktuellesWort.Buchstaben.Count > 0);
        }

        [TestMethod]
        public void SpielHatMoeglicheBuchstaben()
        {
            if (spiel is null)
                StarteSpiel();

            Assert.IsTrue(spiel.MoeglicheBuchstaben.Count > 0);
        }

        [TestMethod]
        public void RateBuchstaben()
        {
            var buchstaben = "abcdefghijklmnopqrstuvwxyzöäü,.-+'~";

            if (spiel is null)
                StarteSpiel();

            foreach (var buchstabe in buchstaben.Select(a => a))
                spiel.RateBuchstaben(buchstabe);
        }

        [TestMethod]
        public void Aufloesen()
        {
            if (spiel is null)
                StarteSpiel();

            spiel.Aufloesen();

            Assert.IsTrue(spiel.AktuellesWort.IstAufgeloest);
            Assert.IsTrue(spiel.Ende);
        }
    }
}
