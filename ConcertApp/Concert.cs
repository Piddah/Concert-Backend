
namespace ConcertApp
{
    public class Concert
    {
        private static int _nextId = 1;
        public int Id { get; private set; }
        public string Artist { get; private set; }
        public string Location { get; private set; }
        public DateTime Date { get; private set; }
        public int Tickets { get; private set; }

        public static readonly List<Concert> concerts =
        [
            new(
            "Ed Sheeran",
            "Malmö, Swedbank Stadion",
            new DateTime(2025, 05, 22, 19, 00, 00),
            20000
        ),
        new(
            "Vibrasphere",
            "Göteborg, Trädgårn",
            new DateTime(2025, 04, 11, 21, 00, 0),
            5000
        ),
        new(
            "Birdy",
            "Göteborg, Concert Hall",
            new DateTime(2025, 06, 05, 20, 30, 0),
            8000
        ),
        new(
            "Band of Horses",
            "Malmö, Babel",
            new DateTime(2025, 03, 17, 19, 30, 0),
            700
        )
        ];

        public Concert(string artist, string location, DateTime date, int tickets)
        {
            Id = _nextId++;
            Artist = artist;
            Location = location;
            Date = date;
            Tickets = tickets;
        }

        public static void AddConcert(Concert concert)
        {
            Concert newConcert = new Concert(concert.Artist, 
                                             concert.Location, 
                                             concert.Date, 
                                             concert.Tickets);

            concerts.Add(newConcert);
        }

        public static void EditConcert(Concert concert, int id)
        {
            var editConcert = concerts.FirstOrDefault(concert => concert.Id == id);

                editConcert.Artist = concert.Artist;
                editConcert.Location = concert.Location;
                editConcert.Date = concert.Date;
                editConcert.Tickets = concert.Tickets;
        }

        public static void DeleteConcert(int id)
        {
                concerts.RemoveAll(concert => concert.Id == id);
        }

        public static int GetValidTickets(string input)
        {
            if (!int.TryParse(input, out int tickets) && tickets > 0)
                return -1;

            else return tickets;
        } 
    }
}
