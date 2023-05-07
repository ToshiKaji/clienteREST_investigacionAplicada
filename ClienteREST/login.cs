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
    public partial class login : Form
    {
        public login()
        {
            InitializeComponent();
        }

        private void login_Load(object sender, EventArgs e)
        {

        }

        private void ingresar_Click(object sender, EventArgs e)
        {
            using (var client = new HttpClient())
            {



                string url = "https://cupones-apirest.herokuapp.com/api/login"; //url con el que se realiza la consulta 
                client.DefaultRequestHeaders.Clear();                                                                                        //client.DefaultRequestHeaders.Add("Authorization","TOKEN");
                                                                                                                                             //client.DefaultRequestHeaders.Clear();
               // client.DefaultRequestHeaders.Add("Authorization", "Bearer eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJpc3MiOiJodHRwOlwvXC9sb2NhbGhvc3Q6ODAwMFwvYXBpXC9sb2dpbiIsImlhdCI6MTY4MzQ0NTA5OCwiZXhwIjoxNjgzNDQ4Njk4LCJuYmYiOjE2ODM0NDUwOTgsImp0aSI6ImU5OVpOcEyYzFCRFVpeDgiLCJzdWIiOjIsInBydiI6IjIzYmQ1Yzg5NDlmNjAwYWRiMzllNzAxYzQwMDg3MmRiN2E1OTc2ZjcifQ.gucWrVMsbGm2ciBEtTQs4UXAuE1jEpur6QkVZ65x4Yw");
             

                string parametros = "{'email':'"+txtemail.Text+"','password':'"+txtcontrasena.Text+"'}";
                dynamic jSonstring = JObject.Parse(parametros);

                var http_content = new StringContent(jSonstring.ToString(), Encoding.UTF8, "applicatio/json");

                var response = client.PostAsync(url, http_content).Result;
            
                var res = response.Content.ReadAsStringAsync().Result;
                dynamic r = JObject.Parse(res);
               if(res!="{}")
                {
                   
                    Form1 menu = new Form1();
                    menu.token = r.token;
                    menu.Show();
                    this.Hide();
                    

                }
               else
                {
                    MessageBox.Show("Correo o contraseña incorrecta");
                }




            }
        }
    }
}
