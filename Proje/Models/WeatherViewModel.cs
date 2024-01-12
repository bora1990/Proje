namespace Proje.Models
{

    public class WeatherViewModel
    {
        public Location location { get; set; }
        public Current_Observation current_observation { get; set; }
    }

    public class Location
    {
        public string city { get; set; }
        public string country { get; set; }
    }

    public class Current_Observation
    {
        public Condition condition { get; set; }
    }



    public class Condition
    {
        public int temperature { get; set; }
        public string text { get; set; }
        public int code { get; set; }
    }

 

}

