using Microsoft.AspNetCore.Mvc;
using TestingApp2.Models;
using TestingApp2.ViewModels.Project;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace TestingApp.Controllers
{
    public class ProjectController : Controller
    {
        private readonly ILogger<ProjectController> _logger;
        const string ViewPath = "~/Views/Project";

        public ProjectController(ILogger<ProjectController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            ProjectIndexViewModel viewModel = new ProjectIndexViewModel();

            using (NewDbContext dbContext = new NewDbContext())
        
            {

                var items = dbContext.Projects.ToList();               
                viewModel.Items = items;
            }

            return View(ViewPath+"/Index.cshtml", viewModel);
        }

        public IActionResult GetProjectForm(int Id)
        {
            Project project = new Project();
            project.Id = 0;

            if (Id > 0)
            {
                using (NewDbContext  dbContext= new NewDbContext())
                {
                    project = dbContext.Projects.FirstOrDefault(x => x.Id == Id);
                }

                if (project == null)
                {
                    project = new Project();
                    project.Id = 0;
                }
            }

            return View( ViewPath + "/ProjectDetails.cshtml", project);
        }

        [HttpPost("Add")]
        public IActionResult Add([FromBody] Project Item)
        {
            //Item=this.HttpContext.Request.ReadFromJsonAsync<DAL.WorkItem>().Result;

            AddResponseDTO responseDTO = new AddResponseDTO();

            using (NewDbContext dbContext = new NewDbContext())
            {
              int  newId= dbContext.Projects.Max(x => x.ProjectId) + 1;

                Item.Id = newId;
                dbContext.Projects.Add(Item);
                dbContext.SaveChanges();
            }
            responseDTO.is_ok = true;
            responseDTO.message = "Item has been added successfully";

            return Ok(responseDTO);
        }


        [HttpPost("Update")]
        public IActionResult Update([FromBody] Project Item)
        {
            //Item=this.HttpContext.Request.ReadFromJsonAsync<DAL.WorkItem>().Result;

            AddResponseDTO responseDTO = new AddResponseDTO();

            using (NewDbContext dbContext = new NewDbContext())
            {
               var olditem= dbContext.Projects.FirstOrDefault(x => x.Id == Item.Id);

                olditem.Id = Item.Id;

                olditem.ProjectId = Item.ProjectId;

                olditem.ProjectName = Item.ProjectName;

           
                  
                dbContext.SaveChanges();
            }
            responseDTO.is_ok = true;
            responseDTO.message = "Item has been updated successfully";

            return Ok(responseDTO);
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
    }

   public class AddResponseDTO
    {
        public bool is_ok { get; set; }
        public string message { get; set; }
    }


}