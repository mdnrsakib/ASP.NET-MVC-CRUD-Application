using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Project_Monthly_08.Models
{
    public enum TransPortMode { Bus=1, PrivateCar, CruiseShip, Plane, PrivateJet}
    public enum PakageCategory { Gold=1, Silver, Platinum, Express, Economy }
    public class TravelAgent
    {
        public int TravelAgentId { get; set; }
        [Required, StringLength(50), Display(Name = "Agent Name")]
        public string AgentName { get; set; }
        [Required, StringLength(50), DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required, StringLength(70), Display(Name = "Agent Address")]
        public string AgentAddress { get; set; }
        public ICollection<AgentTourPackage> AgentTourPackages { get; set; }=new List<AgentTourPackage>();
    }
    public class TourPackage
    {
        public int TourPackageId { get; set; }
        [EnumDataType(typeof(PakageCategory)), Display(Name = "Pakage Category")]
        public PakageCategory PackageCategory { get; set; }
        [Required, StringLength(50), Display(Name = "Package Name")]
        public string PackageName { get; set; }
        [Required, Display(Name = "Cost Per Person"), DisplayFormat(DataFormatString = "{0:0.00}", ApplyFormatInEditMode = true)]
        public decimal CostPerPerson { get; set; }
        [Required, Display(Name = "Tour Time")]
        public int TourTime { get; set; }
        public ICollection<PackageFeature> PackageFeatures { get; set; } =new List<PackageFeature>();
        public ICollection<Tourist> Tourists { get;set; } = new List<Tourist>();
        public ICollection<AgentTourPackage> AgentTourPackages { get; set; } = new List<AgentTourPackage>();
    }
    public class PackageFeature
    {
        public int PackageFeatureId { get; set; }
        [EnumDataType(typeof(TransPortMode)), Display(Name = "TransPort Mode")]
        public TransPortMode TransportMode { get; set; }
        [Required, StringLength(50), Display(Name = "Hotel Booking")]
        public string HotelBooking { get; set; }
        public bool Status { get; set; }
        [ForeignKey("TourPackage")]
        public int TourPackageId { get; set; }
        public TourPackage TourPackage { get; set; }
    }
    public class Tourist
    {
        public int TouristId { get; set; }
        [Required, StringLength(50), Display(Name = "Tourist Name")]
        public string TouristName { get; set; }
        [Required, Column(TypeName = "date"), Display(Name = "Booking Date"), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime BookingDate { get; set; }
        [Required, StringLength(50), Display(Name = "Tourist Occupation")]
        public string TouristOccupation { get; set; }
        [Required]
        public string TouristPicture { get; set; }
        [ForeignKey("TourPackage")]
        public int TourPackageId { get; set; }
        public TourPackage TourPackage { get; set; }

    }
    public class AgentTourPackage
    {
        [Key, Column(Order =0), ForeignKey("TravelAgent")]
        public int TravelAgentId { get; set; }
        [Key, Column(Order = 1),ForeignKey("TourPackage")]
        public int TourPackageId { get; set; }
        public TravelAgent TravelAgent { get; set; }
        public TourPackage TourPackage { get; set; }
    }
    public class TravelTourDbContext : DbContext
    {
        public TravelTourDbContext()
        {
            Database.SetInitializer(new DbInitializer());
        }
        public DbSet<TravelAgent> TravelAgents { get; set; }
        public DbSet<TourPackage> TourPackages { get; set; }
        public DbSet<PackageFeature> PackageFeatures { get; set; }
        public DbSet<Tourist> Tourists { get; set; }
        public DbSet<AgentTourPackage> AgentTourPackages { get; set; }
    }
    public class DbInitializer : DropCreateDatabaseIfModelChanges<TravelTourDbContext>
    {
        protected override void Seed(TravelTourDbContext context)
        {
            TravelAgent t = new TravelAgent { AgentName = "Asib", Email = "asib@gmail.com", AgentAddress = "Mirpur, Dhaka" };
            TourPackage tp = new TourPackage { PackageCategory = PakageCategory.Economy, PackageName = "GhureAsi", CostPerPerson = 2500.00M,TourTime=5 };
            tp.PackageFeatures.Add(new PackageFeature { TransportMode = TransPortMode.Bus, HotelBooking = "Chattrogram View", Status = true});
            tp.Tourists.Add(new Tourist { TouristName = "Sakib", BookingDate = new DateTime(2000, 02, 02), TouristOccupation = "Student", TouristPicture = "1.jpg" });
            AgentTourPackage atp = new AgentTourPackage { TourPackage = tp, TravelAgent = t };
            context.TravelAgents.Add(t);
            context.TourPackages.Add(tp);
            context.AgentTourPackages.Add(atp);
            context.SaveChanges();

            

        }
    }
}