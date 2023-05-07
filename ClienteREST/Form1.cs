using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Http;
using Newtonsoft.Json.Linq;

namespace ClienteREST
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private bool ValidarCampos()
        {
            bool valido = true;

            if (string.IsNullOrWhiteSpace(textBox1.Text))
            {
                errorProvider1.SetError(textBox1, "El valor no debe estar vacío");
                valido = false;
            }
            else
            {
                errorProvider1.SetError(textBox1, "");
            }

            

            return valido;
        }

            private void Form1_Load(object sender, EventArgs e)
        {
           


        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (ValidarCampos())
            {
                using (var client = new HttpClient())
                {



                    string url = "https://jsonplaceholder.typicode.com/posts/" + textBox1.Text; //url con el que se realiza la consulta 
                                                                                                //client.DefaultRequestHeaders.Add("Authorization","TOKEN");
                    client.DefaultRequestHeaders.Clear();

                    var response = client.GetAsync(url).Result;

                    var res = response.Content.ReadAsStringAsync().Result;
                    dynamic r = JObject.Parse(res);
                    if (res !="{}")
                    {
                       // Console.WriteLine();
                        txtnombre.Text = r.nombre;
                        txtdui.Text = r.dui;
                        txtfechaexp.Text = r.fecha_limite;
                        if (r.estado == 0)
                        {
                            txtestadocanjeo.Text = "Sin canjear" ;
                        }
                        else if(r.estado == 1)
                        {
                            txtestadocanjeo.Text = "Canjeado";
                        }
                        txtcondiciones.Text = r.descripcion;
                        txtcondiciones.Text = r.condiciones;

                    }
                    else
                    {
                        MessageBox.Show("Codigo invalido o inexistente, prueba de nuevo");
                    }
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            using (var client = new HttpClient())
            {
                string url = "https://jsonplaceholder.typicode.com/posts/1"; //url con el que se realiza la consulta 
                //client.DefaultRequestHeaders.Add("Authorization","TOKEN");
                client.DefaultRequestHeaders.Clear();
                string parametros = "{'estado':'1',}";
                dynamic jSonstring = JObject.Parse(parametros);

                var http_content = new StringContent(jSonstring.ToString(), Encoding.UTF8,"applicatio/json");

                var response = client.PutAsync(url, http_content).Result;

                var res = response.Content.ReadAsStringAsync().Result;
                dynamic r = JObject.Parse(res);
                

            }
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

    }
}
