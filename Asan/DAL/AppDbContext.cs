using Asan.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Asan.DAL
{
    public class AppDbContext : IdentityDbContext<Appuser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        public DbSet<Slider> Sliders { get; set; }
        public DbSet<Contract> Contracts { get; set; }
        public DbSet<Document> Documents { get; set; }
        public DbSet<AboutSlider> AboutSliders { get; set; }
        public DbSet<GeneralInformation> GeneralInformation { get; set; }
        public DbSet<Regulation> Regulation { get; set; }
        public DbSet<Legislation> Legislations { get; set; }
        public DbSet<LegislationCategory> LegislationCategory { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Quiz>  Quizs { get; set; }
        public DbSet<Exam> Exams { get; set; }
        public DbSet<News> News { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Information> Informations { get; set; }
        public DbSet<Connect> Connects { get; set; }
        public DbSet<Informationuse> Informationuses { get; set; }
        public DbSet<Header> Headers { get; set; }
        public DbSet<Penalty> Penalties { get; set; }
        public DbSet<PenaltyCategory> PenaltyCategories { get; set; }
        public DbSet<Fine>  Fines{ get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Static> Statistic { get; set; }
        public DbSet<Statistic> Statistics { get; set; }
        public DbSet<StatisticImg> StatisticImg { get; set; }
    }
}
