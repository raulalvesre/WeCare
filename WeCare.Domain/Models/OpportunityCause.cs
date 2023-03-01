using WeCare.Domain.Core;

namespace WeCare.Domain.Models;

public class OpportunityCause : Enumeration
{
    public static OpportunityCause Politics = new(1, "POLITICS");
    public static OpportunityCause CitizenParticipation = new(2, "CITIZEN_PARTICIPATION");
    public static OpportunityCause FightAgainstHunger = new(3, "FIGHT_AGAINST_HUNGER");
    public static OpportunityCause FightAgainstPoverty = new(4, "FIGHT_AGAINST_POVERTY");
    public static OpportunityCause ConsciousConsumption = new(5, "CONSCIOUS_CONSUMPTION");
    public static OpportunityCause ChildrenAndYouth = new(6, "CHILDREN_AND_YOUTH");
    public static OpportunityCause CultureSportsAndArt = new(7, "CULTURE_SPORTS_AND_ART");
    public static OpportunityCause CommunityDevelopment = new(8, "COMMUNITY_DEVELOPMENT");
    public static OpportunityCause Education = new(9, "EDUCATION");
    public static OpportunityCause RacialEquity = new(10, "RACIAL_EQUITY");
    public static OpportunityCause Sports = new(11, "SPORTS");
    public static OpportunityCause Elderly = new(12, "ELDERLY");
    public static OpportunityCause Youth = new(13, "YOUTH");
    public static OpportunityCause Lgbti = new(14, "LGBTI");
    public static OpportunityCause Environment = new(15, "ENVIRONMENT");
    public static OpportunityCause UrbanMobility = new(16, "URBAN_MOBILITY");
    public static OpportunityCause Women = new(17, "WOMEN");
    public static OpportunityCause DisabledPeople = new(18, "DISABLED_PEOPLE");
    public static OpportunityCause HomelessPopulation = new(19, "HOMELESS_POPULATION");
    public static OpportunityCause IndigenousPeople = new(20, "INDIGENOUS_PEOPLE");
    public static OpportunityCause AnimalProtection = new(21, "ANIMAL_PROTECTION");
    public static OpportunityCause Refugees = new(22, "REFUGEES");
    public static OpportunityCause Health = new(23, "HEALTH");
    public static OpportunityCause Sustainability = new(24, "SUSTAINABILITY");
    public static OpportunityCause ProfessionalTraining = new(25, "PROFESSIONAL_TRAINING");

    public IEnumerable<VolunteerOpportunity> VolunteerOpportunities { get; set; } = new List<VolunteerOpportunity>();

    public OpportunityCause(int id, string name) : base(id, name)
    {
    }
}