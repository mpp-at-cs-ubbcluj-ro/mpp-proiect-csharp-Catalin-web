using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Model.Entities;
using System.Net.Mime;
using WebApi.Services;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("trip")]
    [Produces(MediaTypeNames.Application.Json)]
    public class TripController
    {
        private readonly ITripFacade _tripFacade;
        public TripController(ITripFacade tripFacade)
        {
            _tripFacade = tripFacade;
        }

        /// <summary>
        /// Authentificate.
        /// </summary>
        /// <response code="200">OK.</response>
        [HttpPost]
        [Route("authenticate")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public void CreateAccount(string email, string password)
        {
            _tripFacade.authentificare(email, password);
        }

        /// <summary>
        /// Login.
        /// </summary>
        /// <response code="200">OK.</response>
        [HttpGet]
        [Route("login")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public void Login(string email, string password)
        {
            _tripFacade.login(email, password);
        }

        /// <summary>
        /// Logout.
        /// </summary>
        /// <response code="200">OK.</response>
        [HttpGet]
        [Route("logout")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public void Logout()
        {
            _tripFacade.logout();
        }

        /// <summary>
        /// Query all trips.
        /// </summary>
        /// <response code="200">OK.</response>
        [HttpGet]
        [Route("query/trips")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public List<Excursie> queryAllTrips()
        {
            return _tripFacade.getAllExcursii();
        }

        /// <summary>
        /// Query by nume obiectiv and ora.
        /// </summary>
        /// <response code="200">OK.</response>
        [HttpGet]
        [Route("query/trips/obiectiv")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public List<Excursie> queryTripByObiectiv(string numeObiectiv, int oraMinim, int oraMaxim)
        {
            return _tripFacade.getAllExcursiiByNumeAndInterval(numeObiectiv, oraMinim, oraMaxim);
        }

        /// <summary>
        /// Query firma transport by id.
        /// </summary>
        /// <response code="200">OK.</response>
        [HttpGet]
        [Route("query/firma/id")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public FirmaTransport queryFirmaById(int idFirma)
        {
            return _tripFacade.getFirmaTransportById(idFirma);
        }

        /// <summary>
        /// Query firma transport by nume.
        /// </summary>
        /// <response code="200">OK.</response>
        [HttpGet]
        [Route("query/firma/nume")]
        public FirmaTransport queryFirmaByNume(string numeFirma)
        {
            return _tripFacade.getFirmaTransportByNume(numeFirma);
        }

        /// <summary>
        /// Get number of left spaces.
        /// </summary>
        /// <response code="200">OK.</response>
        [HttpGet]
        [Route("query/trip/left")]
        public int queryLeftSpaces(Excursie excursie)
        {
            return _tripFacade.getNrLocuriDisponibile(excursie);
        }

        /// <summary>
        /// Query obiectiv by id.
        /// </summary>
        /// <response code="200">OK.</response>
        [HttpGet]
        [Route("query/obiectiv/id")]
        public ObiectiveTuristice queryObiectivById(int idObiectiv)
        {
            return _tripFacade.getObiectivById(idObiectiv);
        }

        /// <summary>
        /// Query obiectiv by nume.
        /// </summary>
        /// <response code="200">OK.</response>
        [HttpGet]
        [Route("query/obiectiv/nume")]
        public ObiectiveTuristice queryObiectivByNume(string numeObiectiv)
        {
            return _tripFacade.getObiectivByNume(numeObiectiv);
        }

        /// <summary>
        /// Get person by name and telefon number.
        /// </summary>
        /// <response code="200">OK.</response>
        [HttpGet]
        [Route("query/persoana/nume")]
        public Persoana getPersoanaByNumeAndTelefon(string numeClient, string numarTelefon)
        {
            return _tripFacade.getPersoanaByNumeAndTelefon(numeClient, numarTelefon);
        }

        /// <summary>
        /// Reservate places.
        /// </summary>
        /// <response code="200">OK.</response>
        [HttpPost]
        [Route("query/rezervare")]
        public void rezervaLocuri(string numeClient, string numarTelefon, int numarBileteDorite, Excursie excursie)
        {
            _tripFacade.rezervaLocuri(numeClient, numarTelefon, numarBileteDorite, excursie);
        }
    }
}
