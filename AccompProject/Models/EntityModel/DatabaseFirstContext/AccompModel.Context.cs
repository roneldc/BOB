﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AccompProject.Models.EntityModel
{
    using NotifSystem.Web.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.ModelConfiguration.Conventions;
    
    public partial class AccomplishmentEntities : DbContext
    {
        public AccomplishmentEntities()
            : base("name=AccomplishmentEntities")
        {
            this.Database.CommandTimeout = 60;
        }

       
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }

        public virtual DbSet<ACCOMPLISHMENT> ACCOMPLISHMENTs { get; set; }
        public virtual DbSet<ASATable> ASATables { get; set; }
        public virtual DbSet<AspNetUser> AspNetUsers { get; set; }
        public virtual DbSet<UsersProfile> UsersProfiles { get; set; }
        public virtual DbSet<TypeUser> TypeUsers { get; set; }
        public virtual DbSet<IMTSS_MOOE> IMTSS_MOOE { get; set; }
        public virtual DbSet<IMTSS_PS> IMTSS_PS { get; set; }
        public virtual DbSet<IMTSSFinancialViewModel> IMTSSFinancialViewModels { get; set; }
        public virtual DbSet<IMTSS_Particulars> IMTSS_Particulars { get; set; }
        public virtual DbSet<IMTSS_Physical> IMTSS_Physical { get; set; }
        public virtual DbSet<IMTSS_SubParticulars> IMTSS_SubParticulars { get; set; }
        public virtual DbSet<IMTSS_SubsubParticulars> IMTSS_SubsubParticulars { get; set; }
        public virtual DbSet<IMTSSViewModelPhysical> IMTSSViewModelPhysicals { get; set; }
        public virtual DbSet<IMTSS_Financial> IMTSS_Financial { get; set; }
        public virtual DbSet<IMTSS_ParticularsFinancial> IMTSS_ParticularsFinancial { get; set; }
        public virtual DbSet<IMTSSViewModelFinancial> IMTSSViewModelFinancials { get; set; }
        public virtual DbSet<FinancialDetail> FinancialDetails { get; set; }
        public virtual DbSet<FinancialView> FinancialViews { get; set; }
        public virtual DbSet<PhysicalDetail> PhysicalDetails { get; set; }
        public virtual DbSet<PhysicalView> PhysicalViews { get; set; }
        public virtual DbSet<FinancialOBD> FinancialOBDs { get; set; }
        public virtual DbSet<FinancialOBDView> FinancialOBDViews { get; set; }
        public virtual DbSet<FinancialNCAADA> FinancialNCAADAs { get; set; }
        public virtual DbSet<FinancialNCAADAView> FinancialNCAADAViews { get; set; }
        public virtual DbSet<PhysicalAccomp> PhysicalAccomps { get; set; }
        public virtual DbSet<PhysicalAccompView> PhysicalAccompViews { get; set; }
        public virtual DbSet<DBM> DBMs { get; set; }
        public virtual DbSet<DBMView> DBMViews { get; set; }
        public virtual DbSet<File> Files { get; set; }
        public virtual DbSet<FileAccomp> FileAccomps { get; set; }
        public virtual DbSet<tblPOW_Breakdown> tblPOW_Breakdown { get; set; }
        public virtual DbSet<tblPOW_Main> tblPOW_Main { get; set; }
        public virtual DbSet<tblPOW_Work> tblPOW_Work { get; set; }
        public virtual DbSet<Pow_View> Pow_View { get; set; }
        public virtual DbSet<FileAccompView> FileAccompViews { get; set; }
        public virtual DbSet<FilePOWAccomp> FilePOWAccomps { get; set; }
        public virtual DbSet<FilePOWAccompView> FilePOWAccompViews { get; set; }
        public virtual DbSet<IMTSS_Assistance> IMTSS_Assistance { get; set; }
        public virtual DbSet<IMTSS_CapacityIA> IMTSS_CapacityIA { get; set; }
        public virtual DbSet<IMTSS_CapacityIAWORKSHOP> IMTSS_CapacityIAWORKSHOP { get; set; }
        public virtual DbSet<IMTSS_CapacityStaff> IMTSS_CapacityStaff { get; set; }
        public virtual DbSet<IMTSS_CapacityStaffWORKSHOP> IMTSS_CapacityStaffWORKSHOP { get; set; }
        public virtual DbSet<IMTSSViewModel> IMTSSViewModels { get; set; }
        public virtual DbSet<IMTSS_IARegistration> IMTSS_IARegistration { get; set; }
        public virtual DbSet<IMTSS_IAStrengthening> IMTSS_IAStrengthening { get; set; }
        public virtual DbSet<IMTSS_IASustenance> IMTSS_IASustenance { get; set; }
        public virtual DbSet<IMTSS_IDPPersonnel> IMTSS_IDPPersonnel { get; set; }
        public virtual DbSet<IMTSS_Model1> IMTSS_Model1 { get; set; }
        public virtual DbSet<IMTSS_Model2> IMTSS_Model2 { get; set; }
        public virtual DbSet<IMTSS_Model3> IMTSS_Model3 { get; set; }
        public virtual DbSet<IMTSS_Model4> IMTSS_Model4 { get; set; }
        public virtual DbSet<IMTSS_IAOrganization> IMTSS_IAOrganization { get; set; }
        public virtual DbSet<IMTSS> IMTSSes { get; set; }
        public virtual DbSet<IMTSS_ConAct> IMTSS_ConAct { get; set; }
        public virtual DbSet<ContractProfile> ContractProfiles { get; set; }
        public virtual DbSet<ContractStatu> ContractStatus { get; set; }
        public virtual DbSet<ContractView> ContractViews { get; set; }
        public virtual DbSet<ContractStatusView> ContractStatusViews { get; set; }
        public virtual DbSet<ClimateChangeAccomp> ClimateChangeAccomps { get; set; }
        public virtual DbSet<ClimateChangeProfile> ClimateChangeProfiles { get; set; }
        public virtual DbSet<ClimateChangeAccompView> ClimateChangeAccompViews { get; set; }
        public virtual DbSet<ClimateChangeProfileView> ClimateChangeProfileViews { get; set; }
        public virtual DbSet<FinancialASAAttachment> FinancialASAAttachments { get; set; }
        public virtual DbSet<ASAAttachmentView> ASAAttachmentViews { get; set; }
        public virtual DbSet<RegionalFinancialView> RegionalFinancialViews { get; set; }
        public virtual DbSet<RegionalFinancialViewSummaryProject> RegionalFinancialViewSummaryProjects { get; set; }
        public virtual DbSet<RegionalFinancialViewSummaryProjectASA> RegionalFinancialViewSummaryProjectASAs { get; set; }
        public virtual DbSet<RegionalFinancialViewForOD> RegionalFinancialViewForODs { get; set; }
        public virtual DbSet<IMTSS_ModelOther> IMTSS_ModelOther { get; set; }
        public virtual DbSet<IMTSS_ModContract> IMTSS_ModContract { get; set; }
        public virtual DbSet<ProjectCoordinate> ProjectCoordinates { get; set; }
        public virtual DbSet<ProjectCoordinatesView> ProjectCoordinatesViews { get; set; }
        public virtual DbSet<ProjectStatusImplementation> ProjectStatusImplementations { get; set; }
        public virtual DbSet<ProjectStatusImplementationView> ProjectStatusImplementationViews { get; set; }
        public virtual DbSet<FSDEPhysical> FSDEPhysicals { get; set; }
        public virtual DbSet<FSDEPhysicalView> FSDEPhysicalViews { get; set; }
        public virtual DbSet<IMTSS_EModContract> IMTSS_EModContract { get; set; }
        public virtual DbSet<IMTSS_Finance> IMTSS_Finance { get; set; }
        public virtual DbSet<IMTSS_Finance_View> IMTSS_Finance_View { get; set; }
        public virtual DbSet<Notification> Notifications { get; set; }
        public virtual DbSet<NotificationsView> NotificationsViews { get; set; }
        public virtual DbSet<ContractSuspension> ContractSuspensions { get; set; }
        public virtual DbSet<ContractExtension> ContractExtensions { get; set; }
        public virtual DbSet<ContractExtensionView> ContractExtensionViews { get; set; }
        public virtual DbSet<ContractSuspensionView> ContractSuspensionViews { get; set; }
        public virtual DbSet<ToCluster> ToClusters { get; set; }
        public virtual DbSet<ToClusterView> ToClusterViews { get; set; }
        public virtual DbSet<NCARequest> NCARequests { get; set; }
        public virtual DbSet<NCARequestView> NCARequestViews { get; set; }
        public virtual DbSet<ContractBilling> ContractBillings { get; set; }
        public virtual DbSet<ContractBillingView> ContractBillingViews { get; set; }
        public virtual DbSet<ContractAmountHistory> ContractAmountHistories { get; set; }
        public virtual DbSet<ContractAmountHistoryView> ContractAmountHistoryViews { get; set; }
        public virtual DbSet<SM> SMS { get; set; }
        public virtual DbSet<SMSMonitoredView> SMSMonitoredViews { get; set; }
        public virtual DbSet<ImplementationAndCoordinate> ImplementationAndCoordinates { get; set; }
        public virtual DbSet<ConsultantList> ConsultantLists { get; set; }
        public virtual DbSet<FSDEContractStudy> FSDEContractStudies { get; set; }
        public virtual DbSet<PositionList> PositionLists { get; set; }
        public virtual DbSet<StudyConsultant> StudyConsultants { get; set; }
        public virtual DbSet<FSDEConsultantListView> FSDEConsultantListViews { get; set; }
        public virtual DbSet<FSDEContractStudyView> FSDEContractStudyViews { get; set; }
        public virtual DbSet<DIMEPic> DIMEPics { get; set; }
        public virtual DbSet<DIMEPicView> DIMEPicViews { get; set; }
        public virtual DbSet<BingoNoti> BingoNotis { get; set; }
        public virtual DbSet<BingoWinner> BingoWinners { get; set; }
        public virtual DbSet<BingoImage> BingoImages { get; set; }
        public virtual DbSet<ListofIssue> ListofIssues { get; set; }
        public virtual DbSet<ProjectIssue> ProjectIssues { get; set; }
        public virtual DbSet<ProjectIssuesView> ProjectIssuesViews { get; set; }
        public virtual DbSet<FileAccompContract> FileAccompContracts { get; set; }
        public virtual DbSet<FileAccompContractView> FileAccompContractViews { get; set; }
        public virtual DbSet<ProDocument> ProDocuments { get; set; }
        public virtual DbSet<FileAccompTarget> FileAccompTargets { get; set; }
        public virtual DbSet<ACCOMPLISHMENTBaseline> ACCOMPLISHMENTBaselines { get; set; }
        public virtual DbSet<PhysicalAccompBaseline> PhysicalAccompBaselines { get; set; }
        public virtual DbSet<PhysicalAccompSummaryLog> PhysicalAccompSummaryLogs { get; set; }
        public virtual DbSet<FileAccompAccomp> FileAccompAccomps { get; set; }
      
    }
}
