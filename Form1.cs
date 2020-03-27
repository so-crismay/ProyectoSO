using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        Socket server;

        public Form1()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        //Botón de registro de usuario
        private void Registro_Click(object sender, EventArgs e)
        {

                // Quiere realizar un registro en la BBDD
                string mensaje = "1/" + user.Text + "/" + contraseña.Text;
                // Enviamos al servidor el user tecleado y la contraseña
                byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                server.Send(msg);

                //Recibimos la respuesta del servidor
                byte[] msg2 = new byte[80];
                server.Receive(msg2);
                mensaje = Encoding.ASCII.GetString(msg2).Split('\0')[0];
                MessageBox.Show(mensaje);
           

        }

        //Botón de inicio de sesión
        private void Sesion_Click(object sender, EventArgs e)
        {
                // Quiere realizar un inicio de sesión
                string mensaje = "2/" + user.Text + "/" + contraseña.Text;
                // Enviamos al servidor el user tecleado y la contraseña
                byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                server.Send(msg);

                //Recibimos la respuesta del servidor
                byte[] msg2 = new byte[80];
                server.Receive(msg2);
                mensaje = Encoding.ASCII.GetString(msg2).Split('\0')[0];
                MessageBox.Show(mensaje);
            
        }

        //Desconectamos del servidor
        private void Desconectar_Click(object sender, EventArgs e)
        {

            // Se terminó el servicio. 
            string mensaje = "0/";

            byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
            server.Send(msg);

            // Nos desconectamos
            this.BackColor = Color.Gray;
            server.Shutdown(SocketShutdown.Both);
            server.Close();
        }

        private void Conectar_Click(object sender, EventArgs e)
        {
            //Creamos un IPEndPoint con el ip del servidor y puerto del servidor 
            //al que deseamos conectarnos
            IPAddress direc = IPAddress.Parse("192.168.56.102");
            IPEndPoint ipep = new IPEndPoint(direc, 9070);


            //Creamos el socket 
            server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                server.Connect(ipep);//Intentamos conectar el socket
                this.BackColor = Color.Green;
            }


            catch (SocketException)
            {
                //Si hay excepcion imprimimos error y salimos del programa con return 
                MessageBox.Show("No he podido conectar con el servidor");
                return;
            } 
        }

        //Consulta el nombre del jugador que ganó en menor tiempo
        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            // Quiere realizar la consulta escogida
            string mensaje = "3/" + user.Text ;
            // Enviamos al servidor el user tecleado y la contraseña
            byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
            server.Send(msg);

            //Recibimos la respuesta del servidor
            byte[] msg2 = new byte[80];
            server.Receive(msg2);
            mensaje = Encoding.ASCII.GetString(msg2).Split('\0')[0];
            MessageBox.Show(mensaje);
        }

        //Consulta el mejor tiempo en el que ganó el usuario indicado en el user.TextBox
        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            // Quiere realizar la consulta escogida
            string mensaje = "4/" + user.Text;
            // Enviamos al servidor el user tecleado y la contraseña
            byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
            server.Send(msg);

            //Recibimos la respuesta del servidor
            byte[] msg2 = new byte[80];
            server.Receive(msg2);
            mensaje = Encoding.ASCII.GetString(msg2).Split('\0')[0];
            MessageBox.Show(mensaje);
        }

        //Consulta el número de partidas jugadas por el usuario indicado en el user.TextBox
        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            // Quiere realizar la consulta escogida
            string mensaje = "5/" + user.Text;
            // Enviamos al servidor el user tecleado y la contraseña
            byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
            server.Send(msg);

            //Recibimos la respuesta del servidor
            byte[] msg2 = new byte[80];
            server.Receive(msg2);
            mensaje = Encoding.ASCII.GetString(msg2).Split('\0')[0];
            MessageBox.Show(mensaje);
        }



        

        
    }
}
