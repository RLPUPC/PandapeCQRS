using MediatR;
using Microsoft.AspNetCore.Mvc;
using Pandape.Application;
using Pandape.Web.Models;
using ValidationException = FluentValidation.ValidationException;

namespace Pandape.Web.Controllers
{
    public class CandidatesController : Controller
    {
        private readonly IMediator _mediator;

        public CandidatesController(IMediator mediator) 
        {
            _mediator = mediator;
        }

        public async Task<IActionResult> Index()
        {
            var candidates = await _mediator.Send(new CandidateQuery());
            return View(candidates.Select(x => 
            {
                return new CandidateViewModel
                {
                    IdCandidate = x.IdCandidate,
                    Name = x.Name,
                    Surname = x.Surname,
                    Birthdate = x.Birthdate,
                    Email = x.Email,
                };
            }).AsEnumerable());
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateCanditeViewModel create)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var newCandidate = new CreateCandidateCommand
                    {
                        Name = create.Name,
                        Surname = create.Surname,
                        Birthdate = create.Birthdate,
                        Email = create.Email,
                    };
                    await _mediator.Send(newCandidate);
                    return RedirectToAction(nameof(Index));
                }
                catch (ValidationException ex)
                {

                }
            }
            return View(create);
        }
    }
}
