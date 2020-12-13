using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Web.Http;
using System.Web.Http.Cors;
using ShottbowingAccess;
namespace ShottBowing.Controllers
{
    [EnableCorsAttribute("*", "*", "*")]
    public class AdvbookController : ApiController
    {
        List<string> strings = new List<string>() {"value1", "value2", "value3"};
        // GET api/values
        //public IEnumerable<AdvBook> Get()//IEnumerable<AdvBook>
        //{
        //   // return strings;
        //    using (ShottDB_BWEntities entities = new ShottDB_BWEntities())
        //    {
        //        return entities.AdvBooks.ToList();
        //    }

        //}
       [BasicAuthentication]
        public HttpResponseMessage Get()
        {
            string username = Thread.CurrentPrincipal.Identity.Name;
            using (ShottDB_BWEntities entities = new ShottDB_BWEntities())
            {
                 switch (username.ToLower())
                    {
                        case "admin":
                            return Request.CreateResponse(HttpStatusCode.OK, entities.AdvBooks.Where(e => e.OrgId == "SRT").ToList());
                        case "ankit":
                          return Request.CreateResponse(HttpStatusCode.OK, entities.AdvBooks.Where(e => e.OrgId == "SBR").ToList());
                        default:
                        return Request.CreateResponse(HttpStatusCode.BadRequest);
                   }


               

                // return entities.AdvBooks.ToList();
            }


        }

        //public HttpResponseMessage Get(int id)//AdvBook Get(int id)
        //{
        //    using (ShottDB_BWEntities bw = new ShottDB_BWEntities())
        //    {
        //        var re= bw.AdvBooks.FirstOrDefault(n => n.SrNo == id);
        //        if (re != null)
        //        {
        //            return Request.CreateResponse(HttpStatusCode.OK, bw.AdvBooks.FirstOrDefault(n => n.SrNo == id));
        //        }
        //        else {
        //            HttpError er = new HttpError();
        //            er.ExceptionMessage = "Record not found";
        //            return Request.CreateResponse(HttpStatusCode.Created, er.ExceptionMessage);
        //        }
        //    }

        //    // return strings[id];
        //}

        public HttpResponseMessage Post([FromBody] AdvBook values)
        {
            try
            {
                using (ShottDB_BWEntities pst = new ShottDB_BWEntities())
                {
                    values.QDate = DateTime.Now;
                    pst.AdvBooks.Add(values);
                    pst.SaveChanges();

                    var message = Request.CreateResponse(HttpStatusCode.Created, values);
                    message.Headers.Location = new Uri(Request.RequestUri + values.SrNo.ToString());
                    return message;


                }

            }
            catch (Exception exe)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, exe);
            }
           
        }




        // POST api/values
        //public void Post([FromBody]string values)
        //{
        //    strings.Add(values);
        //}
        //// PUT api/values/5
        //public void Put(int id,[FromBody] string values)
        //{
        //    strings[id] = values;
        
        //}
        //// DELETE api/values/5
        //public void Delete(int id)
        //{
        //    strings.RemoveAt(id);
        
        //}
      


    }
}




    


   


