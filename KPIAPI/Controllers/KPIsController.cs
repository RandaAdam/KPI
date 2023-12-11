using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using KPIAPI.Data;
using KPIAPI.Data.Models;
using KPIAPI.Core.IConfiguration;

namespace KPIAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KPIsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public KPIsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: api/KPIs
        [HttpGet]
        public async Task<ActionResult<ApiResult<KPI>>> GetKPIs()
        {
            //note using AsNoTracking to perform a read-only task
            var kpis= await _unitOfWork.Kpis.GetAll();
            return Ok(kpis);
        }

        // GET: api/KPIs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<KPI>> GetKPI(int id)
        {
            var kPI = await _unitOfWork.Kpis.GetById(id);

            if (kPI == null)
            {
                return NotFound();
            }

            return Ok(kPI);
        }

        // GET: api/KPIs/GetKPIsInDepartment/4
        [HttpGet("GetKPIsInDepartment/{depNo}")]
        public async Task<ActionResult<ApiResult<KPI>>> GetKPIsInDepartment(int depNo)
        {

            var KPIsInDepartment = await _unitOfWork.Kpis.Find(kpi => kpi.DepNo == depNo);


            if (KPIsInDepartment?.Data.Count == 0)
            {
                return NotFound();
            }
            
            return KPIsInDepartment;
        }

        // PUT: api/KPIs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<ActionResult<ApiResult<KPI>>> PutKPI(int id, KPI kpi)
        {
            if (id != kpi.KPIIDNum)
            {
                return BadRequest("could not update different ids");
            }

            try
            {
                await _unitOfWork.Kpis.Update(kpi);
                await _unitOfWork.CompleteAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return NotFound();
            }
            return NoContent();
        }

        // POST: api/KPIs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ApiResult<KPI>>> PostKPI(KPI kPI)
        {

            //if new dep
            if (DepExist(kPI.DepNo) == false)
            {
                Department department = new() { DepNo = kPI.DepNo };
                await _unitOfWork.Deps.Add(department);
            }
            await _unitOfWork.Kpis.Add(kPI);
            await _unitOfWork.CompleteAsync();

            return await _unitOfWork.Kpis.Find(kpi => kpi.KPIIDNum == kPI.KPIIDNum);
                
        }

        // DELETE: api/KPIs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteKPI(int id)
        {
            var kPI = await _unitOfWork.Kpis.GetById(id);
            if (kPI == null)
            {
                return NotFound();
            }
            else 
            {
                await _unitOfWork.Kpis.Delete(id);
                await _unitOfWork.CompleteAsync();

                return NoContent();
            }

            
        }

        private bool DepExist(int depId)
        {
            var bExist = false;
            if(_unitOfWork.Deps.Find(e => e.DepNo == depId)?.Result !=null)
                bExist = true;
            return bExist ;
        }
    }
}
