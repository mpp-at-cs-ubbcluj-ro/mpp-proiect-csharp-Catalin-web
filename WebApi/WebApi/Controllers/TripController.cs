using DataStore.Provicers.SQLite;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Model.Entities;
using System.Collections.Concurrent;
using System.Net.Mime;
using System.Net.WebSockets;
using System.Runtime.InteropServices;
using System.Text;
using WebApi.Services;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("v1")]
    [Produces(MediaTypeNames.Application.Json)]
    public class TripController : ControllerBase
    {
        private readonly ITripFacade _tripFacade;
        private static ConcurrentBag<WebSocket> webSockets = new();
        private static WebSocketReceiveResult result;

        public TripController(TripContext tripContext)
        {
            _tripFacade = new TripFacade(tripContext);
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
        [HttpPost]
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
        [HttpPost]
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
        [HttpPost]
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
        [HttpPost]
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
        [HttpPost]
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
        [HttpPost]
        [Route("query/firma/nume")]
        public FirmaTransport queryFirmaByNume(string numeFirma)
        {
            return _tripFacade.getFirmaTransportByNume(numeFirma);
        }

        /// <summary>
        /// Get number of left spaces.
        /// </summary>
        /// <response code="200">OK.</response>
        [HttpPost]
        [Route("query/trip/left")]
        public Left queryLeftSpaces(int idExcursie)
        {
            return new Left() { left = _tripFacade.getNrLocuriDisponibile(idExcursie) };
        }

        /// <summary>
        /// Query obiectiv by id.
        /// </summary>
        /// <response code="200">OK.</response>
        [HttpPost]
        [Route("query/obiectiv/id")]
        public ObiectiveTuristice queryObiectivById(int idObiectiv)
        {
            return _tripFacade.getObiectivById(idObiectiv);
        }

        /// <summary>
        /// Query obiectiv by nume.
        /// </summary>
        /// <response code="200">OK.</response>
        [HttpPost]
        [Route("query/obiectiv/nume")]
        public ObiectiveTuristice queryObiectivByNume(string numeObiectiv)
        {
            return _tripFacade.getObiectivByNume(numeObiectiv);
        }

        /// <summary>
        /// Get person by name and telefon number.
        /// </summary>
        /// <response code="200">OK.</response>
        [HttpPost]
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
        [Route("add/rezervare")]
        public void rezervaLocuri(string numeClient, string numarTelefon, int numarBileteDorite, int idExcursie)
        {
            _tripFacade.rezervaLocuri(numeClient, numarTelefon, numarBileteDorite, idExcursie);
            sendDataToAll();
        }

        /// <summary>
        /// Reservate places.
        /// </summary>
        /// <response code="200">OK.</response>
        [HttpGet]
        [Route("webSocket")]
        public async Task get()
        {
            if (HttpContext.WebSockets.IsWebSocketRequest)
            {
                var webSocket = await HttpContext.WebSockets.AcceptWebSocketAsync().ConfigureAwait(false);

                var buffer = new byte[1024 * 4];
                result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);

                webSockets.Add(webSocket);

                sendDataToAll();

            }
            else
            {
                HttpContext.Response.StatusCode = 400;
            }
            await Task.Delay(-1);
        }

        /// <summary>
        /// Add Excursie
        /// </summary>
        /// <response code="200">OK.</response>
        [HttpPost]
        [Route("exursie/add")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<Excursie> addExcursie(string numeObiectiv, string numeFirma, int ora, float pret, int nrLocuriTotale)
        {
            try
            {
            var excursie = _tripFacade.addExcursie(numeObiectiv, numeFirma, ora, pret, nrLocuriTotale);
            return Ok(excursie);
            }
            catch(Exception ex)
            {
                return BadRequest(ex);
            }
        }

        /// <summary>
        /// Delete Excursie
        /// </summary>
        /// <response code="200">OK.</response>
        [HttpPost]
        [Route("exursie/delete")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<Excursie> deleteExcursie(int idExcursie)
        {
            try
            {
                _tripFacade.deleteExcursie(idExcursie);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        /// <summary>
        /// Get Excursie
        /// </summary>
        /// <response code="200">OK.</response>
        [HttpPost]
        [Route("exursie/get")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<Excursie> GetExcursie(int idExcursie)
        {
            try
            {
               var excursie = _tripFacade.getExcursie(idExcursie);
                return Ok(excursie);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        /// <summary>
        /// Update Excursie
        /// </summary>
        /// <response code="200">OK.</response>
        [HttpPost]
        [Route("exursie/update")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<Excursie> UpdateExcursie(int idExcursie, string numeObiectiv, string numeFirma, int ora, float pret, int nrLocuriTotale)
        {
            try
            {
                _tripFacade.updateExcursie(idExcursie,numeObiectiv,numeFirma,ora,pret,nrLocuriTotale);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        private async void sendDataToAll()
        {
            foreach (var webSocket in webSockets)
            {
                try
                {

                var mesaj = new ArraySegment<byte>(Encoding.UTF8.GetBytes("Hello from server"));
                await webSocket.SendAsync(mesaj, WebSocketMessageType.Text, false, CancellationToken.None);
                }
                catch (Exception ex)
                {

                }
            }
        }
    }
}
