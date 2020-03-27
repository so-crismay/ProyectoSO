#include <string.h>
#include <unistd.h>
#include <stdlib.h>
#include <sys/types.h>
#include <sys/socket.h>
#include <netinet/in.h>
#include <stdio.h>

int main(int argc, char *argv[])
{
	int sock_conn, sock_listen, ret;
	struct sockaddr_in serv_adr;
	char peticion[512];
	char respuesta[512];
	// INICIALITZACIONS
	// Obrim el socket
	//SOCKET DE ESCUCHA, en el que el servidor está esperando una conexión
	if ((sock_listen = socket(AF_INET, SOCK_STREAM, 0)) < 0)
		printf("Error creant socket");
	// Fem el bind al port
	
	
	memset(&serv_adr, 0, sizeof(serv_adr));// inicialitza a zero serv_addr
	serv_adr.sin_family = AF_INET;
	
	// asocia el socket a cualquiera de las IP de la m?quina. 
	//htonl formatea el numero que recibe al formato necesario
	serv_adr.sin_addr.s_addr = htonl(INADDR_ANY);
	// escucharemos en el puerto 9050
	serv_adr.sin_port = htons(9070);
	//asociamos en el socket de escucha esta estructura de datos que acabamos de crear
	if (bind(sock_listen, (struct sockaddr *) &serv_adr, sizeof(serv_adr)) < 0)
		printf ("Error al bind");
	//La cola de peticiones pendientes no podra ser superior a 2
	if (listen(sock_listen, 2) < 0)
		printf("Error en el Listen");
	// FIN INICIALIZACIÓN
	
	//bucle que implementa el servidor
	int i;
	// Atenderemos solo 5 peticiones, por eso en el bucle i<5 (no es lo habitual)
	for(;;){
		printf ("Escuchando\n");
		
		//Espera conexion y crea un socket de conexión a través del cual nos conectamos y nos comunicamos
		sock_conn = accept(sock_listen, NULL, NULL);
		printf ("He recibido conexion\n");
		//sock_conn es el socket que usaremos para este cliente
		
		//Bucle de atención al cliente para que pueda realizar varias peticiones
		int terminar=0;
		while(terminar==0)
		{
		
			//recibimos la peticion en este caso:
			// Ahora recibimos su nombre, que dejamos en el buffer peticion
			ret=read(sock_conn,peticion, sizeof(peticion));
			//además nos da un entero que es el número de bytes que he leido
			printf ("Recibido\n");
		
			// Tenemos que añadirle la marca de fin de string 
			// para que no escriba lo que hay despues en el buffer
			peticion[ret]='\0';
		
			//Escribimos el nombre en la consola
		
			printf ("Petición: %s\n",peticion);
		
			// aquí veremos lo que nos están pidiendo
			char *p = strtok( peticion, "/");
			int codigo =  atoi (p);
			char nombre[20];
			
			if(codigo!=0)
			{
				p = strtok( NULL, "/");
				strcpy (nombre, p);
				printf ("Codigo: %d, Nombre: %s\n", codigo, nombre);
			}
			//si recibimos un 0 terminaremos la conexion
			if(codigo==0)
				terminar=1;
			else if (codigo ==1) //piden la longitd del nombre
				sprintf (respuesta,"%d",strlen (nombre));
			else if(codigo==2)
					// quieren saber si el nombre es bonito
					if((nombre[0]=='M') || (nombre[0]=='S'))
					strcpy (respuesta,"SI");
					else
						strcpy (respuesta,"NO");
			else if(codigo==3)	//decir si es alto
			{
				p = strtok( NULL, "/");
				float altura = atof(p);
				if(altura>1.70)
					sprintf(respuesta,"%s: Eres alto", nombre);
				else
					sprintf(respuesta, "%s: Eres bajo", nombre);
			}	
			else if (codigo==4) //Dame los nombres de los conectados
			{
				
			}
			else //Si el codigo es 5, nos dará cuantas invitaciones realizamos 
				//y a quin queremos invitar a jugar
			{
				
			}
			if(codigo!=0)
			{
				printf ("%s\n", respuesta);
				// Y lo enviamos
				//envio la respuesta con el socket de conexion y la respuesta
				write (sock_conn,respuesta, strlen(respuesta));
			}
			
		}
		// Se acabo el servicio para este cliente
		close(sock_conn); 
	}
}

