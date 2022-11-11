namespace RestaurantWebDomain
{
    public class Localization : BaseEntity
    {
        public string IsoLanguageCode { get; set; }
        public string StringCode { get; set; }
        public string LocalizedString { get; set; }
    }
}
