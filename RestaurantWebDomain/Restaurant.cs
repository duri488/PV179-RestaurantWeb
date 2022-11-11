﻿namespace RestaurantWebDomain;

public class Restaurant
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string Address { get; set; }
    public double Latitude { get; set; }
    public double Longtitude { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }
    public List<Meal> Meals { get; set; } = new ();
    public List<Drink> Drinks { get; set; } = new();
    public List<WeeklyMenu> WeeklyMenus { get; set; } = new();
}