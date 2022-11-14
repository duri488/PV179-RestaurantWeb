namespace RestaurantWebBL.DTOs;

public class LocalizationDto : BaseEntityDto
{
    public string IsoLanguageCode { get; set; }
    public string StringCode { get; set; }
    public string LocalizedString { get; set; }
}