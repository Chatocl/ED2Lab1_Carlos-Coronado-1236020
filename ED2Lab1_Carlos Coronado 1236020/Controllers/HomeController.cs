using ED2Lab1_Carlos_Coronado_1236020.Models;
using ED2Lab1_Carlos_Coronado_1236020.Models.Datos;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace ED2Lab1_Carlos_Coronado_1236020.Controllers
{
    public class HomeController : Controller
    {
        private IHostingEnvironment Environment;
       
      
        public HomeController(IHostingEnvironment _environment)
        {
            Environment = _environment;

        }

        public IActionResult Index()
        {
            if (Singleton.Instance.bandera==1)
            {
                Singleton.Instance.bandera = 0;
                return View(Singleton.Instance.Aux);
            }
            else
            {
                return View(Singleton.Instance.ArbolAVL.ObtenerLista());
            }
           
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public ActionResult CargarArchivo(IFormFile File)
        {
    
            try
            {
                string Ope = "", Dpi = "";
                Persona AuxPer = new Persona();
                string Aux1 = Path.Combine(this.Environment.WebRootPath, "Uploads");
                string Aux2 = Path.Combine(Aux1, "inputs");
                DirectoryInfo directory = new DirectoryInfo(Aux2);
                FileInfo[] AuxCartas = directory.GetFiles("*.txt");
                List<string> Cartas = new List<string>();
                foreach (FileInfo file in AuxCartas)
                {
                    Cartas.Add(file.Name);
                }
                if (File != null)
                {
                    string path = Path.Combine(this.Environment.WebRootPath, "Uploads");
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    string FileName = Path.GetFileName(File.FileName);
                    string FilePath = Path.Combine(path, FileName);
                    using (FileStream stream = new FileStream(FilePath, FileMode.Create))
                    {
                        File.CopyTo(stream);
                    }
                    using (TextFieldParser csvFile = new TextFieldParser(FilePath))
                    {

                        csvFile.CommentTokens = new string[] { "#" };
                        csvFile.SetDelimiters(new string[] { ";" });
                        csvFile.HasFieldsEnclosedInQuotes = true;
                       
                        csvFile.ReadLine();

                        while (!csvFile.EndOfData)
                        {
                            string[] fields = csvFile.ReadFields();
                            Ope = fields[0];
                            Persona Aux= JsonSerializer.Deserialize<Persona>(fields[1]);
                            Aux.codificacion = new string[Aux.companies.Length];
                            Aux.decodificacion = new string[Aux.companies.Length];
                            Aux.CarRecomen=new string[4];
                            
                            if (Ope=="INSERT")
                            {
                                for (int i = 0; i < Aux.companies.Length; i++)
                                {
                                    string Acodi = Aux.companies[i] + " " + Aux.dpi;
                                    List<int> encoding = Singleton.Instance.CodiCartas.Comprimir(Acodi);
                                    string decode = Singleton.Instance.CodiCartas.Descomprimir(encoding);   
                                    for (int a = 0; a < encoding.Count; a++)
                                    {
                                        Aux.codificacion[i] += Convert.ToString(encoding[a]);
                                    }
                                    Aux.decodificacion[i] = decode;
                                    
                                }
                                int pas = 1;
                                int guardar = 0;
                                while (Cartas.Contains("REC-" + Aux.dpi + "-" + pas + ".txt"))
                                {
                                    Aux.CarRecomen[guardar] = Cartas[Cartas.IndexOf("REC-" + Aux.dpi + "-" + pas + ".txt")];
                                    pas++;
                                    guardar++;
                                }
                                Singleton.Instance.ArbolAVL.Add(Aux);
                            }
                            else if (Ope=="PATCH")
                            {
                                Dpi = Aux.dpi;
                                List<Persona> personas = Singleton.Instance.ArbolAVL.Obtener(a => a.name == Aux.name);
                                 var  Cambio = personas.Find(a => a.dpi == Aux.dpi);

                                Singleton.Instance.ArbolAVL.Remove(Aux);

                                var AuPersona = new Models.Persona
                                {
                                    name = Cambio.name,
                                    dpi = Cambio.dpi,
                                    address = Cambio.address,
                                    datebirth = Cambio.datebirth,
                                    companies = Cambio.companies,
                                    codificacion=Cambio.codificacion,
                                    decodificacion=Cambio.decodificacion
                                };
                                Singleton.Instance.ArbolAVL.Add(AuPersona);
                            }
                            else 
                            {
                                Singleton.Instance.ArbolAVL.Remove(Aux);
                            }
                        }
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                ViewData["Message"] = "Algo sucedio mal";
                return RedirectToAction(nameof(Index));

            }
        }

        public ActionResult BuscDPI(string BuscDPI)
        {
            try
            {
                Singleton.Instance.bandera = 1;
                Singleton.Instance.Aux = Singleton.Instance.ArbolAVL.Obtener(a => a.dpi == BuscDPI);
                if (Singleton.Instance.Aux.Count == 0)
                {
                    Singleton.Instance.bandera = 0;
                }
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                Singleton.Instance.bandera = 0;
                ViewData["Message"] = "No Encontrado";
                return RedirectToAction(nameof(Index));

            }
        }
        public ActionResult BuscName(string BuscName)
        {
            try
            {
                Singleton.Instance.bandera = 1;
                Singleton.Instance.Aux = Singleton.Instance.ArbolAVL.Obtener(a => a.name == BuscName);
                if (Singleton.Instance.Aux.Count==0)
                {
                    Singleton.Instance.bandera = 0;
                }
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                Singleton.Instance.bandera = 0;
                ViewData["Message"] = "No Encontrado";
                return RedirectToAction(nameof(Index));

            }
        }
    }
}
