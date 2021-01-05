using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyCompany.AgentApplication.Dtos;
using MyCompany.AgentApplication.Services;
using System;
using System.Threading.Tasks;

namespace MyCompany.AgentApplication.Controllers
{
    // [Authorize]
    public class Claims : Controller
    {
        private readonly IClaimService _claimService;

        public Claims(IClaimService claimService)
        {
            _claimService = claimService ?? throw new ArgumentNullException(nameof(claimService));
        }

        public async Task<ActionResult> Index()
        {
            return View(await _claimService.GetAllClaims());
        }

        public async Task<ActionResult> Details(int id)
        {
            return View(await _claimService.GetAsync(id));
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View(new ClaimDTO());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(ClaimDTO claim)
        {
            if (!ModelState.IsValid)
            {
                return View(claim);
            }

            var newlyCreatedClaim = await _claimService.AddAsync(claim);
            return RedirectToAction(nameof(Details), new { id = newlyCreatedClaim.ClaimId });
        }

        [HttpGet]
        public async Task<ActionResult> Edit(int id)
        {
            return View(await _claimService.GetAsync(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, ClaimDTO form)
        {
            if (!ModelState.IsValid)
            {
                return View(form);
            }

            await _claimService.UpdateAsync(form);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<ActionResult> Delete(int id)
        {
            return View(await _claimService.GetAsync(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, IFormCollection collection)
        {
            await _claimService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}