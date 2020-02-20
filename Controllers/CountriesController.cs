using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using MainDbAPI.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MainDbAPI.Controllers
{
    [Route("api/[controller]")]
    public class CountriesController : Controller
    {
        //private readonly DataContext _context;
        private readonly IConfiguration _configuration;
        private readonly string _conString;

        public CountriesController(IConfiguration configuration)
        {
            _configuration = configuration;
            _conString = _configuration.GetConnectionString("DefaultConnection");
            //_context = context;
        }
        // GET: api/<controller>
        //[Route("get_country")]
        [HttpGet]
        public IActionResult GetCountries()
        {
            List<CountryModel> countries = new List<CountryModel>();

            using (SqlConnection conn = new SqlConnection(_conString)) 
            {
                SqlCommand cmd = new SqlCommand("Select * FROM Country ORDER BY countryName", conn);
                cmd.CommandType = CommandType.Text;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                DataTable dt = new DataTable();
                da.Fill(ds);

                if (ds.Tables.Count > 0) 
                {
                    dt = ds.Tables[0];

                    foreach (DataRow dataRow in dt.Rows)
                    {
                        CountryModel countryModel = new CountryModel();
                        countryModel.CountryId = Convert.ToInt32(dataRow["CountryId"]);
                        countryModel.CountryName = dataRow["CountryName"].ToString();
                        countryModel.Description = dataRow.IsNull("Description") ? null : dataRow["Description"].ToString();
                        countries.Add(countryModel);
                    }
                }
            }
                
            return Ok(countries);
            
        }

        // GET api/<controller>/5
        [HttpGet("{countryId}")]
        public IActionResult GetCountriesById(int countryId)
        {
            List<CountryModel> countriesById = new List<CountryModel>();

            using (SqlConnection con = new SqlConnection(_conString))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM Country WHERE CountryId ='" + countryId + "'", con);
                cmd.CommandType = CommandType.Text;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                DataTable dt = new DataTable();
                da.Fill(ds);

                if (ds.Tables.Count > 0)
                {
                    dt = ds.Tables[0];

                    foreach (DataRow dataRow in dt.Rows)
                    {
                        CountryModel countryModel = new CountryModel();
                        countryModel.CountryId = Convert.ToInt32(dataRow["CountryId"]);
                        countryModel.CountryName = dataRow["CountryName"].ToString();
                        countryModel.Description = dataRow.IsNull("Description") ? null : dataRow["Description"].ToString();
                        countriesById.Add(countryModel);
                    }
                }
            }

            return Ok(countriesById);

        }


        // POST api/<controller>
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
