namespace PV179_RestaurantWeb.Models
{
    public class LocalizationCreateModel
    {
        public int Id { get; set; }
        public string IsoLanguageCode { get; set; }
        public string StringCode { get; set; }
        public string LocalizedString { get; set; }
    }
}
