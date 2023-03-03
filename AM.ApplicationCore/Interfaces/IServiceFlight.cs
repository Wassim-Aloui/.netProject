using AM.ApplicationCore.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AM.ApplicationCore.Interfaces
{
    public interface IServiceFlight
    {
        public List<DateTime> GetFlightDates(string destination);
        public IEnumerable<DateTime> GettFlightDates(string destination);
        // public void GetFlights(String filterType,String filterValue)

        public void ShowFlightDetailes(Plane plane);

        public int ProgrammeFlightNumber(DateTime startDate);

        public double DurationAverage(string destination);

        public IList<Flight> OrderedDuration(string destination);

        public IList<Traveller> SenioTravellers(Flight flight);

        public IList<IGrouping<string, Flight>> DestinationGroupedFlight();

    }
}
