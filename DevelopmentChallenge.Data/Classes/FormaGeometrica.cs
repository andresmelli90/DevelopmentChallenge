/******************************************************************************************************************/
/******* ¿Qué pasa si debemos soportar un nuevo idioma para los reportes, o agregar más formas geométricas? *******/
/******************************************************************************************************************/

/*
 * TODO: 
 * Refactorizar la clase para respetar principios de la programación orientada a objetos.
 * Implementar la forma Trapecio/Rectangulo. 
 * Agregar el idioma Italiano (o el deseado) al reporte.
 * Se agradece la inclusión de nuevos tests unitarios para validar el comportamiento de la nueva funcionalidad agregada (los tests deben pasar correctamente al entregar la solución, incluso los actuales.)
 * Una vez finalizado, hay que subir el código a un repo GIT y ofrecernos la URL para que podamos utilizar la nueva versión :).
 */

using DevelopmentChallenge.Data.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;

namespace DevelopmentChallenge.Data.Classes
{
    public abstract class FormaGeometrica
    {
        public TipoForma TipoForma { get; protected set; }
        public abstract decimal CalcularArea();
        public abstract decimal CalcularPerimetro();
    }

    public class Cuadrado : FormaGeometrica
    {
        private readonly decimal _lado;

        public Cuadrado(decimal lado)
        {
            _lado = lado;
            TipoForma = TipoForma.Cuadrado;
        }

        public override decimal CalcularArea()
        {
            return _lado * _lado;
        }

        public override decimal CalcularPerimetro()
        {
            return _lado * 4;
        }
    }

    public class Circulo : FormaGeometrica
    {
        private readonly decimal _radio;

        public Circulo(decimal radio)
        {
            _radio = radio;
            TipoForma = TipoForma.Circulo;
        }

        public override decimal CalcularArea()
        {
            return (decimal)Math.PI * (_radio / 2) * (_radio / 2);
        }

        public override decimal CalcularPerimetro()
        {
            return (decimal)Math.PI * _radio;
        }
    }

    public class TrianguloEquilatero : FormaGeometrica
    {
        private readonly decimal _lado;

        public TrianguloEquilatero(decimal lado)
        {
            _lado = lado;
            TipoForma = TipoForma.TrianguloEquilatero;
        }

        public override decimal CalcularArea()
        {
            return ((decimal)Math.Sqrt(3) / 4) * _lado * _lado;
        }

        public override decimal CalcularPerimetro()
        {
            return _lado * 3;
        }
    }

    public class TrapecioRectangulo : FormaGeometrica
    {
        private readonly decimal _baseMayor;
        private readonly decimal _baseMenor;
        private readonly decimal _altura;

        public TrapecioRectangulo(decimal baseMayor, decimal baseMenor, decimal altura)
        {
            _baseMayor = baseMayor;
            _baseMenor = baseMenor;
            _altura = altura;
            TipoForma = TipoForma.TrapecioRectangulo;
        }

        public override decimal CalcularArea()
        {
            return ((_baseMayor + _baseMenor) * _altura) / 2;
        }

        public override decimal CalcularPerimetro()
        {
            var ladoRecto = Math.Abs(_baseMayor - _baseMenor) / 2;
            var ladoOblicuo = (decimal)Math.Sqrt((double)(_altura * _altura + ladoRecto * ladoRecto));
            return _baseMayor + _baseMenor + 2 * ladoOblicuo;
        }
    }

    public class ReporteFormas
    {
        public static string Imprimir(List<FormaGeometrica> formas, Idioma idioma)
        {
            SetCulture(idioma);

            var sb = new StringBuilder();

            if (!formas.Any())
            {
                sb.Append($"<h1>{Resources.ListaVaciaDeFormas}</h1>");
            }
            else
            {
                sb.Append($"<h1>{Resources.ReporteDeFormas}</h1>");
                sb.Append(GenerarReportePorForma(formas));
                sb.Append(GenerarFooter(formas));
            }

            return sb.ToString();
        }

        private static void SetCulture(Idioma idioma)
        {
            var culture = new CultureInfo(GetEnumerationDescription(idioma));
            Thread.CurrentThread.CurrentCulture = culture;
            Thread.CurrentThread.CurrentUICulture = culture;
        }

        private static string GenerarReportePorForma(List<FormaGeometrica> formas)
        {
            var sb = new StringBuilder();
            var tiposDeFormas = Enum.GetValues(typeof(TipoForma)).Cast<TipoForma>();

            foreach (var tipoForma in tiposDeFormas)
            {
                var formasTipo = formas.Where(f => f.TipoForma == tipoForma).ToList();

                if (formasTipo.Any())
                {
                    var cantidad = formasTipo.Count;
                    var areaTotal = formasTipo.Sum(f => f.CalcularArea());
                    var perimetroTotal = formasTipo.Sum(f => f.CalcularPerimetro());

                    sb.Append($"{cantidad} {TraducirForma(tipoForma, cantidad)} | {Resources.Area} {areaTotal:#.##} | {Resources.Perimetro} {perimetroTotal:#.##} <br/>");
                }
            }

            return sb.ToString();
        }

        private static string GenerarFooter(List<FormaGeometrica> formas)
        {
            var sb = new StringBuilder();
            var totalFormas = formas.Count;
            var areaTotal = formas.Sum(f => f.CalcularArea());
            var perimetroTotal = formas.Sum(f => f.CalcularPerimetro());

            sb.Append($"{Resources.Total.ToUpper()}:<br/>");
            sb.Append($"{totalFormas} {Resources.Formas} ");
            sb.Append($"{Resources.Perimetro} {perimetroTotal:#.##} ");
            sb.Append($"{Resources.Area} {areaTotal:#.##}");

            return sb.ToString();
        }

        private static string TraducirForma(TipoForma tipo, int cantidad)
        {
            var traducciones = new Dictionary<TipoForma, (string singular, string plural)>
            {
                { TipoForma.Cuadrado, (Resources.Cuadrado, Resources.Cuadrados) },
                { TipoForma.Circulo, (Resources.Circulo, Resources.Circulos) },
                { TipoForma.TrianguloEquilatero, (Resources.Triangulo, Resources.Triangulos) },
                { TipoForma.TrapecioRectangulo, (Resources.Trapecio, Resources.Trapecios) }
            };

            var (singular, plural) = traducciones[tipo];

            return cantidad == 1 ? singular : plural;
        }

        private static string GetEnumerationDescription(Enum value)
        {
            var fi = value.GetType().GetField(value.ToString());
            var attributes = fi.GetCustomAttributes(typeof(DescriptionAttribute), false) as DescriptionAttribute[];

            return attributes?.FirstOrDefault()?.Description ?? value.ToString();
        }
    }

    public enum Idioma
    {
        [Description("es")]
        Castellano = 1,
        [Description("en")]
        Ingles,
        [Description("it")]
        Italiano
    }

    public enum TipoForma
    {
        Cuadrado = 1,
        Circulo,
        TrianguloEquilatero,
        TrapecioRectangulo
    }
}
