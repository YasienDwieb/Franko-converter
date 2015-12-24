using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TryaAgain
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Run_Click(object sender, EventArgs e)
        {
            string readyText ="";
            string result = "";
            string temp = "";
            temp =Input.Text.ToLower();
            string[] EnglishWords;
            EnglishWords = temp.Split(' ');
            for (int i = 0; i < EnglishWords.Length; i++)
            {
                readyText += EnglishWords[i] + ',';
            }
            readyText = readyText.Substring(0,readyText.Length-1);
            HttpWebRequest Request = (HttpWebRequest)WebRequest.Create("http://www.google.com/transliterate/indic?tlqt=1&langpair=en|ar&text=" + readyText + "&&tl_app=1");
            Request.Method = "GET";
            Request.ContentType = "application/x-www-form-urlencoded";
            var Response = (HttpWebResponse)Request.GetResponse();
            var Reader = new StreamReader(Response.GetResponseStream());
            var JsonText = Reader.ReadToEnd();
            var Objects = JArray.Parse(JsonText); // parse as array  

            foreach (JObject jObject in Objects)
            {
                List<string> lstArabicWords;
                foreach (KeyValuePair<String, JToken> pair in jObject)
                {
                    lstArabicWords = new List<string>();
                    if (pair.Key.ToString() == "hws")
                    {
                        var ArabicWords = pair.Value;
                        foreach (var word in ArabicWords)
                        {
                            lstArabicWords.Add(word.ToString());
                            result+= lstArabicWords.ElementAt(0)+" ";
                            break;
                        }
                    }
                    Output.Text ="";
                    Output.Text = result;
                }
            }
        }
    }
}
