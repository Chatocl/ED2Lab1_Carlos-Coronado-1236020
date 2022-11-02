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
using System.Security.Cryptography;
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

        public ActionResult Edit(string id)//Descomprimir
        {
            List<int> encoding = new List<int>();
            var viewPersona = Singleton.Instance.ArbolAVL.ObtenerLista().FirstOrDefault(a => a.dpi == id);
            if (viewPersona.DECODI == false)
            {
                for (int i = 0; i < 4; i++)
                {
                    string Aux1 = "";
                    if (viewPersona.CarRecomen[i] != null)
                    {
                        Aux1 = Singleton.Instance.CodiCartas.Descomprimir(viewPersona.CarCod[i]);
                        viewPersona.CarRecomen[i] = Aux1;
                    }
                }

                viewPersona.DECODI = true;
                viewPersona.CODI = false;
            }

            return View(viewPersona);
        }
        public ActionResult CesarDescom(string id)//Cifrar
        {
            List<int> encoding = new List<int>();
            var viewPersona = Singleton.Instance.ArbolAVL.ObtenerLista().FirstOrDefault(a => a.dpi == id);
            if (viewPersona.DECODI == false)
            {
                for (int i = 0; i < 4; i++)
                {
                    string Aux1 = "";
                    if (viewPersona.CarRecomen[i] != null)
                    {
                        Aux1 = Singleton.Instance.CodiCover.Cifrar(viewPersona.Conv[i], "Contraseñ");
                        viewPersona.Conv[i] = Aux1;
                    }
                }

                viewPersona.DECODI2 = true;
                viewPersona.CODI2 = false;
            }

            return View(viewPersona);
        }
        public ActionResult CesarCom(string id)//Descomprimir
        {
            List<int> encoding = new List<int>();
            var viewPersona = Singleton.Instance.ArbolAVL.ObtenerLista().FirstOrDefault(a => a.dpi == id);
            if (viewPersona.CODI2 == false)
            {
                for (int i = 0; i < 4; i++)
                {
                    string Aux1 = "";
                    string Aux2 = "";
                    if (viewPersona.CarRecomen[i] != null)
                    {
                        Aux1 = Singleton.Instance.CodiCover.Descifrar(viewPersona.Conv[i],"Contraseñ");
                        foreach (var Char in Aux1)
                        {
                            if (Char=='.')
                            {
                                Aux2 += Char;
                                Aux2 += "\n";
                            }
                            else if (Char!='\r' && Char != '\n')
                            {
                                Aux2 += Char;
                            }
                           
                        }
                            viewPersona.Conv[i] = Aux2;
                    }
                }

                viewPersona.CODI2 = true;
                viewPersona.DECODI2 = false;
            }

            return View(viewPersona);
        }
        public ActionResult Details(string id)//Comprimir
        {
            List<int> encoding = new List<int>();
            var viewPersona = Singleton.Instance.ArbolAVL.ObtenerLista().FirstOrDefault(a => a.dpi == id);
            if (viewPersona.CODI==false)
            {
                for (int i = 0; i < 4; i++)
                {
                    List<int> Aux1 = new List<int>();
                    if (viewPersona.CarRecomen[i] != null)
                    {
                        Aux1 = Singleton.Instance.CodiCartas.Comprimir(viewPersona.CarRecomen[i]);
                        viewPersona.CarCod[i] = Aux1;
                    }
                }
                for (int a = 0; a < 4; a++)
                {
                    string Aux2 = "";
                    if (viewPersona.CarCod[a]!=null)
                    {
                        for (int b = 0; b < viewPersona.CarCod[a].Count; b++)
                        {
                            Aux2 += viewPersona.CarCod[a][b].ToString();
                            if (b % 50 == 0 && b != 0)
                            {
                                Aux2 += "\n";
                            }
                        }
                        viewPersona.CarRecomen[a] = Aux2;
                    }
         
                }
                viewPersona.CODI = true;
                viewPersona.DECODI = false;
            }

            return View(viewPersona);
        }
        public ActionResult MosLlaves(string id)//Cifrar
        {
            List<int> encoding = new List<int>();
            var viewPersona = Singleton.Instance.ArbolAVL.ObtenerLista().FirstOrDefault(a => a.dpi == id);
            return View(viewPersona);
        }
        public ActionResult RSACif(string id)//Cifrar
        {
            List<int> encoding = new List<int>();
            var viewPersona = Singleton.Instance.ArbolAVL.ObtenerLista().FirstOrDefault(a => a.dpi == id);
            return View(viewPersona);
        }
        public ActionResult RSADeCif(string id)//Descomprimir
        {
            List<int> encoding = new List<int>();
            var viewPersona = Singleton.Instance.ArbolAVL.ObtenerLista().FirstOrDefault(a => a.dpi == id);
            return View(viewPersona);
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
                            Aux.LlavesPriv=new string[Aux.companies.Length];
                            Aux.IRsa= new List<int>[Aux.companies.Length];
                            Aux.CarRecomen = new string[4];
                            Aux.CarCod = new List<int>[4];
                            Aux.Conv=new string[4];
                            
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
                                    Aux.IRsa[i] = Singleton.Instance.RsaCodifi.LLaves();
                                    
                                }
                                Aux.LLavePub = Aux.IRsa[1][1].ToString() + "," + Aux.IRsa[1][0].ToString();
                                for (int i = 0; i < Aux.companies.Length; i++)
                                {
                                    Aux.LlavesPriv[i] = Aux.IRsa[i][2].ToString() + "," + Aux.IRsa[1][0].ToString();
                                }
                                int pas = 1;
                                int guardar = 0;
                                while (Cartas.Contains("REC-" + Aux.dpi + "-" + pas + ".txt"))
                                {
                                    int PosCartas = Cartas.IndexOf("REC-" + Aux.dpi + "-" + pas + ".txt");
                                    Aux.CarRecomen[guardar] = System.IO.File.ReadAllText(AuxCartas[PosCartas].ToString());
                                    pas++;
                                    guardar++;
                                }
                                int pas2 = 1;
                                int guardar2 = 0;
                                while (Cartas.Contains("CONV-" + Aux.dpi + "-" + pas2 + ".txt"))
                                {
                                    int PosCartas = Cartas.IndexOf("CONV-" + Aux.dpi + "-" + pas2 + ".txt");
                                    Aux.Conv[guardar2] = System.IO.File.ReadAllText(AuxCartas[PosCartas].ToString());
                                    pas2++;
                                    guardar2++;
                                }
                                Aux.CODI = false;
                                Aux.CODI2=false;
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
                                    codificacion = Cambio.codificacion,
                                    decodificacion = Cambio.decodificacion,
                                    CarRecomen = Cambio.CarRecomen,
                                    CarCod=Cambio.CarCod,
                                    LLavePub = Cambio.LLavePub,
                                    LlavesPriv = Cambio.LlavesPriv,
                                    recluiter = Cambio.recluiter,
                                    DECODI = Cambio.DECODI,
                                    CODI = Cambio.CODI,
                                    Conv=Cambio.Conv,
                                    DECODI2 = Cambio.DECODI2,
                                    CODI2 = Cambio.CODI2,

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
