//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AccompProject.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class IAEntities : DbContext
    {
        public IAEntities()
            : base("name=IAEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<IAList> IALists { get; set; }
        public virtual DbSet<IAListView> IAListViews { get; set; }
        public virtual DbSet<FSEntry> FSEntries { get; set; }
        public virtual DbSet<YearReference> YearReferences { get; set; }
        public virtual DbSet<IA_PROFILE> IA_PROFILE { get; set; }
        public virtual DbSet<IA_AREA> IA_AREA { get; set; }
        public virtual DbSet<IA_AREA_VIEW> IA_AREA_VIEW { get; set; }
        public virtual DbSet<CANALS_STATIONING> CANALS_STATIONING { get; set; }
        public virtual DbSet<MappingDataSystemIA> MappingDataSystemIA { get; set; }
        public virtual DbSet<MappingDataIAView> MappingDataIAViews { get; set; }
        public virtual DbSet<BOARD> BOARDs { get; set; }
        public virtual DbSet<LIST_OF_POSITION> LIST_OF_POSITION { get; set; }
    }
}
