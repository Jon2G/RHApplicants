using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kit.Sql.Attributes;

namespace EvaluadorRH.Classes.Tests
{

    public class WebTest : Test
    {
        [NotNull]
        public string Url { get; set; }

        public WebTest()
        {

        }
        public WebTest(string Title, string MarkDown, string Url) : this(-1, Title, MarkDown,Url) { }
        public WebTest(int Id, string Title, string MarkDown, string Url) : base(Id, Title, MarkDown)
        {
            this.Url = Url;

        }

        [InitTable]
        public new static void InitTable()
        {
            AppData.SQLiteConnection.Insert(new WebTest("Diseño", @"<!DOCTYPE html>
<html>
    <h2>Diseñe una ventana utilizando los controles que mejor ayuden a resolver el problema de manera adecuada. </h2>
    <h3> Propósito de la ventana:</h3>
    <ul>
        <li> Editar información de productos ya existentes</li>
        <li>Deberá permitir seleccionar algún producto ej. “Leche   entera” y modificar algún campo Ej. Costo</li>
        <li>Mostar la información del producto seleccionado </li>
        <ul>
            <li>Costo</li>
            <li>Precio</li>
            <li>Categoría</li>
            <li>Nombre</li>
            <li>Existencia</li>
        </ul>
      </ul>
</html>", "https://ninjamock.com/s/MZGZDHx"));
        }
    }
}
