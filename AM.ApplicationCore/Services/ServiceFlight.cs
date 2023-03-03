using AM.ApplicationCore.Domain;
using AM.ApplicationCore.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AM.ApplicationCore.Services
{
    public class ServiceFlight : IServiceFlight
    {
        public List<Flight> Flights { get; set; } = new List<Flight>();


        public List<DateTime> GetFlightDates(string destination)
        {
            List<DateTime> dates = new List<DateTime>();
            for (int i = 0; i < Flights.Count; i++)
            {
                if (Flights[i].Destination == destination)
                {
                    dates.Add(Flights[i].FlightDate);
                }
            }
            return dates;
        }
        public List<DateTime> GetFlightDates2(string destination)
        {
            List<DateTime> dates = new List<DateTime>();
            Flights.ForEach(f =>
            {
                if (f.Destination == destination)
                {
                    dates.Add(f.FlightDate);
                };
            }
            );
            return dates;
        }

        public IEnumerable<DateTime> GettFlightDates(string destination)
        {

            foreach (Flight flight in Flights)
            {
                if (flight.Destination == destination)
                {

                    yield return flight.FlightDate;
                }
            }

        }

        public void GetFlights(string filterValue, Func<string, Flight, Boolean> func)
        {

            Func<string, Flight, Boolean> Condition = func;
            foreach (var item in Flights)
            {
                if (Condition(filterValue, item))
                {
                    Console.WriteLine(item);
                }
            }
        }

        public IList<DateTime>GetFlightDate(string destination)
        {
            //sytaxe requette
          //  var query = from f in Flights 
          //              where f.Destination == destination 
           //             select f.FlightDate;
          //  return query.ToList();
          //syntaxe de methode 
          var query = Flights.Where(f=>f.Destination== destination)
                .Select(f=>f.FlightDate);
            return query.ToList();
        }

        public void ShowFlightDetailes(Plane plane) { 
           // var req = from f in Flights
            //          where(f.plane==plane)
            //          select new { f.FlightDate,f.Destination };
           // foreach (var item in req)
           // {
            //    Console.WriteLine( item.Destination+""+item.FlightDate);
            //}
            var req = Flights
                .Where(f => f.plane== plane)
                .Select(f => new {f.FlightDate,f.Destination});
            foreach (var item in req)
            {
                Console.WriteLine(item.Destination+""+item.FlightDate);
            }

        }

        public int ProgrammeFlightNumber(DateTime startDate)
        {
            /*var req = from f in Flights
                          // methode1
                          // where f.FlightDate > startDate && f.FlightDate < startDate.AddDays(7)
                          //methode2
                     where f.FlightDate>startDate && (f.FlightDate - startDate).TotalDays < 7
                     select f;
            return req.Count();*/
            //methode syntaxe methode
            return Flights
                .Where(f =>  f.FlightDate > startDate && (f.FlightDate - startDate).TotalDays < 7 )
                .Count();
        }

        public double DurationAverage(string destination)
        {
            /* var query = from f in Flights
                         where f.Destination == destination
                         select f.EstimatedDuration;
             return query.Average();*/
            //syntaxe de methode
            return  Flights
                 .Where(f => f.Destination == destination)
                 .Average(f=>f.EstimatedDuration);
        }

        public IList<Flight> OrderedDuration(string destination)
        {
            /*var req = from f in Flights
                      orderby f.EstimatedDuration descending
                      select f;
            return req.ToList();*/
            //methode syntaxe
            return Flights
                .OrderByDescending(f => f.EstimatedDuration).ToList();
        }

        public IList<Traveller> SenioTravellers(Flight flight)
        {
            /*var req = (from f in Flights
                      where f.FlightId== flight.FlightId
                      select f).Single();*/
            return flight.passangers.OfType<Traveller>().OrderBy(p=>p.BirthDate).Take(3).ToList();
            
        }

        public IList<IGrouping<string,Flight>> DestinationGroupedFlight()
        {
            var req= Flights
                .GroupBy(f => f.Destination).ToList();
            foreach (var item in req)
            {
                Console.WriteLine("destination:"+item.Key);
                foreach (var itemx in item)
                {
                    Console.WriteLine("decolage:"+itemx.FlightDate );
                }
            }
            return req;
        }

        Action<Plane> FlightDetailsDel;

        Func<string, double> DurationAverageDel;
        public ServiceFlight()
        {
            FlightDetailsDel =  plane => 
            {
                var req = Flights
                    .Where(f => f.plane == plane)
                    .Select(f => new { f.FlightDate, f.Destination });
                foreach (var item in req)
                {
                    Console.WriteLine(item.Destination + "" + item.FlightDate);
                }

            };
       
        DurationAverageDel = destination=>
            (from f in Flights
                        where f.Destination == destination
                         select f.EstimatedDuration).Average();
        }
    }
}