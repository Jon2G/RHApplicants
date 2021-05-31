
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kit;
using Kit.Model;
using Kit.Sql.Attributes;
using Kit.Sql.Helpers;


namespace EvaluadorRH.Classes.Tests
{
    public class Test : ModelBase
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get;  set; }
        [NotNull]
        public string Title { get; set; }
        [NotNull, Column("MARKDOWN")]
        public string Html { get;  set; }

        public Test()
        {

        }
        public Test(string Title, string MarkDown):this(-1,Title,MarkDown)
        {


        }
        public Test(int Id, string Title, string MarkDown)
        {
            this.Id = Id;
            this.Title = Title;
            this.Html = MarkDown;

        }
        [InitTable]
        public static void InitTable()
        {
            AppData.SQLiteConnection.InsertAll(
                new Test("Código ASCII", @"<!DOCTYPE html>
<html>
    <h2> Programa que muestre en pantalla los caracteres del código ASCII y su combinación de teclado:</h2>
   <h3>Ejemplo</h3>
     <p>
        Alt + 85 =''U''
     </p> 
</html>"),
                new Test("Ordenamiento", @"<!DOCTYPE html>
<html>
    <h2> Se desea realizar un algoritmo que realice las siguientes tareas: </h2>

    <ul>
        <li>Leer una lista de números enteros</li>
        <li>Visualizar dichos números</li>
        <li>El algoritmo deberá preguntar a el <b>usuario</b> si desea ordenar en sentido decreciente o creciente.</li>
      </ul>
      <h3>Ejemplo:</h3>
<p>
    Entrada:		        10, 15, 20, 8
    Salida [decreciente]:  	20, 15, 10, 8
    Salida [creciente]:  	 8, 10, 15, 20
</p>
</html>"),
                new Test("Pilares POO", @"<!DOCTYPE html>
<html>
    <h2>Responda las siguientes preguntas: </h2>

    <ul>
        <li>¿Cómo definiría de forma sencilla la abstracción? Mencione un ejemplo simple. </li>
        <li>¿Cómo definiría de forma sencilla el polimorfismo? Mencione un ejemplo simple. </li>
        <li>¿Cómo definiría de forma sencilla el herencia/sobreescritura? Mencione un ejemplo simple. </li>
        <li>¿Cómo definiría de forma sencilla el encapsulamiento? Mencione un ejemplo simple. </li>
    </ul>

</html>"),
                new Test("Abstracción", @"<!DOCTYPE html>
<html>
    <h3> Crea un modelo de datos que permita representar una lista de compras para un restaurante (incluye todos los campos que consideres importantes)</h2>
</html>"),
                new Test("Análisis de código", @"<!DOCTYPE html>
<html>
    <h2>Resuelva las siguientes preguntas, si encuentra algún error marquelo e indique por qué: </h2>
<code>
    
        #include&lt;iostream&gt; </br>
        #include&lt;string&gt;    </br>
        using namespace std; </br>
        class Auto </br>
        {</br>
        
        public:</br>
        
            void Acelerar(int Fuerza)</br>
            {</br>
                //Instrucciones</br>
            }</br>
        
            //Que hace este método?</br>
            //Respuesta: </br>
            Auto(string Marca)</br>
            {</br>
                this->Nombre = Marca;</br>
            }</br>
        private:</br>
            std::string Nombre;</br>
        };</br>
        
        //Que hace este método?</br>
        //Respuesta: </br>
        int main()</br>
        {</br>
            Auto* ford = new Auto();</br>
            ford->Acelerar(12.5f);</br>
        }</br>
</code>

    <ul>
        <li>¿Cómo modificaría el código para que ademas incluya:? </li>
        <ul>
            <li>Un método de frenado que reciba la fuerza del pedal.</li>
            <li>Un método que indique  si el auto esta totalmente detenido.</li>
        </ul>
        <li>Modfique asi mismo el método ""main"" para llamar a los dos nuevos metodos</li>
     
            <li>Imprima en pantalla un mensaje cuando el auto este detenido</li>
        
       
      </ul>

</html>")
                );

        }

        internal static IEnumerable<Test> GetAll()
        {
            List<Test> tests = new List<Test>(AppData.SQLiteConnection.Table<Test>());
            tests.AddRange(AppData.SQLiteConnection.Table<WebTest>());
            return tests;

        }
    }
}
