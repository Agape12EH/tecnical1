using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using tecnical1.DTO;
using tecnical1.Generic;
using tecnical1.Models;

namespace tecnical1.Controllers
{
    [ApiController]
    [Route("api/countries")]
    public class CountriesController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public CountriesController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet("response")]
        public async Task<ActionResult<int>> Dto()
        {
            var countries = await context.Countries
                .ProjectTo<CountryDTO>(mapper.ConfigurationProvider)
                .ToListAsync();

            return countries.Count();
        }

        [HttpGet]
        public async Task<IEnumerable<Country>> Get()
        {
            return await context.Countries
                .Include(c => c.Restaurants)
                .Include(c => c.Hotels)
                .AsNoTracking().ToListAsync();
        }

        //Filtro por Name IsoCode y Population   FALTA PARAMETROS[Name, ]
        [HttpGet("filter")]
        public async Task<IEnumerable<Country>> Filter()
        {
            return await context.Countries
                .Where(
                    c => c.Name.StartsWith("Co") || 
                    c.isoCode.StartsWith("CO") || 
                    c.Population.Equals(45000)
                )
                .OrderBy(c => c.Name)
                .ToListAsync();
        }

        //ByID
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Country>> Get(int id)
        {
            var country = await context.Countries
                .Include(c => c.Restaurants)
                .Include(c => c.Hotels)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (country == null)
            {
                return NotFound();
            }

            return country;
        }

        //Paginacion Faltan PARAMETROS INT
        [HttpGet("paginacion")]
        public async Task<ActionResult<IEnumerable<Country>>> GetPaginacion(int pageNumber = 1, int rowsPerPage = 10)
        {
            var Country = await context.Countries
                .Skip((pageNumber - 1) * rowsPerPage)
                .Take(rowsPerPage)
                .ToListAsync();

            return Country;
        }

        [HttpGet("getpaginate")]
        public async Task<ActionResult<PaginadorGenerico<CountryDTO>>> GetPaginate(
            string search,
            string order = "Name",
            string typeOrder = "ASC",
            int pageNumber = 1,
            int rowsPerPage = 10
        )
        {
            List<CountryDTO> countriesDTO;
            PaginadorGenerico<CountryDTO> paginatorCountry;

            // Recuperamos el 'DbSet' completo
            countriesDTO = await context.Countries
                            .ProjectTo<CountryDTO>(mapper.ConfigurationProvider)
                            .ToListAsync();

            // Filtramos el resultado por el 'texto de búqueda'
            if (!string.IsNullOrEmpty(search))
            {
                foreach (var item in search.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    countriesDTO = countriesDTO.Where(x => x.Name.Contains(item) || x.isoCode.Contains(item)).ToList();
                }
            }

            switch (order)
            {
                case "Name":
                    if (typeOrder.ToLower() == "desc")
                        countriesDTO = countriesDTO.OrderByDescending(x => x.Name).ToList();
                    else if (typeOrder.ToLower() == "asc")
                        countriesDTO = countriesDTO.OrderBy(x => x.Name).ToList();
                    break;

                case "isoCode":
                    if (typeOrder.ToLower() == "desc")
                        countriesDTO = countriesDTO.OrderByDescending(x => x.isoCode).ToList();
                    else if (typeOrder.ToLower() == "asc")
                        countriesDTO = countriesDTO.OrderBy(x => x.isoCode).ToList();
                    break;

                case "Population":
                    if (typeOrder.ToLower() == "desc")
                        countriesDTO = countriesDTO.OrderByDescending(x => x.Population).ToList();
                    else if (typeOrder.ToLower() == "asc")
                        countriesDTO = countriesDTO.OrderBy(x => x.Population).ToList();
                    break;

                default:
                    if (typeOrder.ToLower() == "desc")
                        countriesDTO = countriesDTO.OrderByDescending(x => x.Id).ToList();
                    else if (typeOrder.ToLower() == "asc")
                        countriesDTO = countriesDTO.OrderBy(x => x.Id).ToList();
                    break;
            }

            int TotalRecords = 0;
            int TotalPages = 0;
            // Número total de registros de la tabla Customers
            TotalRecords = countriesDTO.Count();
            // Obtenemos la 'página de registros' de la tabla Customers
            countriesDTO = countriesDTO.Skip((pageNumber - 1) * rowsPerPage)
                                             .Take(rowsPerPage)
                                             .ToList();

            // Número total de páginas de la tabla Customers
            TotalPages = (int)Math.Ceiling((double)TotalRecords / rowsPerPage);

            // Instanciamos la 'Clase de paginación' y asignamos los nuevos valores
            paginatorCountry = new PaginadorGenerico<CountryDTO>()
            {
                RowsPerPage = rowsPerPage,
                TotalRecords = TotalRecords,
                TotalPages = TotalPages,
                CurrentPage = pageNumber,
                CurrentSearch = search,
                CurrentOrder = order,
                CurrentOrderType = typeOrder,
                Result = countriesDTO
            };

            return paginatorCountry;
        }
    }
}
