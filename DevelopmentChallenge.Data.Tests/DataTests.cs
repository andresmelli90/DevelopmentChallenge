using DevelopmentChallenge.Data.Classes;
using NUnit.Framework;
using System.Collections.Generic;

namespace DevelopmentChallenge.Data.Tests
{
    [TestFixture]
    public class DataTests
    {
        [TestCase]
        public void TestResumenListaVacia()
        {
            Assert.AreEqual("<h1>Lista vacía de formas!</h1>",
                ReporteFormas.Imprimir(new List<FormaGeometrica>(), Idioma.Castellano));
        }

        [TestCase]
        public void TestResumenListaVaciaFormasEnIngles()
        {
            Assert.AreEqual("<h1>Empty list of shapes!</h1>",
                ReporteFormas.Imprimir(new List<FormaGeometrica>(), Idioma.Ingles));
        }

        [TestCase]
        public void TestResumenListaVaciaFormasEnItaliano()
        {
            Assert.AreEqual("<h1>Elenco vuoto di moduli!</h1>",
                ReporteFormas.Imprimir(new List<FormaGeometrica>(), Idioma.Italiano));
        }

        [TestCase]
        public void TestResumenListaConUnCuadrado()
        {
            var cuadrados = new List<FormaGeometrica> {new Cuadrado(5)};

            var resumen = ReporteFormas.Imprimir(cuadrados, Idioma.Castellano);

            Assert.AreEqual("<h1>Reporte de Formas</h1>1 Cuadrado | Area 25 | Perimetro 20 <br/>TOTAL:<br/>1 formas Perimetro 20 Area 25", resumen);
        }

        [TestCase]
        public void TestResumenListaConUnTrapecioEnItaliano()
        {
            var trapecios = new List<FormaGeometrica> { new TrapecioRectangulo(1, 3, 5) };

            var resumen = ReporteFormas.Imprimir(trapecios, Idioma.Italiano);

            Assert.AreEqual("<h1>Rapporto sui moduli</h1>1 Trapezio | Area 10 | Perimetro 14,2 <br/>TOTALE:<br/>1 moduli Perimetro 14,2 Area 10", resumen);
        }

        [TestCase]
        public void TestResumenListaConMasCuadrados()
        {
            var cuadrados = new List<FormaGeometrica>
            {
                new Cuadrado(5),
                new Cuadrado(1),
                new Cuadrado(3)
            };

            var resumen = ReporteFormas.Imprimir(cuadrados, Idioma.Ingles);

            Assert.AreEqual("<h1>Shapes report</h1>3 Squares | Area 35 | Perimeter 36 <br/>TOTAL:<br/>3 shapes Perimeter 36 Area 35", resumen);
        }

        [TestCase]
        public void TestResumenListaConMasTrapeciosEnItaliano()
        {
            var trapecios = new List<FormaGeometrica>
            {
                new TrapecioRectangulo(1, 3, 5),
                new TrapecioRectangulo(2, 2, 4),
                new TrapecioRectangulo(1.2m, 3, 2.4m)
            };

            var resumen = ReporteFormas.Imprimir(trapecios, Idioma.Italiano);

            Assert.AreEqual("<h1>Rapporto sui moduli</h1>3 Trapezi | Area 23,04 | Perimetro 35,52 <br/>TOTALE:<br/>3 moduli Perimetro 35,52 Area 23,04", resumen);
        }

        [TestCase]
        public void TestResumenListaConMasTipos()
        {
            var formas = new List<FormaGeometrica>
            {
                new Cuadrado(5),
                new Circulo(3),
                new TrianguloEquilatero(4),
                new Cuadrado(2),
                new TrianguloEquilatero(9),
                new Circulo(2.75m),
                new TrianguloEquilatero(4.2m)
            };

            var resumen = ReporteFormas.Imprimir(formas, Idioma.Ingles);

            Assert.AreEqual(
                "<h1>Shapes report</h1>2 Squares | Area 29 | Perimeter 28 <br/>2 Circles | Area 13.01 | Perimeter 18.06 <br/>3 Triangles | Area 49.64 | Perimeter 51.6 <br/>TOTAL:<br/>7 shapes Perimeter 97.66 Area 91.65",
                resumen);
        }

        [TestCase]
        public void TestResumenListaConMasTiposEnCastellano()
        {
            var formas = new List<FormaGeometrica>
            {
                new Cuadrado(5),
                new Circulo(3),
                new TrianguloEquilatero(4),
                new Cuadrado(2),
                new TrianguloEquilatero(9),
                new Circulo(2.75m),
                new TrianguloEquilatero(4.2m)
            };

            var resumen = ReporteFormas.Imprimir(formas, Idioma.Castellano);

            Assert.AreEqual(
                "<h1>Reporte de Formas</h1>2 Cuadrados | Area 29 | Perimetro 28 <br/>2 Círculos | Area 13,01 | Perimetro 18,06 <br/>3 Triángulos | Area 49,64 | Perimetro 51,6 <br/>TOTAL:<br/>7 formas Perimetro 97,66 Area 91,65",
                resumen);
        }

        [TestCase]
        public void TestResumenListaConMasTiposEnItaliano()
        {
            var formas = new List<FormaGeometrica>
            {
                new Cuadrado(3),
                new Circulo(7),
                new TrianguloEquilatero(4),
                new TrapecioRectangulo(2, 6, 2),
                new TrianguloEquilatero(6),
                new Circulo(3),
                new TrapecioRectangulo(2, 6, 2),
                new TrianguloEquilatero(1.2m)
            };

            var resumen = ReporteFormas.Imprimir(formas, Idioma.Italiano);

            Assert.AreEqual(
                "<h1>Rapporto sui moduli</h1>1 Quadrato | Area 9 | Perimetro 12 <br/>2 Cerchi | Area 45,55 | Perimetro 31,42 <br/>3 Triangoli | Area 23,14 | Perimetro 33,6 <br/>2 Trapezi | Area 16 | Perimetro 27,31 <br/>TOTALE:<br/>8 moduli Perimetro 104,33 Area 93,69",
                resumen);
        }
    }
}
