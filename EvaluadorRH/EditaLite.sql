CREATE TABLE ADMINISTRADORES
(
ID  INTEGER PRIMARY KEY AUTOINCREMENT,
USUARIO TEXT,
NAME TEXT,
PASSWORD  TEXT);


CREATE TABLE APPLICANTS
(
ID  INTEGER PRIMARY KEY AUTOINCREMENT,
NAME TEXT,
SURNAME1 TEXT,
SURNAME2 TEXT,
SCHOOL TEXT,
GRADE TEXT,
AGE INT,
FAVORITE_LANGUAJE TEXT,
REGISTER_DATE TEXT);

CREATE TABLE TESTS
(
ID  INTEGER PRIMARY KEY AUTOINCREMENT,
TITLE TEXT NOT NULL DEFAULT '',
MARKDOWN  TEXT NOT NULL DEFAULT '');

CREATE TABLE MAIN_TESTS
(
ID  INTEGER PRIMARY KEY AUTOINCREMENT,
APPLICANT_ID INTEGER NOT NULL,
START_DATE TEXT,
END_DATE TEXT,
TIME_ELAPSED TEXT);

CREATE TABLE COMPLETED_TESTS
(
ID  INTEGER PRIMARY KEY AUTOINCREMENT,
MAIN_TEST_ID INTEGER NOT NULL,
TEST_ID INTEGER,
SOLUTION TEXT,
START_DATE TEXT,
END_DATE TEXT,
TIME_ELAPSED TEXT);



INSERT INTO ADMINISTRADORES(USUARIO,NAME,PASSWORD) VALUES ('JANIS','Janis Zafra','1234');
INSERT INTO TESTS(TITLE,MARKDOWN) VALUES('Analisis de código',
'//Resuelva las siguientes preguntas,¡si encuentra algún error marquelo e indique por qué!
#include <iostream>
#include<string>
using namespace std;
class Auto
{

public:

	void Acelerar(int Fuerza)
	{
		//Instrucciones
	}

	//Que hace este método?
	//Respuesta: 
	Auto(string Marca)
	{
		this->Nombre = Marca;
	}

private:
	std::string Nombre;
};

//Que hace este método?
//Respuesta: 
int main()
{
	Auto* ford = new Auto();
	ford->Acelerar(12.5f);
}
//1) ¿Cómo modificaria el anterior código para que ademas incluya:? 
//	a) Un método de frenado que reciba la fuerza del pedal.
//	b) Un método que indique  si el auto esta totalmente detenido
//2) Modfique asi mismo el método "main" para llamar a los dos nuevos metodos y
//	a) Imprima en pantalla un mensaje cuando el auto este detenido');
