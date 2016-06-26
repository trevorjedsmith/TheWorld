using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TheWorld.Models
{
    public class WorldRepository : IWorldRepository
    {
        private WorldContext _context;
        private ILogger<WorldRepository> _logger;

        public WorldRepository(WorldContext context, ILogger<WorldRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public void AddStop(string tripName,Stop stop,string username)
        {
            var theTrip = GetTripByName(tripName,username);
            stop.Order = theTrip.Stops.Max(s => s.Order) + 1;
            theTrip.Stops.Add(stop);
            _context.Stops.Add(stop);
        }

        public void AddTrip(Trip newTrip)
        {
            _context.Add(newTrip);
        }

        public IEnumerable<Trip> GetAllTrips()
        {
            try
            {
                return _context.Trips.OrderBy(t => t.Name).ToList();
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return null;
            }
        }

        public IEnumerable<Trip> GetAllTripsWithStops()
        {
            try
            {
                return _context.Trips
                            .Include(t => t.Stops)
                            .OrderBy(t => t.Name)
                            .ToList();
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return null;
            }
        }

        public Trip GetTripByName(string tripName, string username)
        {
            return _context.Trips.Include(t => t.Stops)
                            .Where(w => w.Name == tripName && w.UserName == username)
                            .FirstOrDefault();
        }

        public IEnumerable<Trip> GetUserTripsWithStops(string name)
        {
            
              var trips =  _context.Trips.Include(t => t.Stops).
                Where(w => w.UserName == name);
            return trips;
        }

        public bool SaveAll()
        {
            return _context.SaveChanges() > 0;
        }
    }
}
