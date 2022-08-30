using ED2Lab1_Carlos_Coronado_1236020.Models;
using ED2Lab1_Carlos_Coronado_1236020.Models.Datos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.VisualBasic.FileIO;

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
            return View(Singleton.Instance.ArbolAVL.ObtenerLista());
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
                string Ope = "";
                Persona AuxPer = new Persona();
               

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
                            if (Ope=="INSERT")
                            {
                                Singleton.Instance.ArbolAVL.Add(Aux);
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
    }
}
