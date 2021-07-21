using MvcBurCs.Controllers;
using MvcBurCs.Models;
using MvcBurCs.Models.bur;
using MvcBurCs.Models.UserPlus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;

namespace MvcBurCs.ControllersApi
{
    public class BursCreateController : ApiController
    {
        public IHttpActionResult Get(int id)
        {
            var m = new UserPlusCreateVM {
                Payee = "Payee",
                Office = "Office",
                Address = "Address",
                SigANam = "Signatory A Name",
                SigAPos = "Signatory A Position",
                SigBNam = "Signattory B Name",
                SigBPos = "Signatory B Position",
                ParticularsText = "Particulars Text",
                Particulars = new List<UserPlusParticularsVM>(),
            };
            m.Particulars.Add(new UserPlusParticularsVM() { 
                RespCen = "RespCen",
                at = "at",
                ac= "ac",
                amount = 0
            });
            return Json(m);
        }
        // POST api/<controller>
        public IHttpActionResult Post(int id, [FromBody] UserPlusCreateVM m)
        {
            var db = new burContext();
            if (ModelState.IsValid)
            {
                var bur = new bur()
                {
                    date = DateTime.Now,
                    payee = m.Payee,
                    office = m.Office,
                    address = m.Address,
                    boxaname = m.SigANam,
                    boxapos = m.SigAPos,
                    boxbname = m.SigBNam,
                    boxbpos = m.SigBPos,
                    _particulars = m.ParticularsText,
                    status = "Saved",
                    userid = id
                };
                db.burs.Add(bur);
                db.SaveChanges();

                var parts = m.Particulars.Select(i => new bur_part
                {
                    rc = i.RespCen,
                    part_subtype = i.at,
                    uacscode = i.ac,
                    amount = i.amount
                }).ToList();
                BurHelperClass.SaveParticulars(parts, bur.id);
                BurHelperClass.SubmitBur(bur.id, id);

                return Ok(new { status = "Success", message = $"Bur submitted {bur.id}" });
            }
            return StatusCode(HttpStatusCode.BadRequest);
        }
    }
}