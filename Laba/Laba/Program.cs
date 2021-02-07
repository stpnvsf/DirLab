using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;

namespace Laba
{
    class Program
    {
        static void Main(string[] args)
        {
            string json = @" {" +
                "'name': 'Название формы'," +
                "'postmessage': 'Сообщение в случае успешного заполнения формы'," +
                "'items': [ { 'type': 'filler', 'attributes': { 'message': 'Произвольные текст'}}," +
                            "{ 'type': 'text', 'attributes': { 'name': 'Имя элемента', 'placeholder': 'Текст для placeholder','required': true," +
                              "'value': '', 'label': 'Label для элемента', 'class': 'css-class', 'validationRules': [{ 'type': 'email'}],'disabled': false }}]}";


            Form f = JsonConvert.DeserializeObject<Form>(json);
            
            string writePath = @"C:\Users\123\source\repos\Laba\Laba\index.html";

            try
            {
                using (StreamWriter sw = new StreamWriter(writePath, false, System.Text.Encoding.Default))
                {
                    sw.WriteLine("<html>");
                    sw.WriteLine("<head>");
                    sw.WriteLine("<meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8>");
                    sw.WriteLine("</head>");
              
                    sw.WriteLine("<body>");
                    sw.WriteLine($"<form name = \"{f.name}\", postmessage = \"{f.postmessage}\">");

                    for (int i = 0; i < f.items.Count; i++)
                    {
                        if(f.items[i].type.Equals("text") || f.items[i].type.Equals("textarea") ||
                            f.items[i].type.Equals("filler") || f.items[i].type.Equals("checkbox"))
                        {
                            StringBuilder temp = new StringBuilder($"<input type = {f.items[i].type}, ");
                            foreach (string s in f.items[i].attributes.Keys)
                            {
                                temp.Append($" {s} = \"{f.items[i].attributes[s].ToString()}\",");
                            }
                            temp.Remove(temp.Length - 1, 1);
                            temp.Append("></input>");
                            sw.WriteLine(temp);
                        }

                        else if (f.items[i].type.Equals("button"))
                        {
                            StringBuilder temp = new StringBuilder($"<button class = \"{f.items[i].attributes["class"].ToString()}\">" +
                                $"{f.items[i].attributes["text"]}</button>");
                            sw.WriteLine(temp);
                        }
                        else if (f.items[i].type.Equals("radio"))
                        {

                            StringBuilder temp = new StringBuilder("");
                            foreach (string s in f.items[i].attributes.Keys)
                            {
                                temp.Append($"<input type = radio, label = \"{f.items[i].attributes["label"].ToString()}\", ");
                                temp.Append($" checked = {f.items[i].attributes["checked"].ToString()}>");
                                temp.Append(f.items[i].attributes["value"].ToString());
                                temp.Append("</input>");
                            }
                            sw.WriteLine(temp);
                        }
                        else if (f.items[i].type.Equals("select"))
                        {
                            StringBuilder temp = new StringBuilder("<select");
                            foreach (string s in f.items[i].attributes.Keys)
                            {
                                temp.Append($" {s} = \"{f.items[i].attributes[s].ToString()}\",");
                            }
                            temp.Remove(temp.Length - 1, 1);
                            temp.Append("></select>");
                            sw.WriteLine(temp);
                        }
                                               
                    }

                    sw.WriteLine("</form>");
                    sw.Write("</body>");
                    sw.Write("</html>");
                }
                Console.WriteLine("Запись выполнена");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            
            Console.ReadKey();
        }
    }
}
