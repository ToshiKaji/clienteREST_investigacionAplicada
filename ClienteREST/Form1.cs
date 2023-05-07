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
        public string token { get; set; }
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
            button2.Enabled = false;
            login login = new login();
            login.Close();
            


        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (ValidarCampos())
            {
                using (var client = new HttpClient())
                {



                    string url = "https://cupones-apirest.herokuapp.com/api/getCuponById/" + textBox1.Text; //url con el que se realiza la consulta 
                                                                                                            //client.DefaultRequestHeaders.Add("Authorization","TOKEN");
                                                                                                            //client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
                    var response = client.GetAsync(url).Result;

                    var res = response.Content.ReadAsStringAsync().Result;
                    //Console.WriteLine(res);
                    dynamic jsonrespuesta = JObject.Parse(res);
                    var r = jsonrespuesta["body"];
                    Console.WriteLine(r);
                    if (r.Count>0)
                    {
                        // Console.WriteLine();
                        txtempresa.Text = r[0].empresa.ToString();
                        txtfechaexp.Text = r[0].fecha_limite.ToString();
                        if (r[0].estado == 0)
                        {
                            txtestadocanjeo.Text = "Sin canjear" ;
                            button2.Enabled = true;
                        }
                        else if(r[0].estado == 1)
                        {
                            txtestadocanjeo.Text = "Canjeado";
                            button1.Enabled = true;
                            button2.Enabled = false;
                        }
                        txtdescripcion.Text = r[0].descripcion.ToString();
                        txtcondiciones.Text = r[0].condiciones.ToString();
                       
                        Console.WriteLine(r);
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
                string url = "https://cupones-apirest.herokuapp.com/api/actualizar-cupon/"+textBox1.Text; //url con el que se realiza la consulta 
                client.DefaultRequestHeaders.Add("Authorization", "Bearer "+token);
                //client.DefaultRequestHeaders.Clear();
                string parametros = "{'estado':'1',}";
                dynamic jSonstring = JObject.Parse(parametros);

                var http_content = new StringContent(jSonstring.ToString(), Encoding.UTF8,"applicatio/json");

                var response = client.PutAsync(url, http_content).Result;

                var res = response.Content.ReadAsStringAsync().Result;
                dynamic r = JObject.Parse(res);
                Console.WriteLine(r);
                if (r.status != 400)
                {
                    MessageBox.Show("Ocurrio un error, intente mas tarde");
                }
                else
                {
                    MessageBox.Show("Operacion realizada con exito");
                    txtcondiciones.Clear();
                    txtdescripcion.Clear();
                    txtempresa.Clear();
                    txtestadocanjeo.Clear();
                    txtfechaexp.Clear();
                    button1.Enabled = true;
                }

            }
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

    }
}
