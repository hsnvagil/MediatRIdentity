using System.Threading.Tasks;
using MediatR;
using MediatRIdentity.Auth;
using Microsoft.AspNetCore.Mvc;

namespace MediatRIdentity.Controllers {
    public class AuthController : Controller {
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator) {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> SignUp() {
            return View(new SignUp.Command());
        }

        [HttpPost]
        public async Task<IActionResult> SignUp(SignUp.Command command) {
            if (ModelState.IsValid) {
                var result = await _mediator.Send(command);
                if (result != null) return RedirectToAction("SignIn");
            }

            return View(command);
        }

        [HttpGet]
        public async Task<IActionResult> SignIn() {
            return View(new SignIn.Query());
        }

        [HttpPost]
        public async Task<IActionResult> SignIn(SignIn.Query query) {
            if (ModelState.IsValid) {
                var result = await _mediator.Send(query);
                if (result != null) return RedirectToAction("Index", "Home");
            }

            return View(query);
        }
    }
}